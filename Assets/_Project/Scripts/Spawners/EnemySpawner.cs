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

    List<SpawnData> spawnDatas = new List<SpawnData>();


    IEnumerator Ping(){
        while (true){
            Debug.Log("Ping");
            yield return null;
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        foreach (Transform transChild in transform){
            spawnDatas.Add(new SpawnData{
            lastSpawnTime = -Mathf.Infinity,
            nextSpawnTime = 0,
            location = transChild,
        });
        }
        UIEnableDisable.Instance.UIOff += RestartSpawners;
        RestartSpawners();
    }

    private void Update() {
        elapsedTime += Time.deltaTime;
    }

    class SpawnData
    {
        public float nextSpawnTime;
        public float lastSpawnTime;

        public Transform location;

        public override string ToString(){
            return $"SpawnData(nextSpawnTime={this.nextSpawnTime},lastSpawTime={lastSpawnTime}";
        }
    }

    IEnumerator Spawner(SpawnData data)
    {
        int id = Random.Range(10000, 9999);
        //Debug.Log($"Starting Spawn with id: {id}!");
        // SpawnData data = new SpawnData{
        //     lastSpawnTime = -Mathf.Infinity,
        //     nextSpawnTime = 0,
        // };
        while (true)
        {
            //Debug.Log($"{Time.time}, data: ${data}, ration: {spawnRatio}");
            if (Time.time > data.nextSpawnTime)
            {
                //Debug.Log($"Prespawn: {data}");
                Spawn(ref data);
                //Debug.Log($"postspawn: {data}");
            }

            yield return null;
        }

        //Debug.Log($"Stopping spawning (for some reason) with id: {id}!");
    }

    void Spawn(ref SpawnData data)
    {
        Instantiate(enemyPrefab, data.location);
        data.lastSpawnTime = Time.time;
        data.nextSpawnTime = data.lastSpawnTime + spawnRatio * spawnRateScaleFactor + fudge;
    }

    public void EnemyKilled()
    {
        enemykills++;
    }

    void RestartSpawners(){
        StopAllCoroutines();
        Debug.Log("String Spawners");
        StartCoroutine(Ping());

        foreach (var data in spawnDatas)
        {
            StartCoroutine(Spawner(data));
        }
    }
}
