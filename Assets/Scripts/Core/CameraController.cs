using System;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;


public class CameraController : MonoBehaviour
{
    public RawImage CameraFeed;
    private WebCamTexture frontCamera;
    private Texture defaultbackgroud;

    public bool CamRunning { get; private set; }

    private void Awake()
    {
        
    }

    void Start()
    {
        // Request camera permission
        if (Application.HasUserAuthorization(UserAuthorization.WebCam))
        {
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
        byte[] bytes = texture.EncodeToPNG();
        System.IO.File.WriteAllBytes("C:\\Users\\Marwa\\Documents\\Unity\\CapturedPicture.png", bytes);
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
}