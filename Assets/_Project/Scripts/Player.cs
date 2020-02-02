using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] Transform camera;
    [SerializeField] float reach;
    [SerializeField] LayerMask pickupMask;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown("e"))
        {
            RaycastHit hit;
            if (Physics.Raycast(camera.position, camera.forward, out hit, reach, pickupMask))
            {
                var comp = hit.collider.GetComponent<GunComponent>();
                if (comp != null)
                {
                    Pickup(comp);
                }
            }
        }
    }

    void Pickup(GunComponent comp)
    {
        //Debug.Log(comp.gameObject);
        Destroy(comp.GetComponent<MeshFilter>());
        Destroy(comp.GetComponent<MeshRenderer>());
        Destroy(comp.GetComponent<Collider>());
        comp.AddToInventory();
    }
}
