using NUnit.Framework;
using System;
using System.Collections.Generic;
using Unity.Netcode;
using UnityEngine;

public interface IDamageble
{
    void GetDamage(float damage);
    GameObject GetObject();
}

public class DestroyableObject : NetworkBehaviour, IDamageble
{
    [SerializeField] private NetworkVariable<float> hp = new NetworkVariable<float>(6f, NetworkVariableReadPermission.Everyone, NetworkVariableWritePermission.Owner);

    public Action OnDamage { get; set; }

    public virtual void GetDamage(float damage)
    {
        hp.Value -= damage;
        
        OnDamage?.Invoke();
    }

    public virtual GameObject GetObject()
    {
        return gameObject;
    }

    public virtual void Die()
    {
        NetworkObject.Despawn(gameObject);
    }

    public virtual void Update()
    {
        if (hp.Value <= 0 && IsOwner)
        {
            Die();
        }
    }
}
