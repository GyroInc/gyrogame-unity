using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InterfaceSampleScript : MonoBehaviour
{
    public GameObject Cube1;
    public GameObject Cube2;

    private Quaternion qOffset = Quaternion.identity;

    // Start is called before the first frame update
    void Start()
    {
        HardwareInterface.Instance.Connect();
    }

    // Update is called once per frame
    void Update()
    {
        Cube1.transform.rotation = HardwareInterface.Instance.GetRotation();
        Cube2.transform.rotation = HardwareInterface.Instance.GetRotation() * qOffset;

        HandleKeyInput();
    }

    private void HandleKeyInput()
    {
        // Connect / Disconnect
        if (Input.GetKeyDown(KeyCode.C))
        {
            HardwareInterface.Instance.Connect();
        }
        if (Input.GetKeyDown(KeyCode.D))
        {
            if (HardwareInterface.Instance.IsConnected())
            {
                HardwareInterface.Instance.Disconnect();
            }
            else
            {
                HardwareInterface.Instance.CancelConnectionAttempt();
            }
        }

        // Color changes

        /* ### Lights fade off ### */
        if (Input.GetKeyDown(KeyCode.Space))
        {
            HardwareInterface.Instance.FadeAllLeds(CubeColor.black, 1000);
        }
        /* ### All RED ### */
        if (Input.GetKeyDown(KeyCode.R))
        {
            HardwareInterface.Instance.SetAllLeds(CubeColor.red);
        }
        /* ### All fade to BLUE ### */
        if (Input.GetKeyDown(KeyCode.B))
        {
            HardwareInterface.Instance.FadeAllLeds(CubeColor.black, 1000);
        }
        /* ### All fade to ORANGE ### */
        if (Input.GetKeyDown(KeyCode.O))
        {
            HardwareInterface.Instance.FadeAllLeds(CubeColor.orange, 1000);
        }
        /* ### Example of the calculation of an offset for calibration, fully functional ### */
        if (Input.GetKeyDown(KeyCode.N))
        {
            qOffset = Quaternion.Inverse(HardwareInterface.Instance.GetRotation());

            /* ### Send a singe 'c' to reinitialize gyro in cube firmware ### */
            HardwareInterface.Instance.SendCommand("c");
        }

        /* ### Fade to individual colors ### */
        if (Input.GetKeyDown(KeyCode.I))
        {
            HardwareInterface.Instance.FadeLed(0, new CubeColor(0, 255, 100), 1000);
            HardwareInterface.Instance.FadeLed(1, new CubeColor(0, 10, 100), 1000);
            HardwareInterface.Instance.FadeLed(2, new CubeColor(80, 255, 100), 1000);
            HardwareInterface.Instance.FadeLed(3, new CubeColor(140, 255, 0), 1000);
        }
    }
}
