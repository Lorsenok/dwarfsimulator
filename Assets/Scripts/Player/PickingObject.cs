using Unity.Netcode;
using UnityEngine;

public class PickingObject : NetworkBehaviour
{
    [SerializeField] private IDManager pickPoint;

    public static float AdditionalDistance { get; set; } = 0f;
    [SerializeField] private float distance;

    [SerializeField] private float takeTimeChangeSpeed = 1f;
    [SerializeField] private float takeStartTime;
    [SerializeField] private float takeMinTime;

    private float curTakeTime = 0.01f;
    private float curTakeTimeSet = 0f;

    private PickableObject target;

    private void Start()
    {
        curTakeTimeSet = takeStartTime;
    }

    private void Update()
    {
        if (!IsOwner) return;

        foreach (PickableObject obj in PickableObject.PickableObjects)
        {
            float _distance = Vector2.Distance(transform.position, obj.transform.position);
            if (_distance < distance + AdditionalDistance)
            {
                if (target == null)
                {
                    target = obj;
                }
                else if (_distance < Vector2.Distance(transform.position, target.transform.position))
                {
                    target = obj;
                }
                else
                {
                    obj.CanBePicked = true;
                }
            }
            else obj.CanBePicked = false;
        }

        if (target == null)
        {
            curTakeTimeSet = 0f;
            return;
        }

        if (PlayerInput.Take())
        {
            curTakeTime -= Time.deltaTime;
        }

        if (curTakeTime <= 0f)
        {
            PickServerRpc(target.GetComponent<IDManager>().ID.Value);
            curTakeTime = curTakeTimeSet;
            curTakeTimeSet = Mathf.Lerp(curTakeTimeSet, takeMinTime, takeTimeChangeSpeed);
        }
    }

    [ServerRpc]
    public void PickServerRpc(int objectId)
    {
        GameObject obj = IDManager.FindByID(objectId);
        if (obj == null) return;
        obj.TryGetComponent(out PickableObject pickobj);
        if (pickobj != null) pickobj.PickedByServerRpc(pickPoint.ID.Value);
    }
}
