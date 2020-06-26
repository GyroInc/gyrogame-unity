using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class PickupController : MonoBehaviour
{
    public bool setCubeColor, fadeCubeColor;
    public int fadeTime;
    public Color cubeColor;

    public UnityEvent cubePickupEvent;

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
            cubePickupEvent.Invoke();
            Destroy(gameObject);
        }
    }
}