using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Holder : MonoBehaviour
{
    List<GunComponent> mods;

    public void AddMod(GunComponent mod)
    {
        mods.Add(mod);
    }

    public void RemoveMod(GunComponent mod)
    {
        mods.Remove(mod);
    }
}
