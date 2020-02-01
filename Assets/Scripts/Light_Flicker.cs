using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Light_Flicker : MonoBehaviour
{
    public int[] intRange;
    public int cutoff;

    Light streetlamp;


    void Start()
    {
        streetlamp = this.GetComponent<Light>();
    }

    // Update is called once per frame
    void LateUpdate()
    {
        
    }
}
