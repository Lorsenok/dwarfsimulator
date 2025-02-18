using UnityEngine;

public class PlayerAnimator : MonoBehaviour
{
    [SerializeField] private Rigidbody rg;
    [SerializeField] private CustomAnimator idle;
    [SerializeField] private CustomAnimator dig;
    [SerializeField] private Transform moveAnimObject;
    [SerializeField] private float moveAnimDistance;
    [SerializeField] private float moveAnimSpeed;
    [SerializeField] private Material mat;

    [SerializeField] private float minSpeedForRunAnim = 0.1f;

    private bool isDigging = false;

    private void OnStartDig()
    {
        isDigging = true;
    }

    private void OnFinishDig()
    {
        isDigging = false;
    }

    private void Awake()
    {
        PlayerManager.Instance.OnStartedDigging += OnStartDig;
        PlayerManager.Instance.OnFinishedDigging += OnFinishDig;
    }

    private void Update()
    {
        if (Mathf.Abs(rg.linearVelocity.x) > minSpeedForRunAnim || Mathf.Abs(rg.linearVelocity.z) > minSpeedForRunAnim)
            moveAnimObject.localRotation = Quaternion.Euler(0f, 0f, ProjMath.SinTime(m:moveAnimSpeed, canBeNegative:true) * moveAnimDistance);
        else
            moveAnimObject.localRotation = Quaternion.identity;

        idle.enabled = !isDigging;
        dig.enabled = isDigging;
    }
}
