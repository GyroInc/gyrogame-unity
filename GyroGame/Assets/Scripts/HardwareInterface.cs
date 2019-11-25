//#define DEBUG

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO.Ports;
using System.Threading;
using System;
using System.Globalization;


//responsible for handling and maintaining connection to the Cube and interface functions
public class HardwareInterface : MonoBehaviour
{
    //there must only be one
    public static HardwareInterface active;

    public bool connectionEstablished;
    private bool abortConnect = false;
    private Vector3 orientation;
    public GameObject test;
    private Quaternion qRaw , q;
    private Quaternion qNull = Quaternion.identity;
    private Vector3 eulerNull;

    SerialPort port;
    Thread connectionHandler;
    Thread inputListener;

    Queue<string> messages = new Queue<string>();

    static int baudRate = 38400;

    private void Start()
    {
        active = this;

        connectionHandler = new Thread(OpenConnection);
        connectionHandler.Start();

        inputListener = new Thread(WaitForInput);
        inputListener.Start();
    }

    private void Update()
    {
        if (port == null) return;
        if (!port.IsOpen) return;

        if (messages.Count > 0)
        {
            string message = messages.Dequeue();
            Debug.Log("Incoming message\n" + message);
            if (message[0] == 'q')
            {
                message = message.TrimStart('q');
                var culture = (CultureInfo)CultureInfo.CurrentCulture.Clone();
                culture.NumberFormat.NumberDecimalSeparator = ".";
                string[] parts = message.Split('_');
                print("w" + parts[0] + " x" + parts[1] + " y" + parts[2] + " z" + parts[3]);
                messages.Clear();


                qRaw.w = -float.Parse(parts[0], culture);
                qRaw.x = float.Parse(parts[1], culture);
                qRaw.y = float.Parse(parts[3], culture);
                qRaw.z = -float.Parse(parts[2], culture);
                qRaw.Normalize();

                q = qNull * qRaw;
                //Vector3 euler = q.eulerAngles;
                //euler.z = -euler.z;



                //q.w = qRaw.w * qNull.w;
                //q.x = qRaw.x * qNull.x;
                //q.y = qRaw.y * qNull.y;
                //q.z = qRaw.z * qNull.z;

                //q = qRaw * Quaternion.Inverse(qNull);
                //q.Normalize();
                //test.transform.rotation = Quaternion.RotateTowards(test.transform.rotation, newQ, 1000*Time.deltaTime);

                //test.transform.rotation = Quaternion.Euler(euler);
                test.transform.rotation = qRaw;
                //Vector3 euler = test.transform.rotation.eulerAngles;
                //euler.x -= 90;
                //test.transform.rotation = Quaternion.Euler(euler);

                //print(qNull);

            }
        }

        HandleKeyInput();
    }

    void OpenConnection()
    {
        while (!abortConnect)
        {
#if DEBUG
            string[] ports = { "COM5" };
#else
            //connect to a cube by handshaking with the cube firmware
            string[] ports = SerialPort.GetPortNames();
            Debug.Log("Available ports: " + String.Join("   ",
             new List<string>(ports)
             .ConvertAll(i => i.ToString())
             .ToArray()));
#endif
            //cycle through all the com ports
            for (int i = 0; i < ports.Length; i++)
            {
                if (abortConnect) return;
                port = new SerialPort(ports[i], baudRate);
                port.ReadTimeout = 500;
                port.WriteTimeout = 500;
                try
                {
                    port.Open();
                    if (port.IsOpen)
                    {
                        if (testForCube())
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

    private void HandleKeyInput()
    {
        if (Input.GetKeyDown(KeyCode.N))
        {
            qNull = Quaternion.identity * Quaternion.Inverse(qRaw);
            qNull.Normalize();

            //eulerNull = qRaw.eulerAngles;

            //eulerNull.x = -1 * (float)Math.Round(eulerNull.x / 90.0) * 90;
            //eulerNull.y = -1 * (float)Math.Round(eulerNull.y / 90.0)  * 90;
            //eulerNull.z = -1 * (float)Math.Round(eulerNull.z / 90.0) * 90;

            //port.WriteLine("c");

        }
        if (Input.GetKeyDown(KeyCode.Space))
        {
            port.WriteLine("far0g0b0nt1000");
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

    bool testForCube()
    {
        port.WriteLine("cc");
        Thread.Sleep(20);
        string response = "";
        response = port.ReadLine();
        return response.Contains("y");

    }

    void WaitForInput()
    {
        while (!connectionEstablished) ;
        while (!abortConnect)
        {
            if (abortConnect || !port.IsOpen) return;

            if (port.BytesToRead > 3)
            {
                string message = "";
                while (port.BytesToRead > 0)
                {
                    char next = (char)port.ReadChar();
                    message += next;
                }
                messages.Enqueue(message);
            }
            Thread.Sleep(20);
        }
    }

    private void OnApplicationQuit()
    {
        abortConnect = true;

        if (port.IsOpen)
        {
            port.WriteLine("+DISC");
            port.Close();
        }

        if (connectionHandler != null) { connectionHandler.Abort(); }
    }
}
