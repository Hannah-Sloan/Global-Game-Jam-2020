using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using System;

public class UIModMover : MonoBehaviour
{
    public Blank home = null;
    private Blank potentialNewHome = null;
    public float radius = 5f;

    //private void Awake()
    //{
    //    home = transform.parent.GetComponentInChildren<Blank>();
    //}

    public void Grabbed(Vector3 mouseWorldLoc)
    {
        transform.position = new Vector3(mouseWorldLoc.x, mouseWorldLoc.y, transform.position.z);
    }

    public void AddToInv(int indexToInsertInto)
    {
        if (home.resident != null)
            Destroy(home.resident.gameObject);
        transform.parent = home.transform.parent;
        home.resident = this;
        home.transform.parent.parent.GetComponent<InventoryHolder>().AddMod(transform.GetComponentInChildren<GunComponent>(), indexToInsertInto);
        transform.localPosition = Vector3.zero;
    }

    public void Dropped()
    {
        Update();

        bool allowed = false;
        if (transform.GetComponentInChildren<GunComponent>() is MagasineComponent && potentialNewHome.transform.parent.parent.GetComponent<Holder>().GetTypeAllowance() == 1)
            allowed = true;
        else if (transform.GetComponentInChildren<GunComponent>() is MuzzleComponent && potentialNewHome.transform.parent.parent.GetComponent<Holder>().GetTypeAllowance() == 2)
            allowed = true;
        else if (transform.GetComponentInChildren<GunComponent>() is TriggerComponent && potentialNewHome.transform.parent.parent.GetComponent<Holder>().GetTypeAllowance() == 3)
            allowed = true;
        else if (potentialNewHome.transform.parent.parent.GetComponent<Holder>().GetTypeAllowance() == 4)
            allowed = true;

        if (potentialNewHome != null && potentialNewHome.resident == null && allowed)
        {
            home.resident = null;
            home.transform.parent.parent.GetComponent<Holder>().RemoveMod(transform.GetComponentInChildren<GunComponent>());
            home = potentialNewHome;
            transform.parent = home.transform.parent;
            home.resident = this;
            int index;
            Int32.TryParse(potentialNewHome.transform.parent.name.Replace("Slot", ""), out index);
            potentialNewHome.transform.parent.parent.GetComponent<Holder>().AddMod(transform.GetComponentInChildren<GunComponent>(), index-1);
        }
        transform.localPosition = Vector3.zero;
    }

    public void Break()
    {
        home.resident = null;
        home.transform.parent.parent.GetComponent<Holder>().RemoveMod(transform.GetComponentInChildren<GunComponent>());
        transform.parent = null;

        //Spawn explosion animation
        Instantiate(BreakIt.Instance.explosionAnimation, transform.position, Quaternion.identity);

        //Destroy this gameobject
        Destroy(this.gameObject);
    }

    private void Update()
    {
        potentialNewHome = ((Blank[])(FindObjectsOfType(typeof(Blank)))).
                            Where(blank => (transform.position - blank.transform.position).magnitude <= radius).
                            OrderBy(blank => (transform.position - blank.transform.position).sqrMagnitude).
                            FirstOrDefault();
    }
}
