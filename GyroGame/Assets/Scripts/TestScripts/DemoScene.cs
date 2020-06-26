using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DemoScene : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        //HardwareInterface.active.Connect();
        HardwareInterface.Instance.AddCubeConnectedAction(OnCubeConnected);
    }
    
    void OnCubeConnected()
    {
        HardwareInterface.Instance.FadeAllLeds(CubeColor.orange, 1000);
    }

    // Update is called once per frame
    void Update()
    {
        if(HardwareInterface.Instance.IsConnected())
            Physics.gravity = HardwareInterface.Instance.GetRotation() * -Vector3.up * 9.81f;

        if(Mathf.Abs( Physics.gravity.x) > Mathf.Abs(Physics.gravity.y) && Mathf.Abs(Physics.gravity.x) > Mathf.Abs(Physics.gravity.z))
        {
            Physics.gravity = new Vector3(Physics.gravity.x, 0, 0);
        }
        if (Mathf.Abs(Physics.gravity.y) > Mathf.Abs(Physics.gravity.x) && Mathf.Abs(Physics.gravity.y) > Mathf.Abs(Physics.gravity.z))
        {
            Physics.gravity = new Vector3(0, Physics.gravity.y, 0);
        }
        if (Mathf.Abs(Physics.gravity.z) > Mathf.Abs(Physics.gravity.y) && Mathf.Abs(Physics.gravity.z) > Mathf.Abs(Physics.gravity.x))
        {
            Physics.gravity = new Vector3(0, 0, Physics.gravity.z);
        }
    }
}
