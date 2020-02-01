using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Trigger : MonoBehaviour
{
    [SerializeField]
    TriggerComponent[] components = new TriggerComponent[Gun.COMPONENT_NUMBER];

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public bool CanFire()
    {
        return true;
    }
}
