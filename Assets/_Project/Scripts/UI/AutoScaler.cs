using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AutoScaler : MonoBehaviour
{

    public float UIwidth = 20;

    void Start()
    {
        float orthoSize = UIwidth * Screen.height / Screen.width * .5f;

        GetComponent<Camera>().orthographicSize = orthoSize;
    }
}
