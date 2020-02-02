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

    public bool ImGonnaWreckIt = false;

    private void Start()
    {
        UIEnableDisable.Instance.UIOn += WreckIt;
    }

    System.Type componentTypeToBreak;
    System.Action done;

    public void OnBreak(System.Type componentTypeToBreak, System.Action done)
    {
        this.componentTypeToBreak = componentTypeToBreak;
        this.done = done;
        ImGonnaWreckIt = true;
    }

    public void WreckIt()
    {
        if (ImGonnaWreckIt) ImGonnaWreckIt = false;

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
