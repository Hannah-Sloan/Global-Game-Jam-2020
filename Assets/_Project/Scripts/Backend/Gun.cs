using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

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
        player.GetComponent<CPMPlayer>().playerVelocity -= transform.forward * (float)(config.kickback);

        Bullet[] bullets = new Bullet[config.numberOfProjectiles];
        bullets[0] = config.bullet;
        for (int i = 1; i < bullets.Length; i++)
        {
            bullets[i] = Instantiate(config.bullet);
        }

        foreach (var bullet in bullets)
        {
            var rb = bullet.GetComponent<Rigidbody>();

            var randomDir =
                ((2 * Random.value - 1) * firePositon.up +
                (2 * Random.value - 1) * firePositon.right).normalized;
            var perturbation = Random.value * (1 - config.accuracy);
            var error = randomDir * perturbation;
            var dir = (firePositon.forward + error).normalized;

            Debug.Log(
                $"fd: {firePositon.forward}" +
                $"perturb: {perturbation}" +
                $"error: {error}" +
                $"dir: {dir}"
            );

            rb.AddForce(dir * default_gun_strength);
        }
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
        public int   numberOfProjectiles;
        public float kickback;
        public float accuracy;
        public Bullet bullet;
    }
}
