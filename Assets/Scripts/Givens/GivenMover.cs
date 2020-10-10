using System;
using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using JetBrains.Annotations;
using UnityEditor;
using UnityEngine;
using UnityEngine.Serialization;

public class RotationAndPosition
{
   public Quaternion Rot { get; set; }
   public Vector3 Pos { get; set; }
}
public class GivenMover : MonoBehaviour
{
   [CanBeNull] public GameObject[] Cubes;

   [CanBeNull] private Collider[] ChildrenColliders;

   private float waitBeforeMoveTime = 1.0f;
   private float lastTimeRun = 0.0f;
   private int childrenCount;
   private Ray RayForwardMoveControl;
   private Ray myRay;
   public bool canMove = true;

   private RotationAndPosition LastSecureRotPost { get; set; }
   
   private int MovementControlLayerMaskValue;

   //private Action drawContsForRotatingRight;


   public void Awake()
   {
      LastSecureRotPost = new RotationAndPosition()
      {
         Pos = transform.position,
         Rot = transform.rotation
      };
   }

   public void Start()
   {
      
      Debug.Log("this.name:"+this.name);

      // if (name == "TCube_Parcali")
      // {
      //    drawContsForRotatingRight = drawContsFor_T_CubeRotateRight;
      // }
      // else if (name == "LCube_Parcali")
      // {
      //    drawContsForRotatingRight = drawContsFor_L_CubeRotateRight;
      // }
      //
      //
      rotSpace = Space.World;
      childrenCount = Cubes.Length;
      RayForwardMoveControl = new Ray();
      RayForwardMoveControl.direction = Vector3.forward;
      setChildrenColliders(childrenCount);
      MovementControlLayerMaskValue= LayerMask.GetMask("Default");
   }

   private void setChildrenColliders(int size)
   {
     List<Collider> cList = new List<Collider>(size);
      
      foreach (GameObject cube in Cubes)
      {
         cList.Add(cube.GetComponent<Collider>());    
      }

      ChildrenColliders = cList.ToArray();
   }
   
   
   private Space rotSpace;
   
       
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

   private void SetRotPosToTheLastSecure()
   {
      transform.rotation = LastSecureRotPost.Rot;
      transform.position = LastSecureRotPost.Pos;
   }

  

   // spawner olarak MainController kullanılacak
   // MainController: given object spawn edecek
   // GivenMover (bu sınıf) given object kontrolü playerController olacak
   // sağa sola tuşlarını bu dinleyecek
   // moveleft moveright henüz yapılmadı, onlarda da raycast kontrol yapılabilir (trigger çözüm olmazsa)

   private bool ShouldStop()
   {
      for (int cindex = 0; cindex < childrenCount; cindex++)
      {
         GameObject childBoxEach = Cubes[cindex];

         RayForwardMoveControl.origin = Cubes[cindex].transform.position;

         // free fall
         bool isHit = Physics.Raycast(RayForwardMoveControl, 1.0f);

         if (isHit == true) // digerlerini bekleme dön hemen
         {
            Debug.DrawRay(RayForwardMoveControl.origin, RayForwardMoveControl.direction, Color.green, 34444);
            return true;
         }
      }

      return false;
   }

   private void MoveLeft()
   {
      
      for (int cindex = 0; cindex < childrenCount; cindex++)
      {
         Collider c = ChildrenColliders[cindex];
         
         //c.transform.RotateAround(this.transform.position,Vector3.forward,-45.0f);
         myRay.origin = c.transform.position;
         myRay.direction = Vector3.left;
         bool hit = Physics.Raycast(myRay, 1.0f,MovementControlLayerMaskValue);

         if (hit)
         {
            Debug.DrawRay(myRay.origin, myRay.direction, Color.green, 4);
            return;
            
         }
         
      }
      
      transform.Translate(-1,0,0,Space.World);
   }
   
   private void MoveDown()
   {
      
      for (int cindex = 0; cindex < childrenCount; cindex++)
      {
         Collider c = ChildrenColliders[cindex];
         
         //c.transform.RotateAround(this.transform.position,Vector3.forward,-45.0f);
         myRay.origin = c.transform.position;
         myRay.direction = Vector3.down;
         bool hit = Physics.Raycast(myRay, 1.0f,MovementControlLayerMaskValue);

         if (hit)
         {
            Debug.DrawRay(myRay.origin, myRay.direction, Color.green, 4);
            return;
            
         }
         
      }
      
      transform.Translate(0,-1,0,Space.World);
   }
   
   private void MoveUp()
   {
      
      for (int cindex = 0; cindex < childrenCount; cindex++)
      {
         Collider c = ChildrenColliders[cindex];
         
         //c.transform.RotateAround(this.transform.position,Vector3.forward,-45.0f);
         myRay.origin = c.transform.position;
         myRay.direction = Vector3.up;
         bool hit = Physics.Raycast(myRay, 1.0f,MovementControlLayerMaskValue);

         if (hit)
         {
            Debug.DrawRay(myRay.origin, myRay.direction, Color.green, 4);
            return;
            
         }
         
      }
      
      transform.Translate(0,1,0,Space.World);
   }
   
   private void MoveRight()
   {
      
      for (int cindex = 0; cindex < childrenCount; cindex++)
      {
         Collider c = ChildrenColliders[cindex];
         
         //c.transform.RotateAround(this.transform.position,Vector3.forward,-45.0f);
         myRay.origin = c.transform.position;
         myRay.direction = Vector3.right;
         bool hit = Physics.Raycast(myRay, 1.0f,MovementControlLayerMaskValue);

         if (hit)
         {
            Debug.DrawRay(myRay.origin, myRay.direction, Color.green, 4);
            return;
            
         }
         
      }
      
      transform.Translate(1,0,0,Space.World);
   }

    
   
   private Vector3 rotLeftVector = new Vector3(0,0,90.0f);
   
    
   private void RotateLeftByControlEach()
   {
//transform.Rotate(rotLeftVector, rotSpace);
      int childIndexStart = 1; // todo dikkat center origin alınmıyor


      Quaternion startingRot = transform.rotation;
      
      float step = 22.5f;

      for (int iaci = 1; iaci <= 4; iaci++)
      {
         transform.Rotate(0,0,step,rotSpace);
         
         for (int childIndex = childIndexStart; childIndex < childrenCount; childIndex++)
         {
            Collider childBox = ChildrenColliders[childIndex];

         var colls=   Physics.OverlapBox(childBox.bounds.center,new Vector3(0.45f,0.45f,0.45f),childBox.transform.rotation,MovementControlLayerMaskValue);
         if (colls.Length > 0)
         {
            transform.rotation = startingRot;
         }
            
         }     
      }
    
       
      
   }

   private Vector3 rotRightVector = new Vector3(0,0,-90.0f);
   private void RotateRight()
   {   
       transform.Rotate(rotRightVector, rotSpace);
      
    
   }
   
   private Vector3 rotForwardVector = new Vector3(90,0,0);
   private void RotateForward()
   {
      
      
      transform.Rotate(rotForwardVector, rotSpace);
      
   
   }
   
   private Vector3 rotBackVector = new Vector3(-90,0,0);
   private void RotateBack()
   {
   
    //  transform.Rotate(-90, 0, 0, rotSpace);
    transform.Rotate(rotBackVector, rotSpace);
    
   }
 

   private Vector3 rotScrewRightVector = new Vector3(0,90.0f,0);
   public void RotateScrewRight()
   {
      transform.Rotate(rotScrewRightVector, rotSpace);
   }
   
   private Vector3 rotScrewLeftVector = new Vector3(0,-90.0f,0);
   
   public void RotateScrewLeft()
   {
      transform.Rotate(rotScrewLeftVector, rotSpace);
        
      
      
   }
   private Quaternion startRotation = new Quaternion();
   private Quaternion endRotation =new Quaternion();

   

   public void Update()
   {
     
      
      if (Input.GetKeyDown(KeyCode.LeftArrow))
      {
         LastSecureRotPost.Pos = transform.position;
         LastSecureRotPost.Rot = transform.rotation;
         MoveLeft();
      
      }
      if (Input.GetKeyDown(KeyCode.DownArrow) )
      {
         LastSecureRotPost.Pos = transform.position;
         LastSecureRotPost.Rot = transform.rotation;
        MoveDown();
      }
        
        
      if (Input.GetKeyDown(KeyCode.UpArrow) )
      {
         LastSecureRotPost.Pos = transform.position;
         LastSecureRotPost.Rot = transform.rotation;
         MoveUp();
      }
        
      if (Input.GetKeyDown(KeyCode.RightArrow)  )
      {
         LastSecureRotPost.Pos = transform.position;
         LastSecureRotPost.Rot = transform.rotation;
         MoveRight();
      }
      
      if (Input.GetKeyDown(KeyCode.L))
      {
         LastSecureRotPost.Pos = transform.position;
         LastSecureRotPost.Rot = transform.rotation;
         transform.Translate(0,0,-1,Space.World);
      
      }
      
      if (Input.GetKeyDown(KeyCode.O))
      {
         LastSecureRotPost.Pos = transform.position;
         LastSecureRotPost.Rot = transform.rotation;
         transform.Translate(0,0,1,Space.World);
      
      }
      
      if (Input.GetKeyDown(KeyCode.M))
      {
         switchRotSpace();
      }
      if (Input.GetKeyDown(KeyCode.A))
      {
        // transform.Rotate(0, 0, 90, rotSpace);
        LastSecureRotPost.Pos = transform.position;
        LastSecureRotPost.Rot = transform.rotation;
        RotateLeftByControlEach();
      }
      if (Input.GetKeyDown(KeyCode.D))
      {
         LastSecureRotPost.Pos = transform.position;
         LastSecureRotPost.Rot = transform.rotation;
         RotateRight();
      }
      if (Input.GetKeyDown(KeyCode.S))
      {
         LastSecureRotPost.Pos = transform.position;
         LastSecureRotPost.Rot = transform.rotation;
         RotateBack();
      }
        
      if (Input.GetKeyDown(KeyCode.W))
      {
         LastSecureRotPost.Pos = transform.position;
         LastSecureRotPost.Rot = transform.rotation;
        RotateForward();
      }
      
      if (Input.GetKeyDown(KeyCode.Q))
      {
         LastSecureRotPost.Pos = transform.position;
         LastSecureRotPost.Rot = transform.rotation;
         RotateScrewLeft();
      }
      
      if (Input.GetKeyDown(KeyCode.E))
      {
         LastSecureRotPost.Pos = transform.position;
         LastSecureRotPost.Rot = transform.rotation;
         RotateScrewRight();
      }
      
   }

   // public void Update()
   // {
   //    if (canMove == false)
   //    {
   //       return;
   //    }
   //
   //
   //    // release object, testing todo delete
   //
   //
   //    float now = Time.time;
   //    float timeDif = now - lastTimeRun;
   //    if (timeDif >= waitBeforeMoveTime)
   //    {
   //       if (ShouldStop())
   //       {
   //          canMove = false;
   //          return;
   //       }
   //
   //       transform.Translate(0, 0, 1, Space.World);
   //       lastTimeRun = now;
   //    }
   // }

 
}