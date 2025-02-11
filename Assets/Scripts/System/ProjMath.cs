using UnityEngine;

public class ProjMath : MonoBehaviour
{
    public static float EaseOutBounce(float x)
    {
        float n1 = 7.5625f;
        float d1 = 2.75f;

        if (x < 1 / d1)
        {
            return n1 * x * x;
        }
        else if (x < 2 / d1)
        {
            return n1 * (x - 1.5f / d1) * x + 0.75f;
        }
        else if (x < 2.5 / d1)
        {
            return n1 * (x - 2.25f / d1) * x + 0.9375f;
        }
        else
        {
            return n1 * (x - 2.625f / d1) * x + 0.984375f;
        }
    }

    public static float EaseInBounce(float x)
    {
        float n1 = 7.5625f;
        float d1 = 2.75f;

        if (x < 1 / d1)
        {
            return n1 * x * x;
        }
        else if (x < 2 / d1)
        {
            return n1 * (x -= 1.5f / d1) * x + 0.75f;
        }
        else if (x < 2.5 / d1)
        {
            return n1 * (x -= 2.25f / d1) * x + 0.9375f;
        }
        else
        {
            return n1 * (x -= 2.625f / d1) * x + 0.984375f;
        }
    }

    public static float EaseInOutBounce(float x) //for bounce animation
    {
        return x < 0.5
            ? (1 - EaseOutBounce(1 - 2 * x)) / 2
            : (1 + EaseOutBounce(2 * x - 1)) / 2;
    }

    public static float EaseOutQuint(float x)
    {
        return 1 - Mathf.Pow(1 - x, 5);
    }

    public static float RotateTowardsPosition(float x, float y, float targetX, float targetY)
    {
        Vector3 diference = new Vector2(x, y) - new Vector2(targetX, targetY);
        return Mathf.Atan2(diference.y, diference.x) * Mathf.Rad2Deg;
    }

    public static void MoveTowardsAngle(Transform pos, float angle, float speed)
    {
        pos.position += new Vector3(Mathf.Sin(angle * Mathf.Deg2Rad), Mathf.Cos(angle * Mathf.Deg2Rad), 0) * speed;
    }

    public static float SinTime(float m = 1f, bool canBeNegative = false)
    {
        if (!canBeNegative) return Mathf.Sin(Time.timeSinceLevelLoad * m) * (Mathf.Sin(Time.timeSinceLevelLoad * m) > 0 ? 1f : -1f);
        else return Mathf.Sin(Time.timeSinceLevelLoad * m);
    }
}
