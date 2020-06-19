using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO.Ports;
using System.Security.Permissions;
using System.Threading;
using UnityEditor;
using UnityEngine;


//responsible for handling and maintaining connection to the Cube and interface functions
public class HardwareInterface : MonoBehaviour
{
    public static HardwareInterface Instance { get; private set; }

    [Header("Preferences")]
    public int baudRate = 38400;
    public int defaultBrightness = 128;
    public float lowBatteryVoltage = 3;
    private float connectionTimeout = 6.5f;


    [Header("Debug Settings")]
    public bool fixedCOMPort;
    public string debugCOMPort;
    public bool debugConnectionAttempt, debugMessages;

    [Header("Status Information")]
    [ReadOnly] public bool connected;
    [ReadOnly] public bool batteryWarning;
    [ReadOnly] public float voltage;
    [ReadOnly] public Quaternion cubeRotation;

    private int brightness;
    private bool connectionAttempt;
    private float cubeTimeoutTimer;

    private Thread connectionHandlerThread;
    private Thread communicationHandlerThread;

    private SerialPort port;
    Queue<string> inMessages = new Queue<string>();
    Queue<string> outMessages = new Queue<string>();

    private event CubeStatusChangeHandler CubeConnectedEvent;
    private event CubeStatusChangeHandler CubeDisconnectedEvent;

    public delegate void CubeStatusChangeHandler();


    private void Start()
    {
        Instance = this;
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
                Debug.LogError("Too many messages from cube! Skipping some...");
                inMessages.Clear();
            }
            var culture = (CultureInfo)CultureInfo.CurrentCulture.Clone();
            culture.NumberFormat.NumberDecimalSeparator = ".";

            switch (message[0])
            {
                case 'q':
                    message = message.TrimStart('q');
                    string[] parts = message.Split('_');
                    cubeRotation.w = -float.Parse(parts[0], culture);
                    cubeRotation.x = float.Parse(parts[1], culture);
                    cubeRotation.y = float.Parse(parts[3], culture);
                    cubeRotation.z = float.Parse(parts[2], culture);
                    cubeRotation.Normalize();
                    break;
                case 'v':
                    message = message.TrimStart('v');
                    message = message.Insert(1, ".");
                    if (debugMessages) Debug.Log("Incoming voltage:\n" + message + "V");
                    voltage = float.Parse(message, culture);
                    if (voltage < lowBatteryVoltage) batteryWarning = true; else batteryWarning = false;
                    cubeTimeoutTimer = 0f;
                    break;
                default:
                    Debug.LogError("Unhandled incoming message from Cube\n" + message);
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
            if (connectionHandlerThread != null) { connectionHandlerThread.Abort(); }

            ResetConnection();

            if (port != null)
            {
                if (port.IsOpen)
                {
                    SendSpamCommand("+DISC");
                    port.Close();
                }
            }
            Debug.Log("Cube disconnected");
            CubeDisconnectedEvent?.Invoke();

            if (communicationHandlerThread != null) { communicationHandlerThread.Abort(); }

        }
        else
        {
            Debug.Log("Unable to disconnect from Cube!\nCube not connected.");
        }
    }


    [SecurityPermissionAttribute(SecurityAction.Demand, ControlThread = true)]
    public void CancelConnectionAttempt()
    {
        ResetConnection();

        if (connectionHandlerThread != null) { connectionHandlerThread.Abort(); }
        if (communicationHandlerThread != null) { communicationHandlerThread.Abort(); }

        if (port != null)
        {
            if (port.IsOpen)
            {
                SendSpamCommand("+DISC");
            }
            port.Close();
        }
        Debug.Log("Connection attempt cancelled");
    }

    public void SendCommand(string message)
    {
        if (debugMessages) Debug.Log("Sending command to cube:\n" + message);
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

    public bool isAttemptingConnection()
    {
        return connectionAttempt;
    }

    public Quaternion GetRotation()
    {
        return cubeRotation;
    }

    public float GetVoltage()
    {
        return voltage;
    }

    public int GetLedBrightness()
    {
        return brightness;
    }

    public void AddCubeConnectedAction(Action method)
    {
        CubeConnectedEvent += new CubeStatusChangeHandler(method);
    }

    public void AddCubeDisconnectedAction(Action method)
    {
        CubeDisconnectedEvent += new CubeStatusChangeHandler(method);
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
                    if(debugConnectionAttempt) Debug.Log("trying " + ports[i]);
                    port.Open();
                }
                catch
                {
                    port.Close();
                    if (debugConnectionAttempt) Debug.Log(ports[i] + ": failed");
                    continue;
                }
                if (port.IsOpen)
                {
                    if (TestForCube())
                    {
                        if (debugConnectionAttempt) Debug.Log("Cube connected successfully at " + ports[i]);
                        SetAllLeds(CubeColor.black);
                        SetLedBrightness(defaultBrightness);
                        connected = true;
                        connectionAttempt = false;
                        CubeConnectedEvent?.Invoke();
                        communicationHandlerThread = new Thread(T_SendReceive);
                        communicationHandlerThread.Start();
                        return;
                    }
                }
            }
            port.Close();
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
                SendSpamCommand(outMessages.Dequeue());
            }
            if (cubeTimeoutTimer > connectionTimeout)
            {
                Debug.Log("Cube connection timed out!");
                cubeTimeoutTimer = 0f;
                Disconnect();
            }
            Thread.Sleep(5);
        }
    }

    private bool TestForCube()
    {
        if (debugConnectionAttempt) Debug.Log("Sending confirmation message");
        try
        {
            port.WriteLine("cc");
            port.DiscardInBuffer();
            string response = port.ReadLine();
            if (debugConnectionAttempt) Debug.Log("Confirmation Response: " + response);
            return response.Contains("y");
        }
        catch
        {
            Debug.Log("Confirmation timeout");
            return false;
        }


    }

    private void SendSpamCommand(string command)
    {
        for (int i = 0; i < 3; i++)
        {
            port.WriteLine(command);
        }
    }

    private void ResetConnection()
    {
        connected = false;
        connectionAttempt = false;
        cubeTimeoutTimer = 0f;
        inMessages.Clear();
        outMessages.Clear();
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

public class ReadOnlyAttribute : PropertyAttribute
{

}

#if UNITY_EDITOR
[CustomPropertyDrawer(typeof(ReadOnlyAttribute))]
public class ReadOnlyDrawer : PropertyDrawer
{
    public override float GetPropertyHeight(SerializedProperty property,
                                            GUIContent label)
    {
        return EditorGUI.GetPropertyHeight(property, label, true);
    }

    public override void OnGUI(Rect position,
                               SerializedProperty property,
                               GUIContent label)
    {
        GUI.enabled = false;
        EditorGUI.PropertyField(position, property, label, true);
        GUI.enabled = true;
    }
}
#endif