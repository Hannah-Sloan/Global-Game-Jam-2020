using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReloadTime : TriggerComponent
{
    public float value;

    protected override object[] templateSubs => new object[] { value };
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
