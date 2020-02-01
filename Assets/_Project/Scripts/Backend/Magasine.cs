using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Magasine : Singleton<Magasine>
{
    
    public MagasineComponent[] components = new MagasineComponent[Gun.COMPONENT_NUMBER];

    [SerializeField] Bullet bulletPrefab;

    [HideInInspector] public Gun gun;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public Bullet GetBullet()
    {
        var bullet = Instantiate(bulletPrefab, gun.firePositon.position, gun.firePositon.rotation);

        foreach (var comp in components)
        {
            if (comp == null) continue;

            if (comp is AreaOfEffect)
                bullet.aoeEvent += comp.Effect;
            else if (comp is Lightning)
                bullet.withLightning = true;
            else
                bullet.collisionEvent += comp.Effect;
        }

        return bullet;
    }
}
