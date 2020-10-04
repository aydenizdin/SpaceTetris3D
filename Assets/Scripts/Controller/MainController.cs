using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class MainController : MonoBehaviour
{
    // Start is called before the first frame update

    public GameObject gobj;
    
    
    
    private Space rotSpace;
    private float keyDelay = 0.1f;
    private float timePassed =0f;

    private void Start()
    {
        rotSpace = Space.World;
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

    

      void Update()
    {
        timePassed += Time.deltaTime;
         
        if (Input.GetKey(KeyCode.DownArrow) && Input.GetKey(KeyCode.LeftArrow) && timePassed>=keyDelay)
        {
            gobj.transform.Translate(-1,-1,0,Space.World);
            timePassed = 0f;
        }
          
        if (Input.GetKey(KeyCode.DownArrow) && timePassed>=keyDelay)
        {
            gobj.transform.Translate(0,-1,0,Space.World);
            timePassed = 0f;
        }
        
        
        if (Input.GetKey(KeyCode.UpArrow)&& timePassed>=keyDelay)
        {
            gobj.transform.Translate(0,1,0,Space.World);
            timePassed = 0f;
        }
        
        if (Input.GetKey(KeyCode.RightArrow) && timePassed>=keyDelay)
        {
            gobj.transform.Translate(1,0,0,Space.World);
            timePassed = 0f;
        }
        if (Input.GetKey(KeyCode.LeftArrow) && timePassed>=keyDelay)
        {
              gobj.transform.Translate(-1,0,0,Space.World);
             //gobj.transform.position = transform.position + new Vector3(-1, 0, 0);
         
            
            timePassed = 0f;
        }
        
        if (Input.GetKeyDown(KeyCode.M))
        {
            switchRotSpace();
        }
        if (Input.GetKeyDown(KeyCode.A))
        {
            gobj.transform.Rotate(0, 0, 90, rotSpace);
        }
        if (Input.GetKeyDown(KeyCode.D))
        {
            gobj.transform.Rotate(0,0,-90,rotSpace);
        }
        if (Input.GetKeyDown(KeyCode.S))
        {
            gobj.transform.Rotate(-90,0,0,rotSpace);
        }
        
        if (Input.GetKeyDown(KeyCode.W))
        {
            gobj.transform.Rotate(90,0,0,rotSpace);
        }

        
            //
            //
           
    
       
    }

   
    }

