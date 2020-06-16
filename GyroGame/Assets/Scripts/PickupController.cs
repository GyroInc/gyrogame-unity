using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickupController : MonoBehaviour
{
    public GameObject pickupDialogue;

    void Start()
    {
        
    }

    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag != "Player") return;
        pickupDialogue.SetActive(true);
        
    }

    private void OnTriggerStay(Collider other)
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            HardwareInterface.Instance.FadeAllLeds(CubeColor.orange, 1000);
            pickupDialogue.SetActive(false);
            Destroy(this.gameObject);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.tag != "Player") return;
        pickupDialogue.SetActive(false);
    }
}
