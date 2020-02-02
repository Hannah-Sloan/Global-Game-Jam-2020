using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] new Transform camera;
    [SerializeField] float reach;
    [SerializeField] LayerMask pickupMask;

    [SerializeField] MineCraftInv inv;
    [SerializeField] GameObject prompt;


    // Update is called once per frame
    void Update()
    {
        RaycastHit hit;
        if (Physics.Raycast(camera.position, camera.forward, out hit, reach, pickupMask))
        {
            prompt.SetActive(true);

            if (Input.GetKeyDown("e"))
            {
                var comp = hit.collider.GetComponent<GunComponent>();
                if (comp != null)
                {
                    Pickup(comp, inv.selectedIndex);
                }
            }
        }
        else
        {
            prompt.SetActive(false);
        }
    }

    void Pickup(GunComponent comp, int i)
    {
        //Debug.Log(comp.gameObject);
        Destroy(comp.GetComponent<MeshFilter>());
        Destroy(comp.GetComponent<MeshRenderer>());
        Destroy(comp.GetComponent<Collider>());
        Destroy(comp.GetComponent<Light>());
        for (int j = 0; j < comp.transform.childCount; j++)
            Destroy(comp.transform.GetChild(0).gameObject);
        comp.AddToInventory(i);
    }
}
