using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstacleMover : MonoBehaviour
{
    public GameObject selected;
    public float selectionRange;

    public float rotationInterpolation = 0.2f;
    public float angleSnap;

    public Color unselectedCubeColor;
    public Color unselectedColor;
    public Color selectedColor;

    bool selectionActive = false;

    Quaternion offset;
    bool offsetSet = false;

    private void Start()
    {
        //HardwareInterface.active.Connect();
        HardwareInterface.Instance.AddCubeConnectedAction(OnCubeConnected);
    }

    void OnCubeConnected()
    {
        HardwareInterface.Instance.FadeAllLeds(CubeColor.black, 1000);
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
                    if(!offsetSet)
                    {
                        offset = selected.transform.parent.rotation * Quaternion.Euler(Vector3.right);
                        offsetSet = true;
                    }
                        

                    //color stuff
                    HardwareInterface.Instance.FadeAllLeds(new CubeColor(selectedColor), 1000);
                    selectionActive = true;
                }
                else
                {
                    HardwareInterface.Instance.FadeAllLeds(new CubeColor(unselectedCubeColor), 1000);
                    selectionActive = false;
                    offsetSet = false;
                }
            }
            else
            {
                HardwareInterface.Instance.FadeAllLeds(new CubeColor(unselectedCubeColor), 1000);
                selectionActive = false;
                offsetSet = false;
            }
        }

        if(selectionActive)
        {
            if(HardwareInterface.Instance.IsConnected())
            {
                //snap angles
                Quaternion inputAngle = HardwareInterface.Instance.GetRotation() * offset; //OffsetTestScript.active.correctedCube * offset;
                Quaternion outputAngle = inputAngle;
                float x = Mathf.RoundToInt((inputAngle.eulerAngles.x / 360f) * 4f) * 90;
                float y = Mathf.RoundToInt((inputAngle.eulerAngles.y / 360f) * 4f) * 90;
                float z = Mathf.RoundToInt((inputAngle.eulerAngles.z / 360f) * 4f) * 90;
                Quaternion clippedAngle = Quaternion.Euler(x, y, z);
                if (Mathf.Abs(inputAngle.eulerAngles.y) % 90 > 90 - angleSnap || Mathf.Abs(inputAngle.eulerAngles.y) % 90 < angleSnap)
                    outputAngle = clippedAngle;

                selected.transform.parent.rotation = Quaternion.Slerp(selected.transform.parent.rotation, outputAngle, rotationInterpolation);
            }
        }

        if(selected != null)
        {
            //lerp colors
            if(selectionActive)
                selected.transform.GetComponent<MeshRenderer>().material.color = Color.Lerp(selected.transform.GetComponent<MeshRenderer>().material.color, selectedColor, Time.deltaTime * 3);
            else
                selected.transform.GetComponent<MeshRenderer>().material.color = Color.Lerp(selected.transform.GetComponent<MeshRenderer>().material.color, unselectedColor, Time.deltaTime * 3);
        }
    }
}
