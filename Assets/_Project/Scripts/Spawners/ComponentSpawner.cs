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
    [SerializeField] bool randomizeDrops;
    [SerializeField] public float tierScaleFactor;
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

        var toset = Instantiate(comp, transform);

        //Debug.Log($"isRandomized? {randomizeDrops}");

        if (randomizeDrops && isTiered<int>(toset))
        {
            var tosetAsTiered = toset as ITierable<int>;
            var tier = chooseTier(tosetAsTiered.tiers);
            tosetAsTiered.Randomize(tier);
        }
        else if (randomizeDrops && isTiered<float>(toset))
        {
            var tosetAsTiered = toset as ITierable<float>;
            var tier = chooseTier(tosetAsTiered.tiers);
            tosetAsTiered.Randomize(tier);
        }

        toset.transform.position = location;
    }

    bool isTiered<T>(GunComponent gc) where T : struct,
          System.IComparable,
          System.IComparable<T>,
          System.IConvertible,
          System.IEquatable<T>,
          System.IFormattable
    {
        
        var val = typeof(ITierable<T>).IsAssignableFrom(gc.GetType());
        //Debug.Log(
        //    $"type: {gc.GetType()}, " +
        //    $"t: {typeof(T)}, " +
        //    $"val: {val}");
        return val;
    }

    int chooseTier<T>(List<GunComponent.Tier<T>> tiers)
        where T : struct,
          System.IComparable,
          System.IComparable<T>,
          System.IConvertible,
          System.IEquatable<T>,
          System.IFormattable
    {
        int PoolMax = tiers.Count;
        List<int> tierPool = new List<int>();

        

        for (int i = 0; i < PoolMax; i++)
        {
            if (EnemySpawner.Instance.totalKills + 1 >= Mathf.Pow(tierScaleFactor, i))
            {
                tierPool.Add(i);
            }
        }

        var chosenTier =  tierPool[Random.Range(0, tierPool.Count)];
        //Debug.Log($"tier: {chosenTier}");
        return chosenTier;
    }
}
