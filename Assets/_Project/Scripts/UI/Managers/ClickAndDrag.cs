using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ClickAndDrag : MonoBehaviour
{
    private new Camera camera;
    public float distToMods;
    public TextMeshProUGUI flavourText;

    //public delegate void HoverCB();

    //public static HoverCB OnHover;

    void OnHover(UIModMover modMover)
    {
        var gunComp = modMover.GetComponentInChildren<GunComponent>();
        flavourText.text = "" ?? gunComp?.ToString();
    }

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
            
            RaycastHit hit;

            Ray ray = camera.ScreenPointToRay(Input.mousePosition);
            Debug.DrawRay(ray.origin, ray.direction);
            if (Physics.Raycast(ray, out hit))
            {
                var maybeMover = hit.collider.GetComponent<UIModMover>();
                if (maybeMover != null) OnHover(maybeMover);

                if (Input.GetMouseButtonDown(0))
                {
                    mover = maybeMover;
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
