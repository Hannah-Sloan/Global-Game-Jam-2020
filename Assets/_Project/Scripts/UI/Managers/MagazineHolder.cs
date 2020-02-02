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
        base.AddMod(mod);
        Refresh();
    }

    public override void RemoveMod(GunComponent mod)
    {
        base.RemoveMod(mod);
        Refresh();
    }

    public override void Start()
    {
        mods = new List<GunComponent>() { null, null, null, null };
    }
}
