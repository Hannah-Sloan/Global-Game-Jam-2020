using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FixMeBreakage : MonoBehaviour
{
    System.Type componentTypeToBreak;
    System.Action done;

    public void Init(System.Type componentTypeToBreak, System.Action done)
    {
        this.componentTypeToBreak = componentTypeToBreak;
        this.done = done;
    }

    public void RunAction()
    {
        done();

        Holder holderToBreakFrom;
        //Check if barrel
        if (componentTypeToBreak == typeof(MagasineComponent))
        {
            holderToBreakFrom = FindObjectOfType<BarrelHolder>();
        }
        //Check if mag
        else if (componentTypeToBreak == typeof(MuzzleComponent))
        {
            holderToBreakFrom = FindObjectOfType<MagazineHolder>();
        }
        //Check if body
        else //Trigger Comp
        {
            holderToBreakFrom = FindObjectOfType<BodyHolder>();
        }

        foreach (var mod in holderToBreakFrom.mods)
        {

        }
        bool deleteProb = Random.Range(0, 5) >= 4;

        Destroy(this.gameObject);
    }
}
