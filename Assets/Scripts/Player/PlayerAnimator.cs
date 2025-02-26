using Unity.Netcode;
using UnityEngine;

public class PlayerAnimator : NetworkBehaviour
{
    [SerializeField] private Rigidbody rg;
    [SerializeField] private CustomAnimator idle;
    [SerializeField] private CustomAnimator dig;
    [SerializeField] private Transform moveAnimObject;
    [SerializeField] private float moveAnimDistance;
    [SerializeField] private float moveAnimSpeed;
    [SerializeField] private Material mat;

    [SerializeField] private float minSpeedForRunAnim = 0.1f;

    private NetworkVariable<bool> isDigging = new NetworkVariable<bool>(false, NetworkVariableReadPermission.Everyone, NetworkVariableWritePermission.Owner);
    private NetworkVariable<bool> isMoving = new NetworkVariable<bool>(false, NetworkVariableReadPermission.Everyone, NetworkVariableWritePermission.Owner);

    private void OnStartDig()
    {
        if (!IsOwner) return;
        isDigging.Value = true;
    }

    private void OnFinishDig()
    {
        if (!IsOwner) return;
        isDigging.Value = false;
    }

    private void Awake()
    {
        PlayerManager.Instance.OnStartedDigging += OnStartDig;
        PlayerManager.Instance.OnFinishedDigging += OnFinishDig;
    }

    private void Update()
    {
        idle.enabled = !isDigging.Value;
        dig.enabled = isDigging.Value;

        if (IsOwner)
        {
            isMoving.Value = Mathf.Abs(rg.linearVelocity.x) > minSpeedForRunAnim | Mathf.Abs(rg.linearVelocity.z) > minSpeedForRunAnim;
        }

        if (isMoving.Value)
            moveAnimObject.localRotation = Quaternion.Euler(0f, 0f, ProjMath.SinTime(m:moveAnimSpeed, canBeNegative:true) * moveAnimDistance);
        else
            moveAnimObject.localRotation = Quaternion.identity;
    }
}
