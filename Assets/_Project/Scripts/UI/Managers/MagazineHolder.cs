using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class MagazineHolder : Holder
{
    public override int GetTypeAllowance()
    {
        return 2;
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
            if (i > Muzzle.Instance.components.Length) break;
            if (gc is MuzzleComponent)
            {
                Muzzle.Instance.components[i++] = gc as MuzzleComponent;
            }
        }
        Muzzle.Instance.UpdateValues();
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
        //var trash = mods[location];
        mods[location] = null;
       // Destroy(trash);
        Refresh();
    }
}
