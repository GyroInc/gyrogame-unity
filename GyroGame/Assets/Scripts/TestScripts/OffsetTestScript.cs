﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OffsetTestScript : MonoBehaviour
{
    public Transform absoluteCube;
    public Transform relativeCube;

    public Quaternion correctedCube = Quaternion.identity;
    public Vector3 cubeEulers;
    readonly Quaternion playerPerspectiveCorrection = Quaternion.identity;

    int calibrationStep;
    float yawOffset;

    private void Start()
    {
        HardwareInterface.Instance.Connect();
    }

    void Update()
    {
        if (HardwareInterface.Instance.IsConnected())
        {
            if(absoluteCube != null)
                absoluteCube.rotation = HardwareInterface.Instance.GetRotation();
            
            correctedCube = Quaternion.Euler(new Vector3(0, yawOffset, 0)) * HardwareInterface.Instance.GetRotation();
            cubeEulers = correctedCube.eulerAngles;

            if (relativeCube != null)
                relativeCube.rotation = playerPerspectiveCorrection * correctedCube;

            if (Input.GetKeyDown(KeyCode.Space))
            {
                switch(calibrationStep)
                {
                    case 0:
                        HardwareInterface.Instance.SetLed(5, CubeColor.green);
                        HardwareInterface.Instance.SetLed(1, CubeColor.red);
                        calibrationStep++;
                        break;
                    case 1:
                        yawOffset = -HardwareInterface.Instance.GetRotation().eulerAngles.y;
                        HardwareInterface.Instance.SetLed(5, CubeColor.black);
                        HardwareInterface.Instance.SetLed(1, CubeColor.black);
                        break;
                    default:
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
