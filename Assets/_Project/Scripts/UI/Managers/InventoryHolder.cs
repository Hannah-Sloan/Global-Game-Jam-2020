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
        UIEnableDisable.Instance.UIOff += OnUIEnd;
        mods = new List<GunComponent>() { null, null, null, null, null};
    }

    public void OnUIEnd()
    {
        Inventory.Instance.SyncToAddLater(mods);
    }
}
