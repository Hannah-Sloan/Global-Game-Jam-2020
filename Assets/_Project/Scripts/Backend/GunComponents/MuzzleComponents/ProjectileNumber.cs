using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileNumber : MuzzleComponent, ITierable<int>
{
    public int value;

    public List<Tier<int>> tiers => _tiers;

    [SerializeField]
    List<Tier<int>> _tiers = new List<Tier<int>>{
            new Tier<int>
            {
                lowerBound = 2,
                upperBound = 4,
            },
            new Tier<int>
            {
                lowerBound = 5,
                upperBound = 10,
            },
            new Tier<int>
            {
                lowerBound = 10,
                upperBound = 20,
            },
            new Tier<int>
            {
                lowerBound = 20,
                upperBound = 40,
            },
            new Tier<int>
            {
                lowerBound = 40,
                upperBound = 80,
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
