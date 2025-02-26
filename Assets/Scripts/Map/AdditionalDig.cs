using System.Collections.Generic;
using Unity.Netcode;
using UnityEngine;
using UnityEngine.UI;

public class AdditionalDig : NetworkBehaviour, IDamageble
{
    [SerializeField] private float appearSpeed;
    [SerializeField] private Color colorSet;
    [SerializeField] private Image image;

    private bool appeared = false;

    [SerializeField] private RectTransform canvas;
    [SerializeField] private DestroyableObject damagebleObject;

    [SerializeField] private float damageMultiplier;

    [SerializeField] private GameObject particles;

    public void GetDamage(float damage)
    {
        damagebleObject.GetDamage(damage * damageMultiplier);
        Instantiate(particles, transform.position, Quaternion.identity).GetComponent<NetworkObject>().Spawn(true);
        SetRandomPosition();
    }

    public virtual GameObject GetObject()
    {
        return gameObject;
    }

    private void SetRandomPosition()
    {
        transform.localPosition = new Vector3(Random.Range(-canvas.sizeDelta.x, canvas.sizeDelta.x), Random.Range(-canvas.sizeDelta.x, canvas.sizeDelta.y), 0);
    }

    private void Start()
    {
        damagebleObject.OnDamage += SetRandomPosition;
    }

    public void Appear()
    {
        appeared = true;
    }

    private void Update()
    {
        if (!appeared) return;
        image.color = Color.Lerp(image.color, colorSet, appearSpeed * Time.deltaTime);
    }
}