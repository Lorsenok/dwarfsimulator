using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bar : MonoBehaviour
{
    public float Value { get; set; } = 0f;
    public float MaxValue { get; set; } = 0f;
    [SerializeField] private float speed;
    [SerializeField] private Vector3 scale = new Vector3(1, 0, 0);

    private Vector3 startScale;

    private void Awake()
    {
        startScale = transform.localScale;
    }

    private void Update()
    {
        Vector3 s = Vector3.Lerp(transform.localScale, startScale / MaxValue * Value, Time.deltaTime * speed) - transform.localScale;
        s = new Vector3(s.x * scale.x, s.y * scale.y, s.z * scale.z);
        transform.localScale += s;
    }
}
