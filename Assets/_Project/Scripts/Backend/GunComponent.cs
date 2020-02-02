using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.Serialization;

public abstract class GunComponent : MonoBehaviour 
{
    [TextArea(15, 20)]
    public string descriptionTemplate;
    protected abstract System.Object[] templateSubs { get; }

    [ContextMenu("AddToInventory")]
    public void AddToInventory(int i)
    {
        Inventory.Instance.AddComponent(this, i);
    }

    public override string ToString()
    {
        return string.Format(descriptionTemplate, templateSubs);
    }

    [System.Serializable]
    public struct Tier<T> where T: 
            struct,
          System.IComparable,
          System.IComparable<T>,
          System.IConvertible,
          System.IEquatable<T>,
          System.IFormattable
    {
        public T lowerBound;
        public T upperBound;
    }
}
