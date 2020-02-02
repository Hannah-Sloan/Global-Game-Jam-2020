using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Accuracy : MuzzleComponent
{
    [Range(0, 1)] public float value;

    protected override System.Object[] templateSubs => new System.Object[] { value };

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
