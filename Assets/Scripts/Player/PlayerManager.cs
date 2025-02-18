using System;
using Unity.Netcode;
using Unity.Services.Authentication;
using Unity.Services.Core;
using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    public static PlayerManager Instance { get; private set; }

    [SerializeField] private string menuSceneName;

    public Action OnStartedDigging { get; set; }
    public Action OnFinishedDigging { get; set; }
    private bool isDigging = false;

    private void OnGameStop(bool i)
    {
        SceneSwitcher.Instance.ChangeScene(menuSceneName);
    }

    private void Awake()
    {
        Instance = this;
    }

    private async void Start()
    {
        await UnityServices.InitializeAsync();
        await AuthenticationService.Instance.SignInAnonymouslyAsync();

        NetworkManager.Singleton.OnClientStopped += OnGameStop;
    }

    private void Update()
    {
        if (PlayerInput.Dig() && !isDigging)
        {
            isDigging = true;
            OnStartedDigging?.Invoke();
        }
        if (!PlayerInput.Dig() && isDigging)
        {
            isDigging = false;
            OnFinishedDigging?.Invoke();
        }
    }
}
