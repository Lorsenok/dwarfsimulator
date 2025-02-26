using UnityEngine;

public class Billboard : MonoBehaviour
{
    [SerializeField] private bool invert;

    private void Update()
    {
        if (Camera.main == null) return;
        transform.LookAt(Camera.main.transform.position);
        transform.eulerAngles = new Vector3(0f, invert ? transform.eulerAngles.y + 180 : transform.eulerAngles.y, 0f);
    }
}
