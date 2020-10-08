using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class MainController : MonoBehaviour
{
    // Start is called before the first frame update

    [FormerlySerializedAs("gobj")] public GameObject givenSpawned;

    private GivenMover gMoverScriptPart;
    
  
    private float keyDelay = 0.1f;
    private float timePassed =0f;

    private void Start()
    {
        
        gMoverScriptPart = givenSpawned.GetComponent<GivenMover>();
    }
 


    // todo deletei
    

    void Update()
    {
         
        
        // release object, testing todo delete
        if (Input.GetKeyDown(KeyCode.G))
        {
            gMoverScriptPart.canMove = true;
            givenSpawned.transform.position=new Vector3(0,0,0);
        }
        
       
        
        if (gMoverScriptPart.canMove == false)
        {
            return;
            //spawn new veya GivenMover içerisinde spawnNew Yapılabilir
        }
        timePassed += Time.deltaTime;
         
        
          
      
        // if (Input.GetKey(KeyCode.LeftArrow) && timePassed>=keyDelay)
        // {
        //       givenSpawned.transform.Translate(-1,0,0,Space.World);
        //      //gobj.transform.position = transform.position + new Vector3(-1, 0, 0);
        //  
        //     
        //     timePassed = 0f;
        // }
        
      

        
            //
            //
           
    
       
    }

   
    }

