using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClickAndDrag : MonoBehaviour
{
    private new Camera camera;
    public float distToMods;

    void Start()
    {
        camera = GetComponent<Camera>();
    }

    UIModMover mover = null;

    // Update is called once per frame
    void Update()
    {
        if (mover == null)
        {
            if (Input.GetMouseButtonDown(0))
            {
                RaycastHit hit;

                Ray ray = camera.ScreenPointToRay(Input.mousePosition);
                Debug.DrawRay(ray.origin, ray.direction);
                if (Physics.Raycast(ray, out hit))
                {
                    mover = hit.collider.GetComponent<UIModMover>();
                }
            }
        }
        else
        {
            if(Input.GetMouseButton(0))
                mover.Grabbed(camera.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, distToMods)));
            if (Input.GetMouseButtonUp(0))
            {
                mover.Dropped();
                mover = null;
            }
        }
    }
}
