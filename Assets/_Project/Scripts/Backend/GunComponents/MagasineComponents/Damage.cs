using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Damage : MagasineComponent, ITierable<int>
{
    [SerializeField] int damage;

    public List<Tier<int>> tiers => _tiers;

    [SerializeField]
    List<Tier<int>> _tiers = new List<Tier<int>>{
            new Tier<int>
            {
                lowerBound = 4,
                upperBound = 11,
            },
            new Tier<int>
            {
                lowerBound = 12,
                upperBound = 19,
            },
            new Tier<int>
            {
                lowerBound = 20,
                upperBound = 30,
            },
    };

    protected override object[] templateSubs => new object[] { damage };


    public override void Effect(Vector3 pos, ref List<Enemy> enemies, ref Bullet.Flags flags)
    {
        enemies.ForEach(enemy => enemy.TakeDamage(damage));
    }

    public void Randomize(int tier)
    {
        damage = Random.Range(tiers[tier].lowerBound, tiers[tier].upperBound);
    }
}
