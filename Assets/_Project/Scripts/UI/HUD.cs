using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class HUD : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI ammoText;

    public void OnAmmoChange(int ammo)
    {
        ammoText.text = $"ammo: {ammo}";
    }

    public void OnReload(bool reloading)
    {
        if (reloading)
        {
            ammoText.text = $"ammo: reloading!";
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
        
    }
}
