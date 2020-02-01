﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[
    RequireComponent(typeof(Magasine)), 
    RequireComponent(typeof(Trigger)), 
    RequireComponent(typeof(Muzzle))
]
public class Gun : MonoBehaviour
{
    #region GUN_CONSTANTS
    public const int COMPONENT_NUMBER = 5;
    #endregion

    Trigger trigger;
    Magasine magasize;
    Muzzle muzzle;

    [SerializeField] Player player;
    [SerializeField] float default_gun_strength;

    public Transform firePositon;

    private void Awake()
    {
        
    }

    // Start is called before the first frame update
    void Start()
    {
        trigger = GetComponent<Trigger>();
        magasize = GetComponent<Magasine>();
        muzzle = GetComponent<Muzzle>();

        magasize.gun = this;
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
        var bullet = config.bullet;
        var rb = bullet.GetComponent<Rigidbody>();
        var dir = firePositon.transform.forward;
        rb.AddForce(dir * default_gun_strength);
    }

    void Fire()
    {
        if (trigger.CanFire())
        {
            LaunchProjectiles(
                muzzle.ModifyBullet(
                    magasize.GetBullet()
                )
            );
        }
    }

    public struct LaunchConfig
    {
        public int numberOfProjectiles;
        public float kickback;
        public float accuracy;
        public Bullet bullet;
    }
}