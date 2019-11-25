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

    public int baudRate = 38400;
    public bool debugQuaternion;
    public bool fixedCOMPort;
    public string debugCOMPort;

    private Thread connectionHandlerThread;
    private Thread communicationHandlerThread;

    private SerialPort port;
    private bool abortConnect = false;
    Queue<string> inMessages = new Queue<string>();
    Queue<string> outMessages = new Queue<string>();

    public Quaternion cubeRotation;
    public int voltage;
    public bool connected;

    private void Start()
    {
        active = this;
    }

    private void Update()
    {
        if (port == null) return;
        if (!port.IsOpen || !connected) return;

        if (inMessages.Count > 0)
        {
            string message = inMessages.Dequeue();
            if (inMessages.Count > 10)
            {
                Debug.Log("Too many messages from cube! Skipping some...");
                inMessages.Clear();
            }
            if (message[0] == 'q')
            {
                message = message.TrimStart('q');
                var culture = (CultureInfo)CultureInfo.CurrentCulture.Clone();
                culture.NumberFormat.NumberDecimalSeparator = ".";
                string[] parts = message.Split('_');
                if (debugQuaternion)
                {
                    Debug.Log("Incoming Quaternion:\nw" + parts[0] + " x" + parts[1] + " y" + parts[2] + " z" + parts[3]);
                }
                cubeRotation.w = -float.Parse(parts[0], culture);
                cubeRotation.x = float.Parse(parts[1], culture);
                cubeRotation.y = float.Parse(parts[3], culture);
                cubeRotation.z = float.Parse(parts[2], culture);
                cubeRotation.Normalize();

            }
            else
            {
                Debug.Log("Incoming message from Cube\n" + message);
            }
        }
    }

    public void Connect()
    {
        connectionHandlerThread = new Thread(T_OpenConnection);
        connectionHandlerThread.Start();
    }

    public void Disconnect()
    {
        abortConnect = true;
        connected = false;

        if (connectionHandlerThread != null) { connectionHandlerThread.Abort(); }
        if (communicationHandlerThread != null) { communicationHandlerThread.Abort(); }

        if (port != null)
        {
            if (port.IsOpen)
            {
                port.WriteLine("+DISC");
                port.Close();
            }
        }
        Debug.Log("Cube disconnected");
    }

    public void SendCommand(string message)
    {
        Debug.Log("Sending command\n" + message);
        outMessages.Enqueue(message);
    }

    private void T_OpenConnection()
    {
        while (!abortConnect)
        {
            string[] ports;
            if (fixedCOMPort)
            {
                ports = new string[1];
                ports[0] = debugCOMPort;
            }
            else
            {
                //connect to a cube by handshaking with the cube firmware
                ports = SerialPort.GetPortNames();
                Debug.Log("Available ports: " + String.Join("   ",
                 new List<string>(ports)
                 .ConvertAll(i => i.ToString())
                 .ToArray()));
            }

            //cycle through all the com ports
            for (int i = 0; i < ports.Length; i++)
            {
                if (abortConnect) return;
                port = new SerialPort(ports[i], baudRate);
                port.ReadTimeout = 100;
                port.WriteTimeout = 100;
                try
                {
                    port.Open();
                    if (port.IsOpen)
                    {
                        if (TestForCube())
                        {
                            print(ports[i] + "\nconnected successfully");
                            port.WriteLine("ar0g0b0");
                            connected = true;
                            communicationHandlerThread = new Thread(T_SendReceive);
                            communicationHandlerThread.Start();
                            return;
                        }
                        port.Close();
                    }
                }
                catch { Debug.Log(ports[i] + ": failed"); }
            }
        }
    }

    private void T_SendReceive()
    {
        while (connected && !abortConnect && port.IsOpen)
        {
            if (port.BytesToRead > 0)
            {
                string message = port.ReadLine();
                inMessages.Enqueue(message);
            }
            if (outMessages.Count > 0)
            {
                port.WriteLine(outMessages.Dequeue());
            }

            Thread.Sleep(10);
        }
    }

    private bool TestForCube()
    {
        port.WriteLine("cc");
        Thread.Sleep(20);
        if (port.BytesToRead > 0)
        {
            string response = port.ReadLine();
            return response.Contains("y");
        }
        return false;
    }

    private void OnApplicationQuit()
    {
        Disconnect();
    }

    public bool IsConnected()
    {
        return connected;
    }

    public Quaternion GetRotation()
    {
        return cubeRotation;
    }

    public int GetVoltage()
    {
        return voltage;
    }
}
