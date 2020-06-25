using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstacleMover : MonoBehaviour
{
    public bool playerCanRotateObstacles;
    public float selectionRange;
    public Color standbyCubeColor;
    public int cubeColorFadeTime;

    private RotatableObstacle selected;


    private void Start()
    {

    }

    void Update()
    {
        if (EscapeMenu.active.escapeActive || !playerCanRotateObstacles) return;
        //select on click
        if(Input.GetMouseButtonDown(0))
        {
            DeselectObstacle();

            RaycastHit hit;
            if (Physics.Raycast(Camera.main.transform.position, Camera.main.transform.forward, out hit))
            {
                //requires right tag
                if (hit.transform.tag == "Rotatable")
                {
                    selected = hit.transform.parent.gameObject.GetComponent<RotatableObstacle>();
                    if(selected == null)
                    {
                        selected = hit.transform.parent.gameObject.GetComponent<ObstacleCoupler>().GetParentObstacle();
                    }
                    selected.SetActiveObstacle(true);

                    //color stuff
                    HardwareInterface.Instance.FadeAllLeds(new CubeColor(selected.cubeColor), cubeColorFadeTime);
                }
                else
                {
                    HardwareInterface.Instance.FadeAllLeds(new CubeColor(standbyCubeColor), cubeColorFadeTime);
                }
            }
            else
            {
                HardwareInterface.Instance.FadeAllLeds(new CubeColor(standbyCubeColor), cubeColorFadeTime);
            }
        }
        else if (Input.GetMouseButtonDown(1))
        {
            DeselectObstacle();
            HardwareInterface.Instance.FadeAllLeds(new CubeColor(standbyCubeColor), cubeColorFadeTime);
        }
    }

    private void DeselectObstacle()
    {
        //Deactivate old object
        if (selected != null)
        {
            selected.SetActiveObstacle(false);
        }
    }

    public void SetPlayerCanRotateObstacle(bool b)
    {
        playerCanRotateObstacles = b;
    }
}
