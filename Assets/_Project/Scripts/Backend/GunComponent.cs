using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunComponent : MonoBehaviour
{
    [ContextMenu("AddToInventory")]
    public void AddToInventory()
    {
        Inventory.Instance.AddComponent(this, 0);
    }
}
