﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class MainController : MonoBehaviour
{
   // Start is called before the first frame update

   public GameObject givenSpawned;

   private GivenMover gMoverScriptPart;

 

   private void Start()
   {
      gMoverScriptPart = givenSpawned.GetComponent<GivenMover>();
   }
}