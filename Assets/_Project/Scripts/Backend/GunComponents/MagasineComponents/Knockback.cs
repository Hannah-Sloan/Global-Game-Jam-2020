using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Knockback : MagasineComponent
{
    [SerializeField] float knockbackForce;

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
}
