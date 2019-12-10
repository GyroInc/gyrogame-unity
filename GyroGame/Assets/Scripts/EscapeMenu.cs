using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EscapeMenu : MonoBehaviour
{
    public static EscapeMenu active;
    public GameObject escapePanel;
    public bool escapeActive = false;
    public Toggle connectedIndicator;

    private void Start()
    {
        active = this;
    }

    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Escape))
        {
            if(escapeActive)
            {
                escapeActive = false;
                Time.timeScale = 1;
                escapePanel.SetActive(false);
                Cursor.lockState = CursorLockMode.Locked;
            }
            else
            {
                escapeActive = true;
                Time.timeScale = 0;
                escapePanel.SetActive(true);
                Cursor.lockState = CursorLockMode.None;
            }
        }

        if(HardwareInterface.active.connected)
        {
            connectedIndicator.isOn = true;
        }
        else
        {
            connectedIndicator.isOn = false;
        }
    }

}
