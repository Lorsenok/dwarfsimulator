using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnOnDestroy : MonoBehaviour
{
    [SerializeField] private GameObject[] spawnOnDestroyObjects;

    private void OnDestroy()
    {
        if (!gameObject.scene.isLoaded) return;
        foreach (GameObject obj in spawnOnDestroyObjects)
        {
            Instantiate(obj, transform.position, obj.transform.rotation);
        }
    }
}
