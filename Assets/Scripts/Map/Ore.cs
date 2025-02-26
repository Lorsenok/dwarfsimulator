using System;
using UnityEngine;

public class Ore : PickableObject
{
    [Header("Ore")]
    public int chancePoints;
    [SerializeField] private Transform background;
    [SerializeField] private float backgroundSizeChangeSpeed;
    [SerializeField] private Vector3 deafaultBackgroundSize;
    [SerializeField] private Vector3 selectedBackgroundSize;

    [Header("Log")]
    [SerializeField] private float showUpInLogTime = 1f;
    [SerializeField] private string orename;
    [SerializeField] private Color orecolor = Color.white;

    public override void Update()
    {
        base.Update();

        background.localScale = Vector3.Lerp(background.localScale, CanBePicked ? selectedBackgroundSize : deafaultBackgroundSize, backgroundSizeChangeSpeed * Time.deltaTime);
    }

    public override void OnDestroy()
    {
        base.OnDestroy();

        if (!gameObject.scene.isLoaded) return;
        if (CanBePicked)
            Notify.Instance.Log("You've got " + orename + "!", orecolor, showUpInLogTime);
    }
}
