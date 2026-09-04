using System;
using System.Collections;
using System.Collections.Generic;
using System.IO.Ports;
using UnityEngine;

public class readDatafromSerialPort : MonoBehaviour
{


    [SerializeField] string[] ports = SerialPort.GetPortNames();
    int baudRate = 9600; // do not change
    SerialPort sp;
    SerialPort sp1 = null;
    SerialPort sp2 = null;
    string port1id;
    string port2id;

    public string player1data; // the data reseaved from port 1
    public string player2data; // the data reseaved from port 2
    [SerializeField] string Port1;
    [SerializeField] string Port2;
    public bool port1connected = false;
    public bool port2connected = false;
    public static Action<string> Player1Pressed;
    public static Action<string> Player2Pressed;

    public float portcheckTime = 0.5f;





    private void Start()
    {
       //sp1 = openport( "COM4");
       StartCoroutine(getPorts());
        

    }

    void Update()
    {

       



        if (sp1 != null && sp1.IsOpen)
        {

            StartCoroutine(ReadSerialportData(sp1, port1id, Port1));
        }
        if (sp2 != null && sp2.IsOpen)
        {
            StartCoroutine(ReadSerialportData(sp2, port2id, Port2));

        }




    }
    IEnumerator getPorts()
    {
        while (true)
        {
            Checkports();
            yield return new WaitForSeconds(portcheckTime);

        }



    }
    void Checkports()
    {
        ports = SerialPort.GetPortNames();
        if (ports.Length > 0)
        {

            if (sp1 == null || sp2 == null)
            {
                try
                {
                    foreach (string port in ports)
                    {
                        if (port == Port1 || port == Port2)
                            continue;

                        if (CheckIfController(port))
                        {
                            if (sp1 == null)
                            {
                                Port1 = port;
                                sp1 = openport(Port1);
                                port1connected = true;
                            }
                            else if (sp2 == null)
                            {
                                Port2 = port;
                                sp2 = openport(Port2);
                                port2connected = true;
                            }
                        }


                    }
                }
                catch
                {

                }
            }
        }
    }


    void OnApplicationQuit()
    {
        if (sp1 != null && sp1.IsOpen)
            sp1.Close();
        Debug.Log("close port");

        if (sp2 != null && sp2.IsOpen)
            sp2.Close();
        Debug.Log("close port");
    }
    IEnumerator ReadSerialportData(SerialPort port, string portid, string portname)
    {
        try
        {
            string dataport2 = " ";
            string dataport = " ";
            if (port.BytesToRead > 0)
            {

                dataport = port.ReadLine();
                portid = dataport;
                // Debug.Log($"dataport read 1: {dataport}");
            }



            if (dataport.Contains("|"))
            {
                string[] portsplit = dataport.Split("|");
                portid = portsplit[0];
                dataport2 = portsplit[1];
            }



            if (portid == "test1")
            {
                player1data = dataport2;
                Player1Pressed?.Invoke(player1data);
                //Debug.Log($"data from port 1 player 1: {player1data}");
            }
            else if (portid == "test2")
            {
                player2data = dataport2;
                Player2Pressed?.Invoke(player2data);

                // Debug.Log($"data from port1 player 2:  {player2data}");
            }



        }
        catch (System.TimeoutException ex)
        {
            Debug.Log("timeOut");
        }
        catch (System.Exception ex)
        {
            if (port.IsOpen)
            {
                Closeport(port, portname);
            }
            Debug.Log($"error reading from serial port: {ex} ");

        }
        yield return 0;
    }
    bool CheckIfController(string port)
    {
        try
        {
            using (sp = new SerialPort(port, baudRate))
            {
                sp.ReadTimeout = 200;
                sp.WriteTimeout = 200;
                sp.NewLine = "\n";

                sp.Open();

                sp.WriteLine("hello");

                string s = sp.ReadLine().Trim();
                sp.Close();
                // sp.Dispose();

                if (s == "hello")
                {

                    return true;
                }
                else
                {
                    return false;
                }
            }
        }
        catch (TimeoutException)
        {
            return false;
            // This port didn't respond.
        }
        catch (Exception ex)
        {
            Debug.LogWarning($"Could not use {port}: {ex.Message}");
            return false;
        }

    }
    SerialPort openport(string portname)
    {


        try
        {

            SerialPort port = new SerialPort(portname, baudRate);
            port.Open();
            port.ReadTimeout = 200;
           
            return port;
        }
        catch (TimeoutException)
        {
            // This port didn't respond.
            return null;
        }
        catch (Exception ex)
        {
            Debug.LogWarning($"Could not open port: {ex.Message}");
            return null;
        }

    }
    void Closeport(SerialPort port, string portname)
    {
        try
        {
            port.Close();
            port = null;
            portname = "";
            if (!sp1.IsOpen)
            {
                port1connected = false;
                sp1 = null;
                Port1 = null;
            }
            if (!sp2.IsOpen)
            {
                port2connected = false;
                sp2 = null;
                Port2 = null;
            }
        }
        catch (System.TimeoutException ex)
        {

        }
        catch (System.Exception ex)
        {



        }
    }
}
