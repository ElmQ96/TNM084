using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Function to create a continous frequency change with sinus function
public class ContinousFreq : MonoBehaviour {

    Renderer rend;
    public float Number;

    void Start()
    {
        //Get the renderer of the scene
        rend = GetComponent<Renderer>();
    }

    void Update()
    {
        //Change the variable for the material with a sinus function
        rend.material.SetFloat("_ClrFreq", Number+5.0f*Mathf.Sin(0.09f*Time.time));
        
    }
}
