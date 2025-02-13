using Unity.Netcode;
using Unity.Services.Authentication;
using Unity.Services.Core;
using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    [SerializeField] private string menuSceneName;

    private void OnGameStop(bool i)
    {
        SceneSwitcher.Instance.ChangeScene(menuSceneName);
    }

    private async void Start()
    {
        await UnityServices.InitializeAsync();
        await AuthenticationService.Instance.SignInAnonymouslyAsync();

        NetworkManager.Singleton.OnClientStopped += OnGameStop;
    }
}
