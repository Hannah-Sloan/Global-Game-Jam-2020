using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(ParticleSystem))]
public class Poof : MonoBehaviour
{
    ParticleSystem fx;
    [SerializeField] float timeCorrection = 0;
    // Start is called before the first frame update
    void Start()
    {
        Destroy(gameObject, fx.main.duration + timeCorrection);
    }
}
