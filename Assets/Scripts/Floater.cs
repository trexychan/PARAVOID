using UnityEngine;
using System;

//Modified from http://www.donovankeith.com/2016/05/making-objects-float-up-down-in-unity/
 // Makes objects float up & down while gently spinning.
public class Floater : MonoBehaviour {
     // User Inputs
     public float degreesPerSecond = 15.0f;
     public float amplitude = 0.5f;
     public float frequency = 1f;

     [NonSerialized] public float randomOffset;
     
     // Position Storage Variables
     Vector3 posOffset = new Vector3 ();
     Vector3 tempPos = new Vector3 ();

     // Use this for initialization
     void Start () {
         // Store the starting position & rotation of the object
         posOffset = transform.position;
         var rand = new System.Random();
         randomOffset = (float) (rand.NextDouble()-0.5);
         degreesPerSecond += randomOffset*2;
         amplitude += randomOffset;
         frequency += randomOffset;
     }
      
     // Update is called once per frame
     void Update () {
         // Spin object around Y-Axis
         transform.Rotate(new Vector3(0f, Time.deltaTime * degreesPerSecond+randomOffset/2, 0f), Space.World);

         // Float up/down with a Sin()
         tempPos = posOffset;
         tempPos.y += Mathf.Sin (Time.fixedTime * Mathf.PI * frequency+randomOffset) * amplitude;

         transform.position = tempPos;
     }
}