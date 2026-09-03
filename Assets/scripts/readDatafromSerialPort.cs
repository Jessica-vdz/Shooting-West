using JetBrains.Annotations;
using System;
using System.Collections;
using System.IO.Ports;
using System.Web;
using Unity.VisualScripting;
using UnityEditor.Experimental.GraphView;
using UnityEngine;

public class readDatafromSerialPort : MonoBehaviour
{

    [SerializeField] string[] portNames = SerialPort.GetPortNames();

    int baudRate = 9600;
    SerialPort sp1;
    SerialPort sp2;
    string port1id; 
    string port2id;
   
    public string player1data; // the data reseaved from port 1
    public string player2data; // the data reseaved from port 2
    string Port1;
    string Port2;
    bool checkport = true;
    public float portcheckTime = 0.5f;
    string dataport;
    string dataport2;


    //private void Start()
    //{

    //portNames = SerialPort.GetPortNames();
    //string Port1 = portNames[0];
    // Port2 = portNames[1];

    // Debug.Log($"{Port1}");
    // Debug.Log($"{Port2}");



    //sp1 = new SerialPort(Port1, baudRate);
    //sp1.Open();
    //sp1.ReadTimeout = 500;
    //}

    void Update()
    {
        if(portNames.Length < 2)
        {
         StartCoroutine(getPorts());

        }



        if (portNames.Length > 1 && checkport)
        {
            string Port1 = portNames[0];
             Port2 = portNames[1];

            Debug.Log($"{Port1}");
            Debug.Log($"{Port2}");



            sp1 = new SerialPort(Port1, baudRate);
            sp1.Open();
            sp1.ReadTimeout = 500;
            sp2 = new SerialPort(Port2, baudRate);
            sp2.Open();
            sp2.ReadTimeout = 300;
            checkport = false;
            StopCoroutine(getPorts());

        }
        else if (checkport == false)
        {
            StartCoroutine(ReadSerialportData(sp1,port1id));
            StartCoroutine(ReadSerialportData(sp2,port2id));

        }
    }
    IEnumerator getPorts()
    {
        yield return new WaitForSeconds(portcheckTime);
        portNames = SerialPort.GetPortNames();
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
    IEnumerator ReadSerialportData(SerialPort port, string portid )
    {
        try
        {
            
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

                //Debug.Log($"data from port 1 player 1: {player1data}");
            }
            else if (portid == "test2")
            {
                player2data = dataport2;

                // Debug.Log($"data from port1 player 2:  {player2data}");
            }



        }
        catch (System.TimeoutException ex)
        {

        }
        catch (System.Exception ex)
        {
            Debug.Log($"error reading from serial port: {ex} ");
        }
        yield return 0;
    }

}
