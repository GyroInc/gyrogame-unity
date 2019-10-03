using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestScript : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        //rotate obj
        transform.Rotate(new Vector3(0.5f, 0.5f, 0.5f) * Time.deltaTime * 50);
    }
}
