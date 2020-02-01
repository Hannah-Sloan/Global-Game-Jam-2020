using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lightning : MagasineComponent
{
    public override void Effect(Vector3 pos, ref List<Enemy> enemy, ref Bullet.Flags flags)
    {
        Debug.Log("Shocking");
    }
}
