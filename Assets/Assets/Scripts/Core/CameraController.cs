using System;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;


public class CameraController : MonoBehaviour,IObserver
{
    public GameObject cameraCanvas;
    public RawImage CameraFeed;
    public RawImage CapturedImagePreview;
    public GameObject PreviewCanvas;
    public Button OKButton;
    public Button RecaptureButton;

    private WebCamTexture frontCamera;
    private Texture defaultBackground;
    private Texture2D capturedTexture;

    public bool CamRunning { get; private set; }

    private void Awake()
    {
        // Make sure to assign the UI elements in the Inspector
        OKButton.onClick.AddListener(OnOKButtonClicked);
        RecaptureButton.onClick.AddListener(OnRecaptureButtonClicked);
    }

    void OnEnable()
    {
        EventManager.OnScreenChange += OnNotify;
    }

    public void OnNotify(string eventName)
    {
        if (eventName == EventManager.CAMERA_PAGE)
        {
            if (Application.HasUserAuthorization(UserAuthorization.WebCam))
            {
                Debug.Log("Camera Initialized");
                InitializeCamera();
            }
            else
            {
                Debug.LogError("Camera permission not granted.");
            }
        }
    }

    void Update()
    {
        // You can add any necessary update logic here
    }

    void InitializeCamera()
    {
        cameraCanvas.SetActive(true);
        defaultBackground = CameraFeed.texture;
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
                int angle = -frontCamera.videoRotationAngle;
                CameraFeed.rectTransform.localEulerAngles = new Vector3(0, 0, angle);

                break;
            }
        }

        if (frontCamera == null)
        {
            Debug.Log("Unable to find back camera.");
            return;
        }

        if (CameraFeed != null)
        {
            CameraFeed.material.mainTexture = frontCamera;
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
        capturedTexture = new Texture2D(frontCamera.width, frontCamera.height);
        capturedTexture.SetPixels(frontCamera.GetPixels());
        capturedTexture.Apply();

        // Display the captured image in the preview
        CapturedImagePreview.texture = capturedTexture;

        PreviewCanvas.SetActive(true);
        if (frontCamera != null)
        {
            frontCamera.Stop();
        }

        if (CameraFeed != null)
        {
            CameraFeed.material.mainTexture = defaultBackground;
        }
        cameraCanvas.SetActive(false);
    }

    private void OnOKButtonClicked()
    {
        // Handle the OK button click
        // You can implement the logic to proceed with the captured image
        // For example, save the image, process it, or move to the next scene
        // SceneManager.LoadScene("YourNextSceneName");

        // For now, just hide the preview canvas
        PreviewCanvas.SetActive(false);
        UIReferences.Instance.cameraPage.SetActive(false);
        CameraOff();
        EventManager.ChangeScreen(EventManager.MAKE_CHOICE_PAGE);
    }

    private void OnRecaptureButtonClicked()
    {
        // Handle the Recapture button click
        PreviewCanvas.SetActive(false);
        InitializeCamera();
    }

    public void CameraOff()
    {
        if (frontCamera != null)
        {
            frontCamera.Stop();
        }

        if (CameraFeed != null)
        {
            CameraFeed.material.mainTexture = defaultBackground;
        }
        cameraCanvas.SetActive(false);
        UIReferences.Instance.cameraPage.SetActive(false);
        EventManager.ChangeScreen(EventManager.PROFILE_PAGE);
    }

    void OnDisable()
    {
        EventManager.OnScreenChange -= OnNotify;
    }
}