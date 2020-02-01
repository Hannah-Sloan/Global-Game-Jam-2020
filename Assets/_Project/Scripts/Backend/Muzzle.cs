using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Muzzle : MonoBehaviour
{
    [SerializeField]
    MuzzleComponent[] components = new MuzzleComponent[Gun.COMPONENT_NUMBER];
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public Gun.LaunchConfig ModifyBullet(Bullet bullet)
    {
        return new Gun.LaunchConfig
        {
            bullet = bullet
        };
    }
}
