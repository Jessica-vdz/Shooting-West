using System;
using System.IO.Ports;
using System.Web;
using Unity.VisualScripting;
using UnityEditor.Experimental.GraphView;
using UnityEngine;

public class readDatafromEsp32 : MonoBehaviour
{
    string[] portNames = SerialPort.GetPortNames();
    int baudRate = 9600;
    SerialPort spPort1;
    SerialPort spPort2;
    private string port1id; // the id from port 1
    private string port2id; // the id from port 2
    public string player1data; // the data reseaved from port 1
    public string player2data; // the data reseaved from port 2
    public string Port1 = "COM2";

    void Start()
    {
        string porttest = portNames[0];
        Debug.Log($"{Port1}");
        Debug.Log($"{porttest}");
        // string Port2 = portNames[1];
        spPort1 = new SerialPort(Port1, baudRate);
        spPort1.Open();
        spPort1.ReadTimeout = 500;
        //spPort2 = new SerialPort(Port2, baudRate);
        //spPort2.Open();
        //spPort2.ReadTimeout = 500;
    }


    void Update()
    {

        readesp32data(spPort1, port1id);
        
    }

    void OnApplicationQuit()
    {
        if ( spPort1 != null && spPort1.IsOpen)
            spPort1.Close();
        Debug.Log("close port");

        if (spPort2 != null && spPort2.IsOpen)
            spPort2.Close();
        Debug.Log("close port");
    }
    void readesp32data(SerialPort port, string portid)
    {
        try
        {
            for (int i = 0; i < 2; i++)
            {
                string dataport = port.ReadLine();
                // string dataport1 = spPort1.ReadExisting();
                if (!string.IsNullOrEmpty(dataport))
                {
                    if (i == 0)
                    {
                        portid = dataport;
                        Debug.Log($"port1id {portid}");
                    }
                    if (i == 1)
                    {
                        if (portid == "test1")
                        {
                            player1data = dataport;
                            Debug.Log($"data from port 1 player 1: {player1data}");
                        }
                        else if (portid == "test2")
                        {
                            player2data = dataport;
                            Debug.Log($"data from port1 player 2:  {player2data}");
                        }
                    }

                }
                else
                {
                    Debug.Log($"no data reseaved {i}");
                }


            }




        }
        catch (System.TimeoutException ex)
        {

        }
        catch (System.Exception ex)
        {
            Debug.Log($"error reading from serial port: {ex} ");
        }
    }
}
