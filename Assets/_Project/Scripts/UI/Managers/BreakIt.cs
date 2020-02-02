using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BreakIt : Singleton<BreakIt>
{
    public void OnBreak(System.Type componentTypeToBreak, System.Action done)
    {
        if (componentTypeToBreak != typeof(GunComponent))
        {
            Debug.LogError("ERROR ERROR".Bold().Size(35));
            Debug.LogError("RILEY!!!".Bold().Size(35));
            Debug.LogError(("THAT IS" + "NOT".Bold() + "A GUN COMPONENT").Size(35));
        }

        //Check if barrel
        if (componentTypeToBreak == typeof(MagasineComponent))
        {

        }

        //Check if mag
        if (componentTypeToBreak == typeof(MuzzleComponent))
        {

        }

        //Check if body
        if (componentTypeToBreak == typeof(TriggerComponent))
        {
            
        }

        //TODO: Call done once fixed.
        done();
    }
}
