using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO.Ports;

public class HardwareInterface : MonoBehaviour
{
    public static HardwareInterface active;
    SerialPort port = new SerialPort();

    public Vector3 cubeRot;

    private void Start()
    {
        active = this;
        string[] names = SerialPort.GetPortNames();

        for (int i = 0; i < names.Length; i++)
        {
            print(names[i]);
        }

        port.PortName = "COM4";
        port.BaudRate = 115200;
        port.ReadTimeout = 500;
        port.WriteTimeout = 500;
        port.Close();
        port.Open();
    }

    private void Update()
    {
        if(port.BytesToRead > 0)
        {
            string msg = "";
            while(port.BytesToRead > 0)
            {
                msg += port.ReadLine();
            }
            if (msg.Length < 5) return;
            string[] parts = msg.Split('-');
            float x, y, z;
            x = float.Parse(parts[0]);
            y = float.Parse(parts[1]);
            z = float.Parse(parts[2]);

            cubeRot = new Vector3(x, y, z);
        }
    }

    private void OnApplicationQuit()
    {
        port.Close();
    }
}
