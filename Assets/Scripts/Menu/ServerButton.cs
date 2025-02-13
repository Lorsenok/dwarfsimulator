using System;
using System.Collections;
using System.Net;
using System.Net.Sockets;
using TMPro;
using Unity.Netcode;
using Unity.Netcode.Transports.UTP;
using Unity.Services.Authentication;
using Unity.Services.Core;
using Unity.Services.Relay;
using Unity.Services.Relay.Models;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;
using UnityEngine.UIElements;

public class ServerButton : GameButton
{
    [Header("Server")]
    [SerializeField] private bool create;
    [SerializeField] private TMP_InputField code;
    [SerializeField] private GameObject[] destroyObjects;
    [SerializeField] private TextMeshProUGUI codeText;
    public void Start()
    {
        NetworkManager.Singleton.OnClientStarted += OnClientStart;
    }

    public async override void Update()
    {
        base.Update();
        if (isMousePointing && Input.GetMouseButtonUp(mouseButton))
        {
            if (create)
            {
                try
                {
                    Allocation allocation = await RelayService.Instance.CreateAllocationAsync(3);

                    string joinCode = await RelayService.Instance.GetJoinCodeAsync(allocation.AllocationId);
                    codeText.text += joinCode;

                    NetworkManager.Singleton.GetComponent<UnityTransport>().SetHostRelayData(
                        allocation.RelayServer.IpV4,
                        (ushort)allocation.RelayServer.Port,
                        allocation.AllocationIdBytes,
                        allocation.Key,
                        allocation.ConnectionData
                        );

                    NetworkManager.Singleton.StartHost();
                }
                catch (RelayServiceException ex)
                {
                    Debug.LogException(ex);
                }
            }
            else if (code.text != string.Empty && code.text != "")
            {
                try
                {
                    JoinAllocation allocation = await RelayService.Instance.JoinAllocationAsync(code.text);

                    NetworkManager.Singleton.GetComponent<UnityTransport>().SetClientRelayData(
                        allocation.RelayServer.IpV4,
                        (ushort)allocation.RelayServer.Port,
                        allocation.AllocationIdBytes,
                        allocation.Key,
                        allocation.ConnectionData,
                        allocation.HostConnectionData
                        );

                    NetworkManager.Singleton.StartClient();
                }
                catch (RelayServiceException ex)
                {
                    Debug.LogException(ex);
                }
            }
        }
    }

    private void OnClientStart()
    {
        Destroy(gameObject);
        foreach (GameObject obj in destroyObjects)
        {
            Destroy(obj);
        }
    }
}
