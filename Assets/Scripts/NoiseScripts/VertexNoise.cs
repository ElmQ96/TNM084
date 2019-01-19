using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VertexNoise : AudioSyncer {

    Renderer rend;

    public float beatScale;
    public float restScale;

    void Start()
    {
        rend = GetComponent<Renderer>();
    }

    void Update()
    {
        OnUpdate();

       //Debug.Log(rend.material.GetFloat("_Amnt"));
    }

     private IEnumerator MoveToScale(float _target)
     {
        //Get the current value of the material
        float _curr = rend.material.GetFloat("_Amnt");
        float _initial = _curr;
        float _timer = 0;

         while (_curr != _target)
         {
            //Lerp (mix) the initial value with the target beat value, depending on the timeToBeat value
            _curr = Mathf.Lerp(_initial, _target, _timer / timeToBeat);
             _timer += Time.deltaTime;

            //Set the new values to the variable
            rend.material.SetFloat("_Amnt", _curr);

             yield return null;
         }
         m_isBeat = false;
     }

     public override void OnUpdate()
     {
         base.OnUpdate();

         if (m_isBeat) return;

        //If no beat is being registrered, Lerp back to the initial value
        rend.material.SetFloat("_Amnt", Mathf.Lerp(rend.material.GetFloat("_Amnt"), restScale, restSmoothTime * Time.deltaTime));;
     }

     //Call the function from the parent class when a beat has occured
     public override void OnBeat()
     {
         base.OnBeat();

         StopCoroutine("MoveToScale");
         StartCoroutine("MoveToScale", beatScale);
     }
}