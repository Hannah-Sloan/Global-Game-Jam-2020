using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class HUD : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI ammoText;
    [SerializeField] Gun gun;

    readonly string template = "ammo: {0}";

    string ammo = "";

    public void OnAmmoChange(int ammo)
    {
        this.ammo = string.Format(template, ammo);
    }

    public void OnReload(bool reloading)
    {
        if (reloading)
        {
            ammo = $"ammo: reloading!";
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        Trigger.Instance.OnAmmoChange += OnAmmoChange;
        Trigger.Instance.OnReload += OnReload;
    }

    // Update is called once per frame
    void Update()
    {
        if (gun.broken) ammoText.text = "GUN'S BROKEN";
        else ammoText.text = ammo;
    }
}
