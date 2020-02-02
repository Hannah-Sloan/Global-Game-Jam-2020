using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class Gun : MonoBehaviour
{
    #region GUN_CONSTANTS
    public const int COMPONENT_NUMBER = 5;
    #endregion

    Trigger trigger;
    Magasine magasize;
    Muzzle muzzle;

    bool broken = false;
    System.Type lastBrokenComponent = null;

    [SerializeField] Player player;
    [SerializeField] float default_gun_strength;
    [SerializeField] float gunTime;

    public Transform firePositon;

    Timer breakTimer;

    private void Awake()
    {
        
    }

    // Start is called before the first frame update
    void Start()
    {
        trigger = Trigger.Instance;
        magasize = Magasine.Instance;
        muzzle = Muzzle.Instance;

        magasize.gun = this;

        breakTimer = Timer.CreateTimer(gunTime);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButton(0))
        {
            Fire();
        }
    }

    void Break()
    {
        Debug.Log("Broken!");
        broken = true;
        List<System.Type> compTypes = new List<System.Type>();
        compTypes.Add(typeof(MuzzleComponent));
        compTypes.Add(typeof(TriggerComponent));
        compTypes.Add(typeof(MagasineComponent));

        compTypes.Remove(lastBrokenComponent);

        var brokenComp = compTypes[Random.Range(0, compTypes.Count)];
        lastBrokenComponent = brokenComp;

        BreakIt.Instance.OnBreak(brokenComp, Repair);
    }

    public void Repair()
    {
        broken = false;
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

            rb.AddForce(dir * default_gun_strength);
        }
    }

    void Fire()
    {
        if (breakTimer.CheckComplete())
            Break();
        if (trigger.CanFire() && !broken)
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
