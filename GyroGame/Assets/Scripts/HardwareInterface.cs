using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO.Ports;
using System.Threading;

//responsible for handling and maintaining connection to the Cube and interface functions
public class HardwareInterface : MonoBehaviour
{
    //there must only be one
    public static HardwareInterface active;

    public bool connectionEstablished;

    SerialPort port;
    Thread connectionHandler;
    bool abortConnect = false;

    static int baudRate = 57600;

    private void Start()
    {
        active = this;

        connectionHandler = new Thread(OpenConnection);
        connectionHandler.Start();
    }

    private void Update()
    {
        if (port == null) return;
        if (!port.IsOpen) return;

        if(Input.GetKeyDown(KeyCode.Space))
        {
            port.WriteLine("fbt1000");
        }

        if (Input.GetKeyDown(KeyCode.R))
        {
            port.WriteLine("far255g0b0t1000");
        }

        if (Input.GetKeyDown(KeyCode.G))
        {
            port.WriteLine("far0g255b0t1000");
        }

        if (Input.GetKeyDown(KeyCode.B))
        {
            port.WriteLine("far0g0b255t1000");
        }

        if (Input.GetKeyDown(KeyCode.S))
        {
            port.WriteLine("ar200g60b0");
        }
    }
    
    void OpenConnection()
    {
        while (!abortConnect)
        {
        //connect to a cube by handshaking with the second software
        string[] names = SerialPort.GetPortNames();

            //cycle through all the com ports
            for (int i = 0; i < names.Length; i++)
            {
                port = new SerialPort(names[i], baudRate);
                port.ReadTimeout = 100;
                port.WriteTimeout = 100;
                try
                {
                    port.Open();
                    if (port.IsOpen)
                    {
                        port.WriteLine("cc");
                        if (port.ReadLine().Contains("y"))
                        {
                            print("success at " + names[i]);
                            connectionEstablished = true;
                            port.WriteLine("ar0g0b0");
                        }
                        return;
                    }
                } catch { print(names[i] + " failed"); }
            }
        }
    }

    private void OnApplicationQuit()
    {
        if(port != null)
            port.Close();
        abortConnect = true;
        connectionHandler.Abort();
    }
}
