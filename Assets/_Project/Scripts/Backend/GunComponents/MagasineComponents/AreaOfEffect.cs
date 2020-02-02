using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class AreaOfEffect : MagasineComponent
{
    [SerializeField] float radius;

    protected override object[] templateSubs => new object[] { radius };


    public override void Effect(Vector3 pos, ref List<Enemy> enemies, ref Bullet.Flags flags)
    {
        var nearby = Physics
            .SphereCastAll(pos, radius, Vector3.up)
            .Select(thing => thing.collider.GetComponent<Enemy>())
            .Where(e => e != null);
        enemies.AddRange(nearby);
    }
}
