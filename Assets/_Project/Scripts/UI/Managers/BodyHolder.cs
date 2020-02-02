using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class BodyHolder : Holder
{
    public override int GetTypeAllowance()
    {
        return 3;
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
            if (i > Trigger.Instance.components.Length) break;
            if (gc is TriggerComponent)
            {
                Trigger.Instance.components[i++] = gc as TriggerComponent;
            }
        }
        //for (int i = 0; i < Trigger.Instance.components.Length; i++)
        //{
        //    if (uniqueGunComponents[i] is TriggerComponent)
        //        Trigger.Instance.components[i] = uniqueGunComponents[i] as TriggerComponent;
        //}

        Trigger.Instance.UpdateValues();
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