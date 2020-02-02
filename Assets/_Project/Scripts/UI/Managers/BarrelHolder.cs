using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class BarrelHolder : Holder
{
    public override int GetTypeAllowance()
    {
        return 1;
    }

    public override void AddMod(GunComponent mod)
    {
        var noMoreNulls =
            mods
                .Where(x => x != null)
                .GroupBy(x => x.GetType())
                .Select(x => x.First())
                .ToList();
    }

    public override void RemoveMod(GunComponent mod)
    {

    }
}
