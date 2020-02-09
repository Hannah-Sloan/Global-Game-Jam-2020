using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Trigger : Singleton<Trigger>
{
    public delegate void AmmoCB(int ammo);
    public delegate void ReloadCB(bool reloading);

    public AmmoCB OnAmmoChange;
    public ReloadCB OnReload;

    [SerializeField] int default_capacity;
    [SerializeField] float default_fireRate;
    [SerializeField] float default_reloadTime;
    
    public TriggerComponent[] components = new TriggerComponent[Gun.COMPONENT_NUMBER];

    int capacity;
    float fireRate;
    float reloadTime;

    //float shotTimeout => 1 / fireRate;

    Timer timeoutTimer;
    bool reloading = false;
    int inClip;

    // Start is called before the first frame update
    void Start()
    {
        capacity = default_capacity;
        fireRate = default_fireRate;
        reloadTime = default_reloadTime;

        timeoutTimer = Timer.CreateTimer(1 / fireRate);
        UpdateValues();
    }

    public void UpdateValues()
    {
        foreach(var comp in components)
        {
            if (comp == null) continue;

            if (comp is Capacity)
            {
                capacity = (comp as Capacity).value;
            }
            if (comp is FireRate)
            {
                fireRate = (comp as FireRate).value;
            }
            if (comp is ReloadTime)
            {
                reloadTime = (comp as ReloadTime).value;
            }
        }

        Destroy(timeoutTimer.gameObject);
        timeoutTimer = Timer.CreateTimer(1 / fireRate);
        inClip = capacity;
        if (OnAmmoChange != null)
            OnAmmoChange(inClip);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void Reload()
    {
        reloading = true;
        OnReload(true);

        StartCoroutine(WaitAndThen(reloadTime, () =>
        {
            inClip = capacity;
            OnAmmoChange(inClip);
            reloading = false;
            OnReload(false);
        }));
    }

    IEnumerator WaitAndThen(float timeOut, System.Action andThen)
    {
        yield return new WaitForSeconds(timeOut);
        andThen();
    }

    public bool CanFire()
    {
        bool enoughTimeElapsed = timeoutTimer.CheckAndReset();
        bool enoughAmmo = inClip > 0;

        bool canFire = 
            !reloading &&
            enoughAmmo &&
            enoughTimeElapsed;

        if (!enoughAmmo)
            Reload();

        if (canFire)
        {
            inClip--;
            OnAmmoChange(inClip);
        }

        //Debug.Log($"enoughAmmo: {enoughAmmo}, " +
        //    $"enoughTimeElapse: {enoughTimeElapsed}, " +
        //    $"reloading: {reloading}" +
        //    $"capacity: {capacity}");

        return canFire;
    }
}
