using UnityEngine;

public class Controler : MonoBehaviour
{
    public bool CanMove { get; set; } = true;

    public float AdditionalSpeed { get; set; } = 0f;
    public float AdditionalAcceleration { get; set; } = 0f;
    public float AdditionalDeceleration { get; set; } = 0f;


    [SerializeField] private AudioSource sound;
    [SerializeField] private float soundBlockTimeSet;

    private float curSoundBlockTime = 0f;

    [SerializeField] private Rigidbody rg;

    [SerializeField] private float acceleration;
    [SerializeField] private float speed;
    [SerializeField] private float decceleration;

    [SerializeField, Range(0f, 1f)] private float controlsDeadZone = 0.1f;
    [SerializeField, Range(0f, 1f)] private float deceleraitionDeadZone = 0.05f;

    private Vector3 curSpeed = Vector2.zero;
    private Vector3 controls = Vector2.zero;

    private void Move()
    {
        controls = new Vector3(Input.GetAxis("Horizontal"), 0f, Input.GetAxis("Vertical")).normalized;

        curSpeed += Vector3.Lerp(Vector2.zero, controls, (acceleration + AdditionalAcceleration) * Time.deltaTime);

        if (controls.x < controlsDeadZone && controls.x > -controlsDeadZone)
        {
            float x = -curSpeed.x;
            if (x > 0f) x = 1f;
            if (x < 0f) x = -1f;
            curSpeed = new Vector3(curSpeed.x + Mathf.Lerp(0f, x, Time.deltaTime * (decceleration + AdditionalDeceleration)), curSpeed.y, curSpeed.z);

            if (curSpeed.x < deceleraitionDeadZone && curSpeed.x > -deceleraitionDeadZone)
            {
                curSpeed = new Vector3(0f, curSpeed.y, curSpeed.z);
            }
        }

        if (controls.z < controlsDeadZone && controls.z > -controlsDeadZone)
        {
            float z = -curSpeed.z;
            if (z > 0f) z = 1f;
            if (z < 0f) z = -1f;
            curSpeed = new Vector3(curSpeed.x, curSpeed.y, curSpeed.z + Mathf.Lerp(0f, z, Time.deltaTime * (decceleration + AdditionalDeceleration)));

            if (curSpeed.z < deceleraitionDeadZone && curSpeed.z > -deceleraitionDeadZone)
            {
                curSpeed = new Vector3(curSpeed.x, curSpeed.y, 0f);
            }
        }

        curSpeed = new Vector3(Mathf.Clamp(curSpeed.x, -(speed + AdditionalSpeed), speed + AdditionalSpeed),
            curSpeed.y,
            Mathf.Clamp(curSpeed.z, -(speed + AdditionalSpeed), speed + AdditionalSpeed));
        rg.linearVelocity = curSpeed;
    }

    private void Update()
    {
        if (rg.linearVelocity.x > 0.1f | rg.linearVelocity.x < -0.1f | rg.linearVelocity.z > 0.1f | rg.linearVelocity.z < -0.1f && sound != null)
        {
            if (!sound.isPlaying && curSoundBlockTime <= 0)
            {
                sound.Play();
                curSoundBlockTime = soundBlockTimeSet;
            }
        }
        else if (sound != null) sound.Stop();

        if (CanMove) Move();
        else if (sound != null) sound.Stop();
    }
}
