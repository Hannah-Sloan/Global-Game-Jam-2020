using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Blank : MonoBehaviour
{
    public UIModMover resident;
    public Vector3 homePos;

    private void Awake()
    {
        homePos = transform.parent.position;
        resident = transform.parent.GetComponentInChildren<UIModMover>();
    }
}
