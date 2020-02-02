using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Knockback : MagasineComponent, ITierable<float>
{
    [SerializeField] float knockbackForce;

    public List<Tier<float>> tiers => _tiers;

    [SerializeField]
    List<Tier<float>> _tiers = new List<Tier<float>>{
            new Tier<float>
            {
                lowerBound = 100,
                upperBound = 200,
            },
            new Tier<float>
            {
                lowerBound = 210,
                upperBound = 500,
            },
            new Tier<float>
            {
                lowerBound = 510,
                upperBound = 1000,
            },
            new Tier<float>
            {
                lowerBound = 1010,
                upperBound = 1500,
            },
            new Tier<float>
            {
                lowerBound = 1510,
                upperBound = 3000,
            },
            new Tier<float>
            {
                lowerBound = 3010,
                upperBound = 300010,
            },
    };

    protected override object[] templateSubs => new object[] { knockbackForce };

    public override void Effect(Vector3 vel, ref List<Enemy> enemies, ref Bullet.Flags flags)
    {
        Debug.Log($"Knockback Effect called, with {enemies.Count} enemies");
        enemies.ForEach(enemy =>
        {
            var rb = enemy.GetComponent<Rigidbody>();
            var dir = vel.normalized;
            rb.AddForce(dir * knockbackForce);
            Debug.Log($"Knocking back, " +
                $"enemy: {enemy}, " +
                $"pos: {dir}, " +
                $"dir: {dir}");
        });
    }

    public void Randomize(int tier)
    {
        knockbackForce = Random.Range(tiers[tier].lowerBound, tiers[tier].upperBound);
    }
}
