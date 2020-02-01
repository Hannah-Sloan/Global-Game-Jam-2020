using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Penetrative : MagasineComponent
{
    public override void Effect(Vector3 pos, ref List<Enemy> enemy, ref Bullet.Flags flags)
    {
        flags |= Bullet.Flags.supressDestroy;
    }
}
