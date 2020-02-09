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

        //                  uh oh vvv this was somehow changed while using? everytime we hit fix?
        List<UIModMover> toBreak = new List<UIModMover>();
        foreach (var mod in holderToBreakFrom.mods)
        {
            if (mod == null) continue;
            int val = Random.Range(0, 26);
            bool deleteProb = val > 24;

            if(deleteProb)
                toBreak.Add(mod.transform.parent.GetComponent<UIModMover>());
        }
        foreach (var deadGirlWalking in toBreak)
        {
            deadGirlWalking.Break();
        }

        Destroy(this.gameObject);
    }
}
