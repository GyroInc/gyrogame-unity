using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstacleMover : MonoBehaviour
{
    public GameObject selected;
    public float selectionRange;

    public float rotationInterpolation = 0.2f;

    Color orgColor;
    Color targetColor;

    bool selectionActive = false;

    Quaternion offset;

    private void Start()
    {

    }

    void Update()
    {
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
                    offset = Quaternion.Inverse(selected.transform.parent.rotation);

                    //color stuff
                    orgColor = selected.transform.GetComponent<MeshRenderer>().material.color;
                    targetColor = Color.green;
                    HardwareInterface.active.FadeAllLeds(CubeColor.green, 1000);
                    selectionActive = true;
                }
                else
                {
                    targetColor = orgColor;
                    HardwareInterface.active.FadeAllLeds(CubeColor.orange, 1000);
                    selectionActive = false;
                }
            }
            else
            {
                targetColor = orgColor;
                HardwareInterface.active.FadeAllLeds(CubeColor.orange, 1000);
                selectionActive = false;
            }
        }

        if(selectionActive)
        {
            if(HardwareInterface.active.IsConnected())
            {
                selected.transform.parent.rotation = Quaternion.Slerp(selected.transform.parent.rotation, HardwareInterface.active.GetRotation(), rotationInterpolation);
            }
        }

        if(selected != null)
        {
            //lerp colors
            selected.transform.GetComponent<MeshRenderer>().material.color = Color.Lerp(selected.transform.GetComponent<MeshRenderer>().material.color, targetColor, Time.deltaTime);
        }
    }
}
