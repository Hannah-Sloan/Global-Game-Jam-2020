using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using Sirenix.Serialization;

public class AreaOfEffect : MagasineComponent, ITierable<float>
{
    [SerializeField] float radius;

    protected override object[] templateSubs => new object[] { radius };

    public List<Tier<float>> tiers => _tiers;

    [SerializeField]
    List<Tier<float>> _tiers = new List<Tier<float>>{
            new Tier<float>
            {
                lowerBound = 5,
                upperBound = 10,
            },
            new Tier<float>
            {
                lowerBound = 11,
                upperBound = 20,
            },
            new Tier<float>
            {
                lowerBound = 21,
                upperBound = 30,
            },
            new Tier<float>
            {
                lowerBound = 31,
                upperBound = 50,
            },
            new Tier<float>
            {
                lowerBound = 51,
                upperBound = 100,
            },
    };

    public override void Effect(Vector3 pos, ref List<Enemy> enemies, ref Bullet.Flags flags)
    {
        var nearby = Physics
            .SphereCastAll(pos, radius, Vector3.up)
            .Select(thing => thing.collider.GetComponent<Enemy>())
            .Where(e => e != null);
        enemies.AddRange(nearby);
    }

    public void Randomize(int tier)
    {
        radius = Random.Range(tiers[tier].lowerBound, tiers[tier].upperBound);
    }
}
