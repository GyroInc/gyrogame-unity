using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstacleMover : MonoBehaviour
{
    public GameObject selected;
    public float selectionRange;

    Color orgColor;
    Color targetColor;

    bool connectionConfirmed = false;
    bool selectionActive = false;

    private void Start()
    {
        HardwareInterface.active.Connect();
    }

    void Update()
    {
        if(!connectionConfirmed)
        {
            if(HardwareInterface.active.IsConnected())
            {
                connectionConfirmed = true;
                HardwareInterface.active.FadeAllLeds(CubeColor.black, 1000);
            }            
        }

        //select on click
        if(Input.GetMouseButtonDown(0))
        {
            RaycastHit hit;
            if (Physics.Raycast(Camera.main.transform.position, Camera.main.transform.forward, out hit))
            {
                //requires right tag
                if (hit.transform.tag == "Rotatable")
                {
                    selected = hit.transform.gameObject;

                    //color stuff
                    orgColor = selected.transform.GetComponent<MeshRenderer>().material.color;
                    targetColor = Color.green;
                    HardwareInterface.active.FadeAllLeds(CubeColor.green, 1000);
                    selectionActive = true;
                }
                else
                {
                    targetColor = orgColor;
                    HardwareInterface.active.FadeAllLeds(CubeColor.black, 1000);
                    selectionActive = false;
                }
            }
            else
            {
                targetColor = orgColor;
                HardwareInterface.active.FadeAllLeds(CubeColor.black, 1000);
                selectionActive = false;
            }
                
        }

        if(selectionActive)
        {
            if(HardwareInterface.active.IsConnected())
            {
                selected.transform.parent.rotation = HardwareInterface.active.GetRotation();
            }
        }

        if(selected != null)
        {
            //lerp colors
            selected.transform.GetComponent<MeshRenderer>().material.color = Color.Lerp(selected.transform.GetComponent<MeshRenderer>().material.color, targetColor, Time.deltaTime);
        }
    }
}
