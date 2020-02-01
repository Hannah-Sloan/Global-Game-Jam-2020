using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Trigger : MonoBehaviour
{
    [SerializeField] int default_capacity;
    [SerializeField] float default_fireRate;
    [SerializeField] float default_reloadTime;

    [SerializeField]
    TriggerComponent[] components = new TriggerComponent[Gun.COMPONENT_NUMBER];

    #region cached_data
    int capacity;
    float fireRate;
    float reloadTime;

    float shotTimeout => 1 / fireRate;
    #endregion

    Timer timeoutTimer;
    bool reloading = false;
    int inClip;

    // Start is called before the first frame update
    void Start()
    {
        capacity = default_capacity;
        fireRate = default_fireRate;
        reloadTime = default_reloadTime;

        UpdateValues();
        timeoutTimer = Timer.CreateTimer(shotTimeout);
        inClip = capacity;
    }

    void UpdateValues()
    {
        foreach(var comp in components)
        {
            if (comp == null) break;

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
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void Reload()
    {
        reloading = true;

        StartCoroutine(WaitAndThen(reloadTime, () =>
        {
            inClip = capacity;
            reloading = false;
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
            inClip--;

        //Debug.Log($"enoughAmmo: {enoughAmmo}, " +
        //    $"enoughTimeElapse: {enoughTimeElapsed}, " +
        //    $"reloading: {reloading}" +
        //    $"capacity: {capacity}");

        return canFire;
    }
}
