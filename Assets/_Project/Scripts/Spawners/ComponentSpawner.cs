using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ComponentSpawner : MonoBehaviour
{
    [SerializeField] List<GunComponent> gunComponents;
    [SerializeField] float spawnRate;
    [SerializeField] float spawnHeight;
    [SerializeField] Vector2 xRange;
    [SerializeField] Vector2 yRange;
    float spawnTimeout => 1 / spawnRate;
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
            Spawn();
        }
    }

    void Spawn()
    {
        var i = Random.Range(0, gunComponents.Count);
        var comp = gunComponents[i];
        var location = new Vector3(
                    Random.Range(xRange.x, xRange.y),
                    spawnHeight,
                    Random.Range(yRange.x, yRange.y)
                );

        Instantiate(comp, location, transform.rotation);
    }
}
