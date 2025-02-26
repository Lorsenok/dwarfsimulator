using System;
using System.Collections.Generic;
using Unity.Netcode;
using UnityEngine;

public class IDManager : NetworkBehaviour
{
    private static List<IDManager> gameObjects = new List<IDManager>();

    private static int curId = 0;
    public NetworkVariable<int> ID { get; private set; } = new NetworkVariable<int>(0);

    [SerializeField] private bool testValue = false;

    public void Start()
    {
        gameObjects.Add(this);
        if (IsHost || !NetworkManager.IsConnectedClient) ID.Value = curId;
        curId++;
        if (testValue) Debug.Log(ID.Value);
    }

    public override void OnDestroy()
    {
        base.OnDestroy();
        gameObjects.Remove(this);
    }

    public static GameObject FindByID(int id)
    {
        for (int i = 0; i < gameObjects.Count; i++)
        {
            if (Convert.ToInt32(gameObjects[i].ID.Value) == id)
            {
                return gameObjects[i].gameObject;
            }
        }

        return null;
    }
}