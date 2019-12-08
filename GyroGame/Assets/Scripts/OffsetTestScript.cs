using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OffsetTestScript : MonoBehaviour
{
    public Transform absoluteCube;
    public Transform relativeCube;

    Quaternion correctedCube;
    Quaternion playerPerspectiveCorrection = Quaternion.identity;

    int calibrationStep = 0;
    float yawOffset = 0;

    private void Start()
    {
        HardwareInterface.active.Connect();
    }

    void Update()
    {
        if (HardwareInterface.active.IsConnected())
        {
            absoluteCube.rotation = HardwareInterface.active.GetRotation();
            correctedCube = Quaternion.Euler(new Vector3(0, yawOffset, 0)) * HardwareInterface.active.GetRotation();

            relativeCube.rotation = playerPerspectiveCorrection * correctedCube;

            if (Input.GetKeyDown(KeyCode.Space))
            {
                switch(calibrationStep)
                {
                    case 0:
                        HardwareInterface.active.SetLed(5, CubeColor.green);
                        HardwareInterface.active.SetLed(1, CubeColor.orange);
                        calibrationStep++;
                        break;
                    case 1:
                        yawOffset = HardwareInterface.active.GetRotation().eulerAngles.y;
                        HardwareInterface.active.SetLed(5, CubeColor.black);
                        HardwareInterface.active.SetLed(1, CubeColor.black);
                        break;
                }                
            }
        }
    }

    public void CalculatePlayerPerspectiveCorrection()
    {
        //tbd
    }
}
