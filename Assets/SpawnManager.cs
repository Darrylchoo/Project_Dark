using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    [SerializeField] private Transform[] spawnPoints;
    [SerializeField] private GameObject[] crates;
    [SerializeField] private float spawnTimer;

    public int crateNum;
    public int maxCrate;
    public bool isMax;
    public bool spawnOne;
    public bool spawnAll;
    private GameObject crate;
    private int spawnIndex;
    private int crateIndex;
    private float rememberSpawnTimer = 0;

    // Start is called before the first frame update
    void Start()
    {
        spawnPoints = new Transform[transform.childCount];

        for (int i = 0; i < transform.childCount; i++)
        {
            spawnPoints[i] = transform.GetChild(i);
        }

        spawnAll = true;
    }

    // Update is called once per frame
    void Update()
    {
        CheckCrate();
        SpawnCrates();
    }

    private void SpawnCrates()
    {
        while (crateNum < maxCrate && spawnOne)
        {
            spawnIndex = Random.Range(0, spawnPoints.Length);
            crateIndex = Random.Range(0, crates.Length);

            if (spawnPoints[spawnIndex].transform.childCount > 0)
            {
                continue;
            }
            else if (spawnPoints[spawnIndex].transform.childCount == 0)
            {
                if (isMax) 
                {
                    spawnAll = false;
                    return;
                }

                crate = Instantiate(crates[crateIndex], spawnPoints[spawnIndex].position, Quaternion.identity);
                crate.transform.SetParent(spawnPoints[spawnIndex]);
                crateNum++;

                if (spawnAll)
                    spawnOne = true;
                else
                    spawnOne = false;
            }
        }
    }

    private void CheckCrate()
    {
        if (crateNum == maxCrate)
        {
            isMax = true;
            rememberSpawnTimer = spawnTimer;
        }
        else
        {
            if (rememberSpawnTimer > 0)
            {
                rememberSpawnTimer -= Time.deltaTime;
            }

            if (rememberSpawnTimer <= 0)
            {
                isMax = false;
                spawnOne = true;
                rememberSpawnTimer = spawnTimer;
            }
        }
    }
}
