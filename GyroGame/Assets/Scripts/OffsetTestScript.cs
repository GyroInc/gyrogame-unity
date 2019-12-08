using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OffsetTestScript : MonoBehaviour
{
    public static OffsetTestScript active;
    public Transform absoluteCube;
    public Transform relativeCube;

    public Quaternion correctedCube = Quaternion.identity;
    public Vector3 cubeEulers;
    Quaternion playerPerspectiveCorrection = Quaternion.identity;

    int calibrationStep = 0;
    float yawOffset = 0;

    private void Start()
    {
        active = this;
        HardwareInterface.active.Connect();
    }

    void Update()
    {
        if (HardwareInterface.active.IsConnected())
        {
            if(absoluteCube != null)
                absoluteCube.rotation = HardwareInterface.active.GetRotation();
            
            correctedCube = Quaternion.Euler(new Vector3(0, yawOffset, 0)) * HardwareInterface.active.GetRotation();
            cubeEulers = correctedCube.eulerAngles;

            if (relativeCube != null)
                relativeCube.rotation = playerPerspectiveCorrection * correctedCube;

            if (Input.GetKeyDown(KeyCode.Space))
            {
                switch(calibrationStep)
                {
                    case 0:
                        HardwareInterface.active.SetLed(5, CubeColor.green);
                        HardwareInterface.active.SetLed(1, CubeColor.red);
                        calibrationStep++;
                        break;
                    case 1:
                        yawOffset = -HardwareInterface.active.GetRotation().eulerAngles.y;
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
