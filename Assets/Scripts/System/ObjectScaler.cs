using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectScaler : MonoBehaviour
{
    [SerializeField] private Vector2 defaultRatio;

    private Vector3 startScale;

    private void Awake()
    {
        startScale = transform.localScale;
    }

    private void Update()
    {
        float curRatio = (float)Screen.width / (float)Screen.height;
        float defRatio = defaultRatio.x / defaultRatio.y;

        transform.localScale = new Vector3(startScale.x, startScale.y, startScale.z) / defRatio * curRatio;
    }
}
