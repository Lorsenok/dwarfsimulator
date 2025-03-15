using System;
using Unity.Netcode;
using UnityEngine;

public class PlayerDig : NetworkBehaviour
{
    [SerializeField] private GameObject particles;
    [SerializeField] private CustomAnimator anim;
    [SerializeField] private LayerMask hitLayer;
    [SerializeField] private int frame;
    [SerializeField] private float distance;
    [SerializeField] private float speed = 1f;
    [SerializeField] private float damage;

    private float startTimePerFrame;

    private void Start()
    {
        startTimePerFrame = anim.TimePerFrame;
    }

    private void Dig()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

        RaycastHit hit;
        Physics.Raycast(ray, out hit, Mathf.Infinity, hitLayer);
        if (hit.collider == null) return;

        if (Vector3.Distance(hit.point, transform.position) > distance) return;
        Instantiate(particles, hit.point, Quaternion.identity);

        hit.collider.gameObject.TryGetComponent(out IDamageble damageble);
        if (damageble == null) return;
        if (IsHost) damageble.GetDamage(damage);
        else
        {
            DamageServerRpc(damage, damageble.GetObject().GetComponentInChildren<IDManager>().ID.Value);
        }
    }

    [ServerRpc]
    private void DamageServerRpc(float damage, int id)
    {
        IDManager.FindByID(id).GetComponent<IDamageble>().GetDamage(damage);
    }

    private bool hasWorked = false;

    private void Update()
    {
        if (!IsOwner) return;

        anim.TimePerFrame = startTimePerFrame * speed;

        if (anim.CurSprite == frame && !hasWorked)
        {
            hasWorked = true;
            Dig();
        }
        if (anim.CurSprite != frame)
        {
            hasWorked = false;
        }
    }
}
