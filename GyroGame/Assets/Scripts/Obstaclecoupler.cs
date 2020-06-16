using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]
public class Obstaclecoupler : MonoBehaviour
{
    public Transform observe;
    public Vector3 offset;
    
    void Update()
    {
        transform.rotation = Quaternion.Euler(offset) * observe.rotation;
    }
}
