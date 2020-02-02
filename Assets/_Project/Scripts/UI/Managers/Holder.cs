using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public abstract class Holder : MonoBehaviour
{
    public List<GunComponent> mods;

    [ContextMenu("TestMods")]
    public void TestMods()
    {
        Debug.Log($"Testing Mods, with mods length {mods.Count}");
        //mods
        //    .Where(x => x != null)
        //    .GroupBy(x => x.GetType())
        //    .Select(x => x.First())
        //    .ToList()
        //    .ForEach(Debug.Log);
        var int1 = mods;
        var int2 = int1.Where(x => x != null);
        var int3 = int2.GroupBy(x => x.GetType());
        var int4 = int3.Select(x => x.First());

        int1.ToList().ForEach(Debug.Log);
        int2.ToList().ForEach(Debug.Log);
        int3.ToList().ForEach(Debug.Log);
        int4.ToList().ForEach(Debug.Log);

        Debug.Log("ending testing mods");
        
    }

    public virtual void AddMod(GunComponent mod)
    {
        if (mods == null || mods.Capacity == 0 || mods.Count == 0) Start();
        var index = mods.FindIndex(x => x == null);
        mods[index] = mod;
    }

    public virtual void RemoveMod(GunComponent mod)
    {
        var location = mods.FindIndex(x => x == mod);
        if (location != -1) { 
        mods[location] = null;
            return;
        }
        Debug.Log("This mod was not found when removing it from its prev holder.");
    }

    public abstract int GetTypeAllowance();

    public abstract void Start();
}
