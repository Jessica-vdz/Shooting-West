using JetBrains.Annotations;
using System;
using System.Collections;
using System.IO.Ports;
using System.Web;
using Unity.VisualScripting;
using UnityEditor.Experimental.GraphView;
using UnityEngine;
using Microsoft.Win32;

public class readDatafromSerialPort : MonoBehaviour
{

    [SerializeField] string[] portNames;

    int baudRate = 9600;
    SerialPort sp;
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
    int i = 0;


    private void Start()
    {
        string[] ports = SerialPort.GetPortNames();

        foreach (string port in ports)
        {
            sp = new SerialPort(port, baudRate);
            sp.Open();
            sp.ReadTimeout = 500;
            sp.WriteTimeout = 500;
            sp.WriteLine("hello");
            string s = sp.ReadLine();
            if (s == "hello")
            {

                portNames[i] = s;
                i++;
            }
        }
        // GetArduinoPort(portNames);

    }

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
    public void GetArduinoPort(string[] portNames)
    {
        // Veelvoorkomende chipsets en namen van Arduino's
        string[] targetKeywords = { "Arduino", "CH340", "CP210", "FTDI", "USB Serial" };
        int i = 0;
        // Pad in het Windows-register waar USB/Seriële apparaten worden gekoppeld
        string registryPath = @"SYSTEM\CurrentControlSet\Control\COM Name Arbiter\Devices";

        try
        {
            using (RegistryKey key = Registry.LocalMachine.OpenSubKey(registryPath))
            {
                if (key != null)
                {
                    foreach (string valueName in key.GetValueNames())
                    {
                        // valueName bevat vaak de hardware-omschrijving of ID
                        foreach (string keyword in targetKeywords)
                        {
                            if (valueName.IndexOf(keyword, StringComparison.OrdinalIgnoreCase) >= 0)
                            {
                                
                                

                                 portNames[i] =   key.GetValue(valueName).ToString();
                                i++;
                                // Geeft de COM-poort terug (bijv. "COM3")
                            }
                        }
                    }
                }
            }
        }
        catch (Exception e)
        {
            Debug.LogError($"Fout bij lezen register: {e.Message}");
        }

        // Alternatieve fallback: als het register niets vindt, pak de eerste beschikbare poort
        string[] ports = SerialPort.GetPortNames();
        if (ports.Length > 0)
        {
            Debug.LogWarning($"Keywords niet gevonden. Fallback naar eerste poort: {ports[0]}");
           
        }

        
    }
}
