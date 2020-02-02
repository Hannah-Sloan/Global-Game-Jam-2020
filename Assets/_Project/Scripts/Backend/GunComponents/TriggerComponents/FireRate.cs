using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireRate : TriggerComponent, ITierable<float>
{
    public float value;

    public List<Tier<float>> tiers => _tiers;

    [SerializeField]
    List<Tier<float>> _tiers = new List<Tier<float>>{
            new Tier<float>
            {
                lowerBound = 7,
                upperBound = 11,
            },
            new Tier<float>
            {
                lowerBound = 12,
                upperBound = 20,
            },
            new Tier<float>
            {
                lowerBound = 21,
                upperBound = 40,
            },
            new Tier<float>
            {
                lowerBound = .3f,
                upperBound = 6,
            },
            new Tier<float>
            {
                lowerBound = 50,
                upperBound = 150,
            },
    };

    protected override object[] templateSubs => new object[] { value };

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Randomize(int tier)
    {
        value = Random.Range(tiers[tier].lowerBound, tiers[tier].upperBound);
    }
}
