using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Kickback : MuzzleComponent, ITierable<float>
{
    public float value;

    public List<Tier<float>> tiers => _tiers;

    [SerializeField]
    List<Tier<float>> _tiers = new List<Tier<float>>{
            new Tier<float>
            {
                lowerBound = 10,
                upperBound = 15,
            },
            new Tier<float>
            {
                lowerBound = 5,
                upperBound = 20,
            },
            new Tier<float>
            {
                lowerBound = 5,
                upperBound = 50,
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
