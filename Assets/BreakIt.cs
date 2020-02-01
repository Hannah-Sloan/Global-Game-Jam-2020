using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BreakIt : Singleton<BreakIt>
{
    public void OnBreak(System.Type componentTypeToBreak)
    {
        if (componentTypeToBreak != typeof(GunComponent))
        {
            Debug.LogError("ERROR ERROR".Bold().Size(35));
            Debug.LogError("RILEY!!!".Bold().Size(35));
            Debug.LogError(("THAT IS" + "NOT".Bold() + "A GUN COMPONENT").Size(35));
        }
    }
}
