using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryHolder : Holder
{
    public override int GetTypeAllowance()
    {
        return 4;
    }
}
