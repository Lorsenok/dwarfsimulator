using System.Collections.Generic;
using Unity.Netcode;
using UnityEngine;

public class Block : DestroyableObject
{
    [SerializeField] private AdditionalDig addDig;

    [SerializeField] private GameObject[] ores;
    [SerializeField] private int oresMinQuantity;
    [SerializeField] private int oresMaxQuantity;

    [SerializeField] private float oresStartVelocityMultiplier;

    [SerializeField] private Collider coll;
    [SerializeField] private float boxColliderDisableDistance;

    public bool SpawnOreOnDestroy { get; set; } = true;

    public override void GetDamage(float damage)
    {
        base.GetDamage(damage);
        AdditionalDigAppearClientRpc();
    }

    [ClientRpc]
    private void AdditionalDigAppearClientRpc()
    {
        addDig.Appear();
    }

    [ServerRpc]
    public void SpawnOreServerRpc()
    {
        List<GameObject> chance = new List<GameObject>();
        foreach (GameObject ore in ores)
        {
            for (int i = 0; i < ore.GetComponent<Ore>().chancePoints; i++)
            {
                chance.Add(ore);
            }
        }

        int curOresQuantity = Random.Range(oresMinQuantity, oresMaxQuantity);
        for (int i = 0; i < curOresQuantity; i++)
        {
            GameObject obj = Instantiate(chance[Random.Range(0, chance.Count - 1)], transform.position, Quaternion.identity);
            obj.GetComponent<NetworkObject>().Spawn(true);
            obj.GetComponent<Rigidbody>().AddForce(new Vector3(Random.Range(-1f, 1f), Random.Range(-1f, 1f), Random.Range(-1f, 1f)) * oresStartVelocityMultiplier, ForceMode.Impulse);
        }
    }

    public override void Die()
    {
        if (SpawnOreOnDestroy) SpawnOreServerRpc();
        base.Die();
    }

    public override void Update()
    {
        base.Update();
        if (CameraMover.Instance == null) return;
        coll.enabled = Vector3.Distance(CameraMover.Instance.transform.position, transform.position) > boxColliderDisableDistance;
    }
    
    private void Start()
    {
        if (!IsOwner) return;
        
        addDig.DamagebleObject = this;
        OnDamage += addDig.SetRandomPosition;
    }
}
