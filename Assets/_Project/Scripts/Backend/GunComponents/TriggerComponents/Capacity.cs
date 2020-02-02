using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Capacity : TriggerComponent, ITierable<int>
{
    public int value;

    public List<Tier<int>> tiers => _tiers;

    [SerializeField]
    List<Tier<int>> _tiers = new List<Tier<int>>{
            new Tier<int>
            {
                lowerBound = 2,
                upperBound = 20,
            },
            new Tier<int>
            {
                lowerBound = 21,
                upperBound = 50,
            },
            new Tier<int>
            {
                lowerBound = 51,
                upperBound = 100,
            },
            new Tier<int>
            {
                lowerBound = 101,
                upperBound = 300,
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
