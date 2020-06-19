using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class EscapeMenu : MonoBehaviour
{
    public static EscapeMenu active;
    public GameObject escapePanel;
    public bool escapeActive = false;
    public Button buttonConnect;
    public Toggle connectedIndicator;
    private Text connectButtonText;

    private void Start()
    {
        connectButtonText = buttonConnect.GetComponentInChildren<Text>();
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
                Cursor.visible = false;
            }
            else
            {
                escapeActive = true;
                Time.timeScale = 0;
                escapePanel.SetActive(true);
                Cursor.lockState = CursorLockMode.None;
                Cursor.visible = true;
            }
        }

        ColorBlock cb = connectedIndicator.colors;
        if (HardwareInterface.Instance.connected)
        {
            connectedIndicator.isOn = true;
            connectButtonText.text = "Disconnect the Cube";
            cb.disabledColor= Color.green;
        }
        else
        {
            connectedIndicator.isOn = false;
            cb.disabledColor = Color.red;
        }
        connectedIndicator.colors = cb;
    }

    public void toggleCubeConnection()
    {
        if (HardwareInterface.Instance.isAttemptingConnection())
        {
            HardwareInterface.Instance.CancelConnectionAttempt();
            connectButtonText.text = "Connect the Cube";
        }
        else if (HardwareInterface.Instance.connected)
        {
            HardwareInterface.Instance.Disconnect();
            connectButtonText.text = "Connect the Cube";
        }
        else
        {
            HardwareInterface.Instance.Connect();
            connectButtonText.text = "Connecting...";
        }
    }

    public void ExitTitle()
    {
        Time.timeScale = 1;
        SceneManager.LoadSceneAsync(0);
    }

    public void ExitGame()
    {
        Application.Quit();
    }
}
