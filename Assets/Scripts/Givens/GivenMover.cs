using System;
using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using UnityEngine;
using UnityEngine.Serialization;

public class GivenMover : MonoBehaviour
{
    // // Start is called before the first frame update
    // void Start()
    // {
    //     
    // }
    //
    // // Update is called once per frame
    // void Update()
    // {
    //     
    // }

    public bool canMove = true;
   

      private void OnTriggerEnter(Collider other)
      {


        canMove = false;

      }

      // private void OnTriggerStay(Collider other)
      // {
      //    throw new NotImplementedException();
      // }
}
