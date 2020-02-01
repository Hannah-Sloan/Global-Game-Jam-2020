using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Damage : MagasineComponent
{
    [SerializeField] int damage;

    public override void Effect(Vector3 pos, ref List<Enemy> enemies, ref Bullet.Flags flags)
    {
        enemies.ForEach(enemy => enemy.TakeDamage(damage));
    }
}
