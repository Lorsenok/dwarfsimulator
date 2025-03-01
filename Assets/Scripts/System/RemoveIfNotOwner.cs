using Unity.Netcode;
using UnityEngine;

public class RemoveIfNotOwner : NetworkBehaviour
{
    private void Start()
    {
        if (!IsOwner) Destroy(gameObject);
    }
}
