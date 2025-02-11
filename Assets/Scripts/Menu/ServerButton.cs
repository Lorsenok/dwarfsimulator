using System;
using System.Collections;
using System.Net;
using System.Net.Sockets;
using TMPro;
using Unity.Netcode;
using Unity.Netcode.Transports.UTP;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;
using UnityEngine.UIElements;

public class ServerButton : GameButton
{
    [Header("Server")]
    [SerializeField] private bool create;
    [SerializeField] private TMP_InputField ip;
    [SerializeField] private TMP_InputField port;
    [SerializeField] private GameObject[] destroyObjects;

    private void Start()
    {
        var host = Dns.GetHostEntry(Dns.GetHostName());
        foreach (var ip in host.AddressList)
        {
            if (ip.AddressFamily == AddressFamily.InterNetwork) // Get IPv4 address
            {
                this.ip.text = ip.ToString();
            }
        }
    }

    public override void Update()
    {
        base.Update();
        if (isMousePointing && Input.GetMouseButtonUp(mouseButton) && ip.text != string.Empty && ip.text != "" && port.text != string.Empty && port.text != "")
        {
            NetworkManager.Singleton.GetComponent<UnityTransport>().SetConnectionData(ip.text, (ushort)Convert.ToInt32(port.text));

            if (create)
            {
                NetworkManager.Singleton.StartHost();
            }
            else
            {
                NetworkManager.Singleton.StartClient();
            }
        }

        if (NetworkManager.Singleton.IsServer || NetworkManager.Singleton.IsClient || NetworkManager.Singleton.IsHost)
        {
            Destroy(gameObject);
            foreach (GameObject obj in destroyObjects)
            {
                Destroy(obj);
            }
        }
    }
}
