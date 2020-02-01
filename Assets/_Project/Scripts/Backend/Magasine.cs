using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Magasine : MonoBehaviour
{
    [SerializeField]
    MagasineComponent[] components = new MagasineComponent[Gun.COMPONENT_NUMBER];

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public Bullet GetBullet()
    {
        return null;
    }
}
