using UnityEngine;

public class Notify : MonoBehaviour
{
    public static Notify Instance { get; private set; }

    [SerializeField] private Transform spawnPoint;
    [SerializeField] private GameObject notifyObjectPrefab;

    private NotifyObject prevNotify;

    private void Awake()
    {
        Instance = this;
    }

    public void Log(string message, Color color, float time)
    {
        Instantiate(notifyObjectPrefab, spawnPoint.position, spawnPoint.rotation, transform).TryGetComponent(out NotifyObject notify);
        notify.Color = color;
        notify.Message = message;
        notify.ShowUpTime = time;
        if (prevNotify != null )
        {
            prevNotify.NextObject = notify.transform;
        }
        prevNotify = notify;
    }
}
