using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO.Ports;
using System.Security.Permissions;
using System.Threading;
using UnityEngine;


//responsible for handling and maintaining connection to the Cube and interface functions
public class HardwareInterface : MonoBehaviour
{
    //there must only be one
    public static HardwareInterface active;

    [Header("Preferences")]
    public int baudRate = 38400;
    public int defaultBrightness;
    public float connectionTimeout;

    [Header("Debug Settings")]
    public bool debugQuaternion;
    public bool fixedCOMPort;
    public string debugCOMPort;

    [Header("Status Information")]
    public bool connected;
    public Quaternion cubeRotation;

    private int voltage, brightness;
    private bool connectionAttempt;
    private float cubeTimeoutTimer;

    private Thread connectionHandlerThread;
    private Thread communicationHandlerThread;

    private SerialPort port;
    Queue<string> inMessages = new Queue<string>();
    Queue<string> outMessages = new Queue<string>();

    private void Start()
    {
        active = this;
    }

    private void Update()
    {
        if (port == null) return;
        if (!port.IsOpen || !connected) return;
        cubeTimeoutTimer += Time.deltaTime;

        if (inMessages.Count > 0)
        {
            string message = inMessages.Dequeue();
            if (inMessages.Count > 10)
            {
                Debug.Log("Too many messages from cube! Skipping some...");
                inMessages.Clear();
            }
            switch (message[0])
            {
                case 'q':
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
                    break;
                case 'v':
                    Debug.Log("Incoming voltage:\n" + message);
                    message = message.TrimStart('v');
                    voltage = int.Parse(message);
                    cubeTimeoutTimer = 0f;
                    break;
                default:
                    Debug.Log("Unhandled incoming message from Cube\n" + message);
                    break;
            }
        }
    }

    /* ################################
     * ####### Public Methods #########
     * ################################ */

    public void Connect()
    {
        if (!connected)
        {
            if (!connectionAttempt)
            {
                Debug.Log("Connecting to Cube...");
                connectionAttempt = true;
                connectionHandlerThread = new Thread(T_OpenConnection);
                connectionHandlerThread.Start();
            }
            else
            {
                Debug.Log("Unable to connect to Cube!\nConnection attempt in progress.");
            }
        }
        else
        {
            Debug.Log("Unable to connect to Cube!\nCube already connected.");
        }
    }

    [SecurityPermissionAttribute(SecurityAction.Demand, ControlThread = true)]
    public void Disconnect()
    {
        if (connected)
        {
            connected = false;
            connectionAttempt = false;

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
        else
        {
            Debug.Log("Unable to disconnect from Cube!\nCube not connected.");
        }

    }

    [SecurityPermissionAttribute(SecurityAction.Demand, ControlThread = true)]
    public void CancelConnectionAttempt()
    {
        connected = false;
        connectionAttempt = false;

        if (connectionHandlerThread != null) { connectionHandlerThread.Abort(); }
        if (communicationHandlerThread != null) { communicationHandlerThread.Abort(); }

        if (port != null)
        {
            if (port.IsOpen)
            {
                port.WriteLine("+DISC");
            }
            port.Close();
            Debug.Log("Port closed");
        }
        Debug.Log("Connection attempt cancelled");
    }

    public void SendCommand(string message)
    {
        Debug.Log("Sending command:\n" + message);
        outMessages.Enqueue(message);
    }

    public void SetAllLeds(CubeColor c)
    {
        SendCommand("ar" + c.r + "g" + c.g + "b" + c.b);
    }
    public void FadeAllLeds(CubeColor c, int t)
    {
        SendCommand("far" + c.r + "g" + c.g + "b" + c.b + "t" + t);
    }

    public void SetLed(int f, CubeColor c)
    {
        SendCommand("l" + f + "r" + c.r + "g" + c.g + "b" + c.b);

    }
    public void FadeLed(int f, CubeColor c, int t)
    {
        SendCommand("fo" + f + "r" + c.r + "g" + c.g + "b" + c.b + "t" + t);
    }

    public void SetLedBrightness(int b)
    {
        brightness = b;
        SendCommand("b" + b);
    }

    public void IncreaseLedsBrightness()
    {
        brightness = brightness + 20;
        if (brightness > 200) brightness = 200;
        SetLedBrightness(brightness);
    }

    public void DecreaseLedsBrightness()
    {
        brightness = brightness - 20;
        if (brightness < 20) brightness = 20;
        SetLedBrightness(brightness);
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

    public int GetLedBrightness()
    {
        return brightness;
    }

    /* ################################
     * ####### Private Methods ########
     * ################################ */

    private void T_OpenConnection()
    {
        while (connectionAttempt)
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
                if (!connectionAttempt) return;
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
                            SetAllLeds(CubeColor.black);
                            SetLedBrightness(defaultBrightness);
                            connected = true;
                            connectionAttempt = false;
                            communicationHandlerThread = new Thread(T_SendReceive);
                            communicationHandlerThread.Start();
                            return;
                        }
                        port.Close();
                    }
                    port.Close();
                }
                catch
                {
                    port.Close();
                    Debug.Log(ports[i] + ": failed");
                }
            }
        }
    }

    private void T_SendReceive()
    {
        while (connected && port.IsOpen)
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
            if (cubeTimeoutTimer > connectionTimeout)
            {
                port.Close();
                Debug.Log("Cube connection timed out!");
                cubeTimeoutTimer = 0f;
                Disconnect();
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
        if (connected)
        {
            Disconnect();
        }
        else if (connectionAttempt)
        {
            CancelConnectionAttempt();
        }
    }
}
