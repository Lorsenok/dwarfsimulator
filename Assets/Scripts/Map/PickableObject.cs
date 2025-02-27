using System;
using System.Collections.Generic;
using Unity.Netcode;
using UnityEngine;

public class PickableObject : NetworkBehaviour
{
    public static List<PickableObject> PickableObjects = new List<PickableObject>();

    protected bool picked;
    protected Transform followTarget;

    [SerializeField] protected Rigidbody rg;
    [SerializeField] protected float speed;
    [SerializeField] protected float destroyDistance = 0.2f;

    public virtual void Start()
    {
        PickableObjects.Add(this);
    }

    public override void OnDestroy()
    {
        base.OnDestroy();
        PickableObjects.Remove(this);
    }

    public bool CanBePicked { get; set; } = false;

    [ServerRpc]
    public virtual void DieServerRpc()
    {
        if (!IsHost) return;
        NetworkObject.Despawn(gameObject);
    }

    [ServerRpc]
    public virtual void PickedByServerRpc(int id)
    {
        picked = true;
        followTarget = IDManager.FindByID(id).transform;
    }

    public virtual void Update()
    {
        if (!picked) return;

        rg.useGravity = false;
        
        transform.position = Vector3.Lerp(transform.position, followTarget.position, Time.deltaTime * speed);
        if (Vector3.Distance(transform.position, followTarget.position) < destroyDistance) DieServerRpc();
    }
}
