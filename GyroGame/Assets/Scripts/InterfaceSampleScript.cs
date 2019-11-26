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
        //HardwareInterface.active.Connect();
    }

    // Update is called once per frame
    void Update()
    {
        Cube1.transform.rotation = HardwareInterface.active.GetRotation();
        Cube2.transform.rotation = HardwareInterface.active.GetRotation() * qOffset;

        HandleKeyInput();
    }

    private void HandleKeyInput()
    {
        // Connect / Disconnect
        if (Input.GetKeyDown(KeyCode.C))
        {
            HardwareInterface.active.Connect();
        }
        if (Input.GetKeyDown(KeyCode.D))
        {
            if (HardwareInterface.active.IsConnected())
            {
                HardwareInterface.active.Disconnect();
            }
            else
            {
                HardwareInterface.active.CancelConnectionAttempt();
            }
        }

        // Color changes

        /* ### Lights fade off ### */
        if (Input.GetKeyDown(KeyCode.Space))
        {
            HardwareInterface.active.FadeAllLeds(CubeColor.black, 1000);
        }
        /* ### All RED ### */
        if (Input.GetKeyDown(KeyCode.R))
        {
            HardwareInterface.active.SetAllLeds(CubeColor.red);
        }
        /* ### All fade to BLUE ### */
        if (Input.GetKeyDown(KeyCode.B))
        {
            HardwareInterface.active.FadeAllLeds(CubeColor.black, 1000);
        }
        /* ### All fade to ORANGE ### */
        if (Input.GetKeyDown(KeyCode.O))
        {
            HardwareInterface.active.FadeAllLeds(CubeColor.orange, 1000);
        }


        /* ### Example of the calculation of an offset for calibration, fully functional ### */
        if (Input.GetKeyDown(KeyCode.N))
        {
            qOffset = Quaternion.Inverse(HardwareInterface.active.GetRotation());

            /* ### Send a singe 'c' to reinitialize gyro in cube firmware ### */
            //port.WriteLine("c");
        }


        if (Input.GetKeyDown(KeyCode.I))
        {
            //port.WriteLine("b64");
        }
        if (Input.GetKeyDown(KeyCode.O))
        {
            //port.WriteLine("b220");
        }

        /* ### Fade to individual colors ### */
        if (Input.GetKeyDown(KeyCode.I))
        {
            HardwareInterface.active.FadeLed(0, new CubeColor(0, 255, 100), 1000);
            HardwareInterface.active.FadeLed(1, new CubeColor(0, 10, 100), 1000);
            HardwareInterface.active.FadeLed(2, new CubeColor(80, 255, 100), 1000);
            HardwareInterface.active.FadeLed(3, new CubeColor(140, 255, 0), 1000);
        }
    }
}
