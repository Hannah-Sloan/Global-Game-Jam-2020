using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIModMover : MonoBehaviour
{
    private bool untouched = true;
    private bool follow = true;
    private bool homeTravel = true;
    private UIModMover home = null;

    public void Grabbed(Vector3 mouseWorldLoc)
    {
        transform.position = new Vector3(mouseWorldLoc.x, mouseWorldLoc.y, transform.position.z);
    }
}
