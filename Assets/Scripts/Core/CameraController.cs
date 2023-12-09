using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;


public class CameraController : MonoBehaviour
{
    public RawImage CameraFeed;
    private WebCamTexture frontCamera;

    public bool CamRunning { get; private set; }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    void Update()
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

    // Initialize the camera
    void InitializeCamera()
    {
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

    public void CloseCamera()
    {
        if (frontCamera != null)
        {
            frontCamera.Stop();
            SceneManager.LoadScene(0);
        }
    }
}