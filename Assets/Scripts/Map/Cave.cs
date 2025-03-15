using System;
using Unity.Netcode;
using UnityEngine;

public class Cave : NetworkBehaviour
{
    [SerializeField] private GameObject defaultBlock;
    [SerializeField] private GameObject[] blocksPrefabs;
    [SerializeField] private float[] continueChances;
    [SerializeField] private float[] continueChanceDeviders;

    [SerializeField] private int width;
    [SerializeField] private int height;

    [SerializeField] private float widthSpace;
    [SerializeField] private float heightSpace;

    private GameObject[,] blocksGrid = new GameObject[0, 0];
    private GameObject[,] curBlocksGrid = new GameObject[0, 0];

    public void Clear()
    {
        blocksGrid = new GameObject[width, height];
        for (int i = 0; i < blocksGrid.GetLength(0); i++)
        {
            for (int j = 0; j < blocksGrid.GetLength(1); j++)
            {
                blocksGrid[i, j] = defaultBlock;
            }
        }
    }

    public void Set()
    {
        foreach (GameObject obj in curBlocksGrid)
        {
            obj.TryGetComponent(out Block b);
            b.SpawnOreOnDestroy = false;
            b.Die();
        }

        curBlocksGrid = new GameObject[width, height];
        for (int i = 0; i < blocksGrid.GetLength(0); i++)
        {
            for (int j = 0; j < blocksGrid.GetLength(1); j++)
            {
                curBlocksGrid[i, j] = Instantiate(blocksGrid[i, j], transform.position + new Vector3(i * widthSpace, 0f, j * heightSpace), Quaternion.identity);
                curBlocksGrid[i, j].GetComponent<NetworkObject>().Spawn(true);
            }
        }
    }

    public void Extend(int x, int y,  float continueChance, float continueChanceDivider, GameObject prefab)
    {
        blocksGrid[x, y] = prefab;

        Vector2[] directions =
        {
            new(1, 1),
            new(1, 0),
            new(1, -1),
            new(-1, 1),
            new(-1, 0),
            new(-1, -1),
            new(0, 1),
            new(0, -1)
        };

        foreach (var direction in directions)
        {
            if (UnityEngine.Random.Range(0f, 1f) <= continueChance)
            {
                int posX = Mathf.Clamp(x + Convert.ToInt32(direction.x), 0, blocksGrid.GetLength(0) - 1);
                int posY = Mathf.Clamp(y + Convert.ToInt32(direction.y), 0, blocksGrid.GetLength(1) - 1);

                Extend(posX, posY, continueChance / continueChanceDivider, continueChanceDivider, prefab);
            }
        }
    }

    public void Update()
    {
        if (!IsOwner || !Input.GetKeyDown(KeyCode.T)) return;
        Clear();
        for (int i = 0; i < blocksPrefabs.Length; i++)
        {
            Extend(UnityEngine.Random.Range(0, width), UnityEngine.Random.Range(0, height), continueChances[i], continueChanceDeviders[i], blocksPrefabs[i]);
        }
        Set();
    }
}
