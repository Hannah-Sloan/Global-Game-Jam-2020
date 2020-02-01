using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class MagasineComponent : GunComponent
{
    public abstract void Effect(Vector3 pos, ref List<Enemy> enemy, ref Bullet.Flags flags);
}
