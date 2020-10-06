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
    
    private Space rotSpace;
    private float keyDelay = 0.1f;
    private float timePassed =0f;

    private void Start()
    {
        rotSpace = Space.World;
        gMoverScriptPart = givenSpawned.GetComponent<GivenMover>();
    }
 
    
    private void switchRotSpace()
    {
        if (rotSpace == Space.Self)
        {
            rotSpace = Space.World;
        }
        else
        {
            rotSpace = Space.Self;
        }
    }

    // todo deletei
    

    void Update()
    {
        // release object, testing todo delete
        if (Input.GetKeyDown(KeyCode.R))
        {
            gMoverScriptPart.canMove = true;
        }
        
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
         
        if (Input.GetKey(KeyCode.DownArrow) && Input.GetKey(KeyCode.LeftArrow) && timePassed>=keyDelay)
        {
            givenSpawned.transform.Translate(-1,-1,0,Space.World);
            timePassed = 0f;
        }
          
        if (Input.GetKey(KeyCode.DownArrow) && timePassed>=keyDelay)
        {
            
            givenSpawned.transform.Translate(0,-1,0,Space.World);
            timePassed = 0f;
        }
        
        
        if (Input.GetKey(KeyCode.UpArrow)&& timePassed>=keyDelay)
        {
            givenSpawned.transform.Translate(0,1,0,Space.World);
            timePassed = 0f;
        }
        
        if (Input.GetKey(KeyCode.RightArrow) && timePassed>=keyDelay)
        {
            givenSpawned.transform.Translate(1,0,0,Space.World);
            timePassed = 0f;
        }
        if (Input.GetKey(KeyCode.LeftArrow) && timePassed>=keyDelay)
        {
              givenSpawned.transform.Translate(-1,0,0,Space.World);
             //gobj.transform.position = transform.position + new Vector3(-1, 0, 0);
         
            
            timePassed = 0f;
        }
        
        if (Input.GetKeyDown(KeyCode.M))
        {
            switchRotSpace();
        }
        if (Input.GetKeyDown(KeyCode.A))
        {
            givenSpawned.transform.Rotate(0, 0, 90, rotSpace);
        }
        if (Input.GetKeyDown(KeyCode.D))
        {
            givenSpawned.transform.Rotate(0,0,-90,rotSpace);
        }
        if (Input.GetKeyDown(KeyCode.S))
        {
            givenSpawned.transform.Rotate(-90,0,0,rotSpace);
        }
        
        if (Input.GetKeyDown(KeyCode.W))
        {
            givenSpawned.transform.Rotate(90,0,0,rotSpace);
        }

        
            //
            //
           
    
       
    }

   
    }

