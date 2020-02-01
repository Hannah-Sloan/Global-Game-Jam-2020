using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Muzzle : Singleton<Muzzle>
{
    [SerializeField] int   default_projectileNumber;
    [SerializeField] float default_kickback;
    [Range(0,1), SerializeField] float default_accuracy;
    [SerializeField] bool default_heavy;


    [SerializeField] float heavyFactor;

    [SerializeField]
    MuzzleComponent[] components = new MuzzleComponent[Gun.COMPONENT_NUMBER];

    #region data_cache
    int   projectileNumber;
    float kickback;
    float accuracy;
    bool  heavy;
    #endregion

    // Start is called before the first frame update
    void Start()
    {
        projectileNumber = default_projectileNumber;
        kickback         = default_kickback        ;
        accuracy         = default_accuracy        ;
        heavy            = default_heavy           ;

        UpdateValues();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void UpdateValues()
    {
        foreach (var comp in components)
        {
            if (comp == null) continue;

            if (comp is Accuracy)
            {
                accuracy = (comp as Accuracy).value;
            }
            if (comp is Heavy)
            {
                heavy = true;
            }
            if (comp is ProjectileNumber)
            {
                projectileNumber = (comp as ProjectileNumber).value;
            }
            if (comp is Kickback)
            {
                kickback = (comp as ProjectileNumber).value;
            }
        }
    }

    public Gun.LaunchConfig ModifyBullet(Bullet bullet)
    {
        if (heavy)
        {
            var rb = bullet.GetComponent<Rigidbody>();
            rb.mass *= heavyFactor;
        }

        return new Gun.LaunchConfig
        {
            bullet = bullet,
            numberOfProjectiles = projectileNumber,
            kickback = kickback,
            accuracy = accuracy,
        };
    }
}
