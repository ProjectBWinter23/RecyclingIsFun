using Assets.Scripts.Database;
using System;
using System.IO;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.SceneManagement;
using UnityEngine.UI;


public class CameraController : MonoBehaviour
{
    public RawImage CameraFeed;
    private WebCamTexture frontCamera;
    private Texture defaultbackgroud;
    private string ClassificationResult;

    // Intializes path in temp cache directory on user's machine in Start().
    // To test: add a picture on this path C:\Users\<user>\AppData\Local\Temp\DefaultCompany\RecyclingIsFun
    private string ImagePath;
    public bool CamRunning { get; private set; }

    private void Awake()
    {

    }

    void Start()
    {
        // Request camera permission
        if (Application.HasUserAuthorization(UserAuthorization.WebCam))
        {
            ImagePath = Path.Combine(Application.temporaryCachePath.Replace('/', Path.DirectorySeparatorChar), "CapturedImage.jpg");
            InitializeCamera();
        }
        else
        {
            Debug.LogError("Camera permission not granted.");
        }
    }

    void Update()
    {

    }

    void InitializeCamera()
    {
        defaultbackgroud = CameraFeed.texture;
        WebCamDevice[] devices = WebCamTexture.devices;

        if (devices.Length == 0)
        {
            Debug.Log("No camera detected");
            return;
        }

        for (int i = 0; i < devices.Length; i++)
        {
            if (devices[i].isFrontFacing)
            {
                frontCamera = new WebCamTexture(devices[i].name, Screen.width, Screen.height);
                break;
            }
        }

        if (frontCamera == null)
        {
            Debug.Log("Unable to find back camera.");
            return;
        }

        // Check if the object has a Renderer component
        if (CameraFeed != null)
        {
            // Apply the camera texture to the material of the Renderer component
            CameraFeed.material.mainTexture = frontCamera;

            // Start the camera
            frontCamera.Play();
            CamRunning = true;
        }
        else
        {
            Debug.LogError("Renderer component not found on the object.");
        }
    }

    public void Capture()
    {
        // Create a new texture with the dimensions of the webcam texture
        Texture2D texture = new Texture2D(frontCamera.width, frontCamera.height);

        // Set the pixels of the new texture to the pixels of the webcam texture
        texture.SetPixels(frontCamera.GetPixels());
        texture.Apply();

        // Optionally, save the texture as a PNG file
        byte[] bytes = texture.EncodeToJPG();
        System.IO.File.WriteAllBytes(ImagePath, bytes);
    }

    public void CameraOff()
    {
        if (frontCamera != null)
        {
            CameraFeed.material.mainTexture = defaultbackgroud;
            frontCamera.Stop();
        }

        SceneManager.LoadScene(0);
    }

    /// <summary>
    /// TO DO: Method to be linked to a Predict Button or UI Element.
    /// Assigns the result to the <property> ClassificationResult </property>
    /// </summary>
    public async void Predict()
    {
        //TO DO: Close Camera

        await PredictRequest();
        Debug.Log(ClassificationResult);

        //TO DO: Display ClassificationResult In UI
    }


    /// <summary>
    /// Waste Prediction Request
    /// </summary>
    /// <returns></returns>
    public async Task PredictRequest()
    {
        string result = string.Empty;
        var url = "https://garabage-classifier.onrender.com/predict";

        WWWForm form = new WWWForm();
        form.AddBinaryData("image", System.IO.File.ReadAllBytes(ImagePath), "image.jpg");
        try
        {
            using var www = UnityWebRequest.Post(url, form);

            var operation = www.SendWebRequest();

            while (operation.webRequest.result == UnityWebRequest.Result.InProgress)
            {
                await Task.Yield();
            }

            if (www.result != UnityWebRequest.Result.Success)
            {
                Debug.LogError($"Failed: {www.error}");
            }

            ClassificationResult = www.downloadHandler.text;

        }
        catch (Exception ex)
        {
            Debug.LogError(ex.Message);
        }
    }

}