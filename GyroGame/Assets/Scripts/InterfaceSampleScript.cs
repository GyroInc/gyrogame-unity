using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InterfaceSampleScript : MonoBehaviour
{

    public GameObject Cube1;
    public GameObject Cube2;

    private Quaternion qNull = Quaternion.identity;

    // Start is called before the first frame update
    void Start()
    {
        HardwareInterface.active.Connect();
    }

    // Update is called once per frame
    void Update()
    {
        Cube1.transform.rotation = HardwareInterface.active.cubeRotation;
        Cube2.transform.rotation = HardwareInterface.active.cubeRotation * qNull;

        HandleKeyInput();
    }

    private void HandleKeyInput()
    {
        if (Input.GetKeyDown(KeyCode.N))
        {
            qNull = Quaternion.Inverse(HardwareInterface.active.cubeRotation);

            //port.WriteLine("c");

        }
        if (Input.GetKeyDown(KeyCode.Space))
        {
            HardwareInterface.active.SendCommand("far0g0b0nt1000");
        }

        if (Input.GetKeyDown(KeyCode.R))
        {
            HardwareInterface.active.SendCommand("far255g100b0t1000");
        }

        if (Input.GetKeyDown(KeyCode.D))
        {
            HardwareInterface.active.Disconnect();
        }

        if (Input.GetKeyDown(KeyCode.G))
        {
            //port.WriteLine("farg255b100t1000");
        }

        if (Input.GetKeyDown(KeyCode.B))
        {
            //port.WriteLine("far100g0b255t1000");
        }

        if (Input.GetKeyDown(KeyCode.Q))
        {
            //port.WriteLine("ar255g0b0");
        }
        if (Input.GetKeyDown(KeyCode.W))
        {
            //port.WriteLine("ar0g255b0");
        }
        if (Input.GetKeyDown(KeyCode.E))
        {
            //port.WriteLine("ar0g0b255");
        }

        if (Input.GetKeyDown(KeyCode.I))
        {
            //port.WriteLine("b64");
        }
        if (Input.GetKeyDown(KeyCode.O))
        {
            //port.WriteLine("b220");
        }

        if (Input.GetKeyDown(KeyCode.Y))
        {
            //port.WriteLine("fo0r0g255b100t1000");
        }
        if (Input.GetKeyDown(KeyCode.X))
        {
            //port.WriteLine("fo1r0g10b100t1000");
        }
        if (Input.GetKeyDown(KeyCode.C))
        {
            //port.WriteLine("fo2r80g255b100t1000");
        }
        if (Input.GetKeyDown(KeyCode.V))
        {
            //port.WriteLine("fo3r140g255b0t1000");
        }
    }
}
