using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class NotifyObject : MonoBehaviour
{
    public Transform NextObject { get; set; }
    public string Message { get; set; }
    public Color Color { get; set; }

    [SerializeField] private TextMeshProUGUI text;
    [SerializeField] private float spaceBetweenOjects;
    [SerializeField] private float defaultX;
    [SerializeField] private float speed;
    [SerializeField] private float destroyY;
    [SerializeField] private float destroyYMultiplier = 2f;

    public float ShowUpTime { get; set; } = 1f;

    private void Update()
    {
        text.text = Message;
        text.color = Color;

        transform.localPosition = Vector3.Lerp(transform.localPosition, new Vector3(defaultX, transform.localPosition.y, transform.localPosition.z), Time.deltaTime * speed);

        if (NextObject == null)
        {
            ShowUpTime -= Time.deltaTime;
            if (ShowUpTime <= 0f)
                transform.localPosition = Vector3.Lerp(transform.localPosition, new Vector3(transform.localPosition.x, destroyY * destroyYMultiplier, transform.localPosition.z), Time.deltaTime * speed);
        }
        else
        {
            transform.localPosition = Vector3.Lerp(transform.localPosition, new Vector3(transform.localPosition.x, NextObject.localPosition.y + spaceBetweenOjects, transform.localPosition.z),
                Time.deltaTime * speed);
            ShowUpTime = 0f;
        }

        if (transform.localPosition.y > destroyY) Destroy(gameObject);
    }
}
