using UnityEngine;

public class CustomAnimator : MonoBehaviour
{
    [SerializeField] private SpriteRenderer spr;

    [SerializeField] private Sprite[] sprites;
    public float TimePerFrame;
    [SerializeField] private bool loop;

    private float curTimePerFrame = 0f;
    public int CurSprite { get; set; }

    private void Start()
    {
        spr.sprite = sprites[CurSprite];
    }

    private void Update()
    {
        if (!loop && CurSprite == sprites.Length - 1) return;
        if (loop && CurSprite == sprites.Length - 1) CurSprite = 0;

        curTimePerFrame -= Time.deltaTime;

        if (curTimePerFrame <= 0f)
        {
            curTimePerFrame = TimePerFrame;
            CurSprite++;
            spr.sprite = sprites[CurSprite];
        }
    }
}
