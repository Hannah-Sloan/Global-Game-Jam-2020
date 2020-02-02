using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public abstract class Holder : MonoBehaviour
{
    protected List<GunComponent> mods;

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
        Start();
        mods.Add(mod);
    }

    public virtual void RemoveMod(GunComponent mod)
    {
        mods.Remove(mod);
    }

    public abstract int GetTypeAllowance();

    private void Start()
    {
        mods = new List<GunComponent>() { null, null, null, null, null, null, };
    }
}
