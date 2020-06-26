using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class EscapeMenu : MonoBehaviour
{
    public static EscapeMenu Instance { get; private set; }
    public GameObject escapePanel;
    public bool escapeActive;
    public Button buttonConnect;
    public Toggle connectedIndicator;
    public Slider lookSpeedSlider;

    private Text connectButtonText;

    private void Start()
    {
        connectButtonText = buttonConnect.GetComponentInChildren<Text>();
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

        lookSpeedSlider.value = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController2>().GetLooksSpeed();
    }

    public void SetLookSpeed(float speed)
    {
        GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController2>().SetLookSpeed(speed);
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
