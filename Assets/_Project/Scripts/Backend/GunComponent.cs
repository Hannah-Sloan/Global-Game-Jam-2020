using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class GunComponent : MonoBehaviour
{
    [TextArea(15, 20)]
    public string descriptionTemplate;
    protected abstract System.Object[] templateSubs { get; }

    [ContextMenu("AddToInventory")]
    public void AddToInventory()
    {
        Inventory.Instance.AddComponent(this, 0);
    }

    public override string ToString()
    {
        return string.Format(descriptionTemplate, templateSubs);
    }
}
