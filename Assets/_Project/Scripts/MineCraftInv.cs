using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MineCraftInv : MonoBehaviour
{
    public int selectedIndex = 0;
    [SerializeField] RectTransform highlight;
    [SerializeField] List<Image> tokencons;
    [SerializeField] Material defaultMaterial;

    private void Start()
    {
        selectedIndex = 0;
    }

    private void Update()
    {
        //Parse for and update by player scroll wheel input
        //note to not get array inndex out of bounds errors mod will likely be used.

        if (Input.GetAxis("Mouse ScrollWheel") > 0)
            selectedIndex++;
        if (Input.GetAxis("Mouse ScrollWheel") < 0)
            selectedIndex--;

        selectedIndex = (int)((float)(selectedIndex)).ModLoop(5);

        highlight.SetParent(transform.GetChild(selectedIndex));
        highlight.localPosition = Vector3.zero;
    }

    private void OnEnable()
    {
        UpdateFauxTokens();
    }

    private void UpdateFauxTokens()
    {
        for (int i = 0; i < tokencons.Count; i++)
        {
            GunComponent newMod;
            if (Inventory.Instance.toAddLater[i] == null)
                newMod = null;
            else
                newMod = Inventory.Instance.toAddLater[i].newMod;

            GameObject toSpawn = null;
            //Grabs icon from file.
            if (newMod is MagasineComponent)
            {
                if (newMod is AreaOfEffect)
                    toSpawn = Inventory.Instance.modTokens[0].tokens[0];
                if (newMod is Penetrative)
                    toSpawn = Inventory.Instance.modTokens[0].tokens[1];
                if (newMod is Knockback)
                    toSpawn = Inventory.Instance.modTokens[0].tokens[2];
                if (newMod is Lightning)
                    toSpawn = Inventory.Instance.modTokens[0].tokens[3];
                if (newMod is Fire)
                    toSpawn = Inventory.Instance.modTokens[0].tokens[4];
                if (newMod is Damage)
                    toSpawn = Inventory.Instance.modTokens[0].tokens[5];
            }
            else if (newMod is MuzzleComponent)
            {
                if (newMod is ProjectileNumber)
                    toSpawn = Inventory.Instance.modTokens[1].tokens[0];
                if (newMod is Kickback)
                    toSpawn = Inventory.Instance.modTokens[1].tokens[1];
                if (newMod is Heavy)
                    toSpawn = Inventory.Instance.modTokens[1].tokens[2];
                if (newMod is Accuracy)
                    toSpawn = Inventory.Instance.modTokens[1].tokens[3];
            }
            else //Type  of TriggerComponent
            {
                if (newMod is Capacity)
                    toSpawn = Inventory.Instance.modTokens[2].tokens[0];
                if (newMod is FireRate)
                    toSpawn = Inventory.Instance.modTokens[2].tokens[1];
                if (newMod is ReloadTime)
                    toSpawn = Inventory.Instance.modTokens[2].tokens[2];
            }

            if (toSpawn != null)
                tokencons[i].material = toSpawn.GetComponent<MeshRenderer>().sharedMaterial;
            else
                tokencons[i].material = defaultMaterial;
        }
    }
}

