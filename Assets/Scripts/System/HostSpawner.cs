using Unity.Netcode;
using UnityEngine;

public class HostSpawner : NetworkBehaviour
{
    [SerializeField] private GameObject[] objs;

    private void Start()
    {
        if (!IsHost || !IsOwner) return;
        foreach (GameObject obj in objs)
        {
            Instantiate(obj).GetComponent<NetworkObject>().Spawn(true);
        }
    }
}
