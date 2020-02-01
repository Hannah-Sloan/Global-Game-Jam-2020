using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField] Enemy enemyPrefab;
    [SerializeField] float spawnFrequency;

    float spawnTimeout => 1 / spawnFrequency;
    Timer spawnTimer;

    // Start is called before the first frame update
    void Start()
    {
        spawnTimer = Timer.CreateTimer(spawnTimeout);
    }

    // Update is called once per frame
    void Update()
    {
        if (spawnTimer.CheckAndReset())
        {
            Instantiate(enemyPrefab, transform);
        }
    }
}
