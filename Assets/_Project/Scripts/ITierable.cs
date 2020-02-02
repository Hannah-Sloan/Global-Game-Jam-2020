using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface ITierable<T> where T :
            struct,
          System.IComparable,
          System.IComparable<T>,
          System.IConvertible,
          System.IEquatable<T>,
          System.IFormattable
{

    List<GunComponent.Tier<T>> tiers { get; }

    [ContextMenu("Randomize")]
    void Randomize(int tier);
}
