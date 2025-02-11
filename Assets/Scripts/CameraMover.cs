using UnityEngine;

public class CameraMover : MonoBehaviour
{
    public static CameraMover Instance { get; private set; }
    public float AdditionalSpeed { get; set; } = 0.0f;

    public Transform CurPosition { get; set; }

    [SerializeField] private Transform[] instantPositions;
    [SerializeField] private float defaultSpeed;

    public void ResetPosition(int id = 0)
    {
        CurPosition = instantPositions[id];
    }

    private void Awake()
    {
        if (Instance != null)
        {
            Destroy(Instance);
        }
        Instance = this;

        CurPosition = instantPositions[0];
    }

    private void FixedUpdate()
    {
        transform.position = Vector3.Lerp(transform.position, CurPosition.position, Time.deltaTime * (defaultSpeed + AdditionalSpeed));
        transform.rotation = Quaternion.Lerp(transform.rotation, CurPosition.rotation, Time.deltaTime * (defaultSpeed + AdditionalSpeed));
    }
}
