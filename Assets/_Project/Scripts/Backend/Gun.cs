﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gun : MonoBehaviour
{
    #region GUN_CONSTANTS
    public const int COMPONENT_NUMBER = 5;
    #endregion

    [SerializeField] Trigger trigger;
    [SerializeField] Magasine magasize;
    [SerializeField] Muzzle muzzle;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButton(0))
        {
            Fire();
        }
    }

    void LaunchProjectiles(LaunchConfig config)
    {

    }

    void Fire()
    {
        if (trigger.CanFire())
            LaunchProjectiles(
                muzzle.ModifyBullet(
                    magasize.GetBullet()
                )
            );
    }

    public struct LaunchConfig
    {
        public int numberOfProjectiles;
        public float kickback;
        public float accuracy;
        public Bullet bullet;
    }
}
