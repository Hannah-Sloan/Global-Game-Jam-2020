using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BreakIt : Singleton<BreakIt>
{
    public GameObject uhOhBreaky;
    public GameObject explosionAnimation;


    public Transform Mag;
    public Transform Barrel;
    public Transform Body;


    public void OnBreak(System.Type componentTypeToBreak, System.Action done)
    {
        if (componentTypeToBreak != typeof(GunComponent))
        {
            Debug.LogError("ERROR ERROR".Bold().Size(35));
            Debug.LogError("RILEY!!!".Bold().Size(35));
            Debug.LogError(("THAT IS" + "NOT".Bold() + "A GUN COMPONENT").Size(35));
        }

        Transform breakVisPos;
        //Check if barrel
        if (componentTypeToBreak == typeof(MagasineComponent))
        {
            breakVisPos = Barrel;
        }
        //Check if mag
        else if (componentTypeToBreak == typeof(MuzzleComponent))
        {
            breakVisPos = Mag;
        }
        //Check if body
        else //Trigger Comp
        {
            breakVisPos = Body;
        }

        var breaky = Instantiate(uhOhBreaky, breakVisPos);
        breaky.GetComponent<FixMeBreakage>().Init(componentTypeToBreak, done);
        breaky.transform.localPosition = Vector3.zero;
    }
}
