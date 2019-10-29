using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MechanicTestController : MonoBehaviour
{
    public Transform player;
    public Transform respawn;

    void Start()
    {
        
    }
    

    void Update()
    {
        
    }


    private void OnTriggerExit(Collider other)
    {
        if(other.tag == "Player")
        {
            //player left bounds, reset
            other.transform.position = respawn.position;
        }
    }
}
