using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Accuracy : MuzzleComponent, ITierable<float>
{
    [Range(0, 1)] public float value;

    public List<Tier<float>> tiers => _tiers;

    [SerializeField] List<Tier<float>> _tiers = new List<Tier<float>>{
            new Tier<float>
            {
                lowerBound = .4f,
                upperBound = .6f,
            },
            new Tier<float>
            {
                lowerBound = .61f,
                upperBound = .8f,
            },
            new Tier<float>
            {
                lowerBound = .81f,
                upperBound = 1,
            },
            new Tier<float>
            {
                lowerBound = 0f,
                upperBound = .39f,
            },
    };

    protected override System.Object[] templateSubs => new System.Object[] { value };

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
