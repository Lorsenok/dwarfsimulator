using UnityEngine;

public class PlayerAnimator : MonoBehaviour
{
    [SerializeField] private CustomAnimator idle;
    [SerializeField] private CustomAnimator run;

    [SerializeField] private Rigidbody rg;
    [SerializeField] private Material mat;

    [SerializeField] private float minSpeedForRunAnim = 0.1f;

    private void OnDestroy()
    {
        mat.SetVector("_Tiling", new Vector4(1, 1, 0, 0));
        mat.SetVector("_Offset", new Vector4(0, 0, 0, 0));
    }

    private void Update()
    {
        idle.enabled = rg.linearVelocity.x < minSpeedForRunAnim && rg.linearVelocity.x > -minSpeedForRunAnim && rg.linearVelocity.z < minSpeedForRunAnim && rg.linearVelocity.z > -minSpeedForRunAnim;
        run.enabled = !idle.enabled;
        if (rg.linearVelocity.x > minSpeedForRunAnim || rg.linearVelocity.x < -minSpeedForRunAnim)
        {
            mat.SetVector("_Tiling", new Vector4(rg.linearVelocity.x < 0f ? -1 : 1, 1, 0, 0));
            mat.SetVector("_Offset", new Vector4(rg.linearVelocity.x < 0f ? 1 : 0, 0, 0, 0));
        }
    }
}
