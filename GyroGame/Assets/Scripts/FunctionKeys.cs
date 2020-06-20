using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FunctionKeys : MonoBehaviour
{
    public bool ScreenshotEnabled = true;
    public bool OverlayToggleEnabled = true;

    // Start is called before the first frame update
    void Start()
    {

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
}
