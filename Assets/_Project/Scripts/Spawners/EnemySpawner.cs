using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : Singleton<EnemySpawner>
{
    [SerializeField] Enemy enemyPrefab;
    [Range(0, 1)][SerializeField] float spawnRateScaleFactor = 1;

    public int totalKills => enemykills;

   // float spawnTimeout => 1 / spawnFrequency;
    //Timer spawnTimer;
    float elapsedTime = 0;
    int enemykills = 0;
    float spawnRatio => elapsedTime / (enemykills + 1);

    float lastSpawnTime = -Mathf.Infinity;
    float nextSpawnTime = 0;

    // Start is called before the first frame update
    void Start()
    {
        //spawnTimer = Timer.CreateTimer(spawnTimeout);
        nextSpawnTime = lastSpawnTime + spawnRatio * spawnRateScaleFactor;
    }

    // Update is called once per frame
    void Update()
    {
        //if (spawnTimer.CheckAndReset())
        //{
        //    Instantiate(enemyPrefab, transform);
        //}

        if (Time.time > nextSpawnTime)
        {
            Spawn();
        }

        elapsedTime += Time.deltaTime;
    }

    void Spawn()
    {
        Instantiate(enemyPrefab, transform);
        lastSpawnTime = Time.time;
        nextSpawnTime = lastSpawnTime + spawnRatio * spawnRateScaleFactor;
    }

    public void EnemyKilled()
    {
        enemykills++;
    }
}
