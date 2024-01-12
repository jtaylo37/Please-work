// If "using System.IO.Ports" is marked with an error, follow the
// steps listed below it.

using System.IO.Ports;

// 1. Close Visual Studio.
// 2. In Unity, go to Edit/Project Settings..., pick Player, then set
//    API Compatibility Level to .NET 4.x.
// 3. Delete the .sln and .csproj files from your project's Assets folder.
// 4. Double-click this file in Unity to reopen it in Visual Studio.

using UnityEngine;

public class SerialIO : MonoBehaviour
{
    const int ReadTimeout = 100;
    const int BaudRate = 9600;
    const string SkipThisPort = "COM1";

    SerialPort sp;

    void Awake()
    {
        string comPort = "";

        foreach (string portName in SerialPort.GetPortNames())
        {
            if (portName != SkipThisPort)
            {
                comPort = portName;
                break;
            }
        }

        sp = new SerialPort(comPort, BaudRate);

        if (!sp.IsOpen)
        {
            sp.Open();
            sp.ReadTimeout = ReadTimeout;
        }
        else
        {
            Debug.Log("Serial port was already open.");
        }
    }

    public string Read()
    {
        if (sp.IsOpen && sp.BytesToRead > 0)
        {
            return sp.ReadLine();
        }

        return "";
    }

    public bool Write(byte b)
    {
        if (sp.IsOpen)
        {
            sp.SendByte(b);
            return true;
        }

        return false;
    }

    // Be sure to close the serial port before exiting, or it will
    // be busy the next time you try to upload code to the Adafruit.

    private void OnApplicationQuit()
    {
        if (sp.IsOpen)
        {
            sp.Close();
        }
    }
}

static class PortExtension
{
    static byte[] buff = new byte[1];
    public static void SendByte(this SerialPort sp, byte b)
    {
        buff[0] = b;

        sp.Write(buff, 0, 1);
    }
}
