using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExpireOnAwake : MonoBehaviour
{
    private void Awake()
    {
        gameObject.SetActive(false);
    }
}
