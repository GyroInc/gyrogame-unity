﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickupController : MonoBehaviour
{
    public bool setCubeColor, fadeCubeColor;
    public int fadeTime;
    public Color cubeColor;

    void Start()
    {
        
    }

    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.tag != "Player") return;

        if (Input.GetKeyDown(KeyCode.E))
        {
            if (setCubeColor)
            {
                HardwareInterface.Instance.SetAllLeds(new CubeColor(cubeColor));
            } 
            else if (fadeCubeColor)
            {
                HardwareInterface.Instance.FadeAllLeds(new CubeColor(cubeColor), fadeTime);
            }
            Destroy(gameObject);
        }
    }

    private void OnTriggerExit(Collider other)
    {
    }
}
