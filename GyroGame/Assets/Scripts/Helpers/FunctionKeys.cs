using System;
using UnityEngine;

public class FunctionKeys : MonoBehaviour
{
    public bool ScreenshotEnabled = true;
    public bool OverlayToggleEnabled = true;

    public static FunctionKeys Instance { get; private set; }

    // Start is called before the first frame update
    void Start()
    {

    }
    void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
        }
        else
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (ScreenshotEnabled && Input.GetKeyDown(KeyCode.F2))
        {
            TakeScreenshot();
        }
        if(OverlayToggleEnabled && Input.GetKeyDown(KeyCode.F3))
        {
            ToggleOverlay();
        }
        if (Input.GetKeyDown(KeyCode.F11))
        {
            ToggleFullscreen();
        }
    }

    private void TakeScreenshot()
    {
        string path;
#if UNITY_EDITOR_WIN || UNITY_STANDALONE_WIN
        path = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) + "\\GyroGame\\";
        System.IO.Directory.CreateDirectory(path);
#else
            path = Application.persistentDataPath;
#endif
        ScreenCapture.CaptureScreenshot(path + "GyroGame_" + DateTime.Now.ToString("yyyy_MM_dd_HHmmss") + ".png");
    }

    private void ToggleOverlay()
    {
        Canvas overlayCanvas = GameObject.FindGameObjectWithTag("Overlay").GetComponent<Canvas>();
        overlayCanvas.enabled = !overlayCanvas.enabled;
    }

    private void ToggleFullscreen()
    {
        if (Screen.fullScreen)
        {
            Screen.SetResolution(Screen.currentResolution.width, Screen.currentResolution.height, FullScreenMode.MaximizedWindow);
            Screen.fullScreen = false;
        } else
        {
            Screen.SetResolution(Screen.currentResolution.width, Screen.currentResolution.height, FullScreenMode.FullScreenWindow);
            Screen.fullScreen = true;
        }
    }
}
