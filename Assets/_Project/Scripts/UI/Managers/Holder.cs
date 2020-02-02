﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Holder : MonoBehaviour
{
    protected List<GunComponent> mods;

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