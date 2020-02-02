using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryHolder : Holder
{
    public override int GetTypeAllowance()
    {
        return 4;
    }

    public override void Start()
    {
        mods = new List<GunComponent>() { null, null, null, null, null};
    }
}
