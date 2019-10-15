using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO.Ports;
using System.Threading;
using System;

//responsible for handling and maintaining connection to the Cube and interface functions
public class HardwareInterface : MonoBehaviour
{
    //there must only be one
    public static HardwareInterface active;

    public bool connectionEstablished;

    SerialPort port;
    Thread connectionHandler;
    bool abortConnect = false;

    static int baudRate = 38400;

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

        if (Input.GetKeyDown(KeyCode.Space))
        {
            port.WriteLine("far0g0b0t1000");
        }

        if (Input.GetKeyDown(KeyCode.R))
        {
            port.WriteLine("far255g100b0t1000");
        }

        if (Input.GetKeyDown(KeyCode.G))
        {
            port.WriteLine("farg255b100t1000");
        }

        if (Input.GetKeyDown(KeyCode.B))
        {
            port.WriteLine("far100g0b255t1000");
        }

        if (Input.GetKeyDown(KeyCode.Q))
        {
            port.WriteLine("ar255g0b0");
        }
        if (Input.GetKeyDown(KeyCode.W))
        {
            port.WriteLine("ar0g255b0");
        }
        if (Input.GetKeyDown(KeyCode.E))
        {
            port.WriteLine("ar0g0b255");
        }

        if (Input.GetKeyDown(KeyCode.I))
        {
            port.WriteLine("b64");
        }
        if (Input.GetKeyDown(KeyCode.O))
        {
            port.WriteLine("b220");
        }

        if (Input.GetKeyDown(KeyCode.Y))
        {
            port.WriteLine("fo0r0g255b100t1000");
        }
        if (Input.GetKeyDown(KeyCode.X))
        {
            port.WriteLine("fo1r0g10b100t1000");
        }
        if (Input.GetKeyDown(KeyCode.C))
        {
            port.WriteLine("fo2r80g255b100t1000");
        }
        if (Input.GetKeyDown(KeyCode.V))
        {
            port.WriteLine("fo3r140g255b0t1000");
        }
    }

    void OpenConnection()
    {
        while (!abortConnect)
        {
            //connect to a cube by handshaking with the second software
            string[] ports = SerialPort.GetPortNames();
            Debug.Log("Available ports: "+ String.Join("   ",
             new List<string>(ports)
             .ConvertAll(i => i.ToString())
             .ToArray()));
            //cycle through all the com ports
            for (int i = 0; i < ports.Length; i++)
            {
                port = new SerialPort(ports[i], baudRate);
                port.ReadTimeout = 200;
                port.WriteTimeout = 200;
                try
                {
                    port.Open();
                    if (port.IsOpen)
                    {
                        port.WriteLine("cc");
                        string response = port.ReadLine();
                        if (response.Contains("y"))
                        {
                            print(ports[i] + ": success");
                            connectionEstablished = true;
                            port.WriteLine("ar0g0b0");
                            return;
                        }
                        port.Close();
                        return;
                    }
                }
                catch { print(ports[i] + ": failed"); }
            }
        }
    }

    private void OnApplicationQuit()
    {
        if (port.IsOpen)
        {
            port.WriteLine("+DISC");
            port.Close();
        }

        abortConnect = true;
        connectionHandler.Abort();
    }
}
