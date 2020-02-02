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

    void Refresh()
    {
        var uniqueGunComponents = mods
           .Where(x => x != null)
           .GroupBy(x => x.GetType())
           .Select(x => x.First())
           .ToList();
        int i = 0;
        foreach (var gc in uniqueGunComponents)
        {
            if (i > Magasine.Instance.components.Length) break;
            if (gc is MagasineComponent)
            {
                Magasine.Instance.components[i++] = gc as MagasineComponent;
            }
        }
    }

    public override void AddMod(GunComponent mod)
    {
        var nextOpen = mods.FindIndex(x => x == null);
        mods[nextOpen] = mod;

        Refresh();
    }

    public override void RemoveMod(GunComponent mod)
    {
        var location = mods.FindIndex(x => x == mod);
        var trash = mods[location];
        mods[location] = null;
        Destroy(trash);
        Refresh();
    }
}
