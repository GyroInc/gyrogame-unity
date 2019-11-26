using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DemoScene : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        HardwareInterface.active.Connect();
        HardwareInterface.active.OnCubeConnected(OnCubeConnected);
    }
    
    void OnCubeConnected()
    {
        HardwareInterface.active.FadeAllLeds(CubeColor.black, 1000);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
