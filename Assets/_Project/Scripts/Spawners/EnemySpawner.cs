using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : Singleton<EnemySpawner>
{
    [SerializeField] Enemy enemyPrefab;
    //[SerializeField] List<Transform> spawnLocations;
    [Range(0, 1)][SerializeField] float spawnRateScaleFactor = 1;

    public int totalKills => enemykills;

   // float spawnTimeout => 1 / spawnFrequency;
    //Timer spawnTimer;
    float elapsedTime = 0;
    int enemykills = 0;
    float spawnRatio => elapsedTime / (enemykills + 1);

    //float lastSpawnTime = -Mathf.Infinity;
    //float nextSpawnTime = 0;
    float fudge => Random.Range(0, 5f);


    // Start is called before the first frame update
    void Start()
    {
        //spawnTimer = Timer.CreateTimer(spawnTimeout);
        //nextSpawnTime = lastSpawnTime + spawnRatio * spawnRateScaleFactor;
        //spawnLocations.ForEach(location => StartCoroutine(Spawner(location)));
        foreach (Transform transChild in transform)
        {
            StartCoroutine(Spawner(transChild));
        }
    }

    struct SpawnData
    {
        public float nextSpawnTime;
        public float lastSpawnTime;
    }

    IEnumerator Spawner(Transform location)
    {
        SpawnData data = new SpawnData{
            lastSpawnTime = -Mathf.Infinity,
            nextSpawnTime = 0,
        };
        while (true)
        {
            if (Time.time > data.nextSpawnTime)
            {
                Spawn(ref data, location);
            }

            elapsedTime += Time.deltaTime;
            yield return null;
        }
    }

    void Spawn(ref SpawnData data, Transform transform)
    {
        Instantiate(enemyPrefab, transform);
        data.lastSpawnTime = Time.time;
        data.nextSpawnTime = data.lastSpawnTime + spawnRatio * spawnRateScaleFactor + fudge;
    }

    public void EnemyKilled()
    {
        enemykills++;
    }
}
