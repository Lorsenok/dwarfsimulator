using System.Collections.Generic;
using UnityEngine;

public class CameraMover : MonoBehaviour
{
    public static CameraMover Instance { get; private set; }
    public float AdditionalSpeed { get; set; } = 0.0f;

    private Transform curPosition;

    public List<Transform> InstantPositions { get; set; }
    [SerializeField] private float defaultSpeed;

    public Transform InitialCamera { get; set; }

    public void ResetPosition()
    {
        curPosition = InitialCamera;
    }

    public void SetPosition(int id)
    {
        curPosition = InstantPositions[id];
    }

    private void Awake()
    {
        if (Instance != null)
        {
            Destroy(Instance);
        }
        Instance = this;
    }

    private void FixedUpdate()
    {
        if (transform == null || curPosition == null) return;
        transform.position = Vector3.Lerp(transform.position, curPosition.position, Time.deltaTime * (defaultSpeed + AdditionalSpeed));
        transform.rotation = Quaternion.Lerp(transform.rotation, curPosition.rotation, Time.deltaTime * (defaultSpeed + AdditionalSpeed));
    }
}
