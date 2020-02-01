using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class UIModMover : MonoBehaviour
{
    private Blank home = null;
    private Blank potentialNewHome = null;
    public float radius = 5f;

    private void Awake()
    {
        home = transform.parent.GetComponentInChildren<Blank>();
    }

    public void Grabbed(Vector3 mouseWorldLoc)
    {
        transform.position = new Vector3(mouseWorldLoc.x, mouseWorldLoc.y, transform.position.z);
    }

    public void Dropped()
    {
        Update();
        if (potentialNewHome != null && potentialNewHome.resident == null)
        {
            home.resident = null;
            home = potentialNewHome;
            transform.parent = home.transform.parent;
            home.resident = this;
        }
        transform.localPosition = Vector3.zero;
    }

    private void Update()
    {
        potentialNewHome = ((Blank[])(FindObjectsOfType(typeof(Blank)))).
                            Where(blank => (transform.position - blank.transform.position).magnitude <= radius).
                            OrderBy(blank => (transform.position - blank.transform.position).sqrMagnitude).
                            FirstOrDefault();
    }
}
