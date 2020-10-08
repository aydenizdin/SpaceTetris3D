using System;
using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using UnityEngine;
using UnityEngine.Serialization;

public class GivenMover : MonoBehaviour
{
   public GameObject[] Cubes;

   private Collider[] ChildrenColliders={};

   private float waitBeforeMoveTime = 1.0f;
   private float lastTimeRun = 0.0f;
   private int childrenCount;
   private Ray RayForwardMoveControl;
   private Ray myRay;
   public bool canMove = true;

   private int MovementControlLayerMaskValue;

   public void Start()
   {
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
   
   private void RotateLeft()
   {
      bool hit;
      for (int cindex = 0; cindex < childrenCount; cindex++)
      {
         Collider c = ChildrenColliders[cindex];
         
         //c.transform.RotateAround(this.transform.position,Vector3.forward,-45.0f);
         myRay.origin = c.transform.position;
         ////////////////////////////////////////////////////////////////////////////////////////////////////////////
         myRay.direction = Vector3.down;
         
         RaycastHit hitInfo = new RaycastHit();
         
         hit = Physics.Raycast(myRay,out hitInfo,1.0f,MovementControlLayerMaskValue);
         
         if (hit)
         {

            Vector3 AB = c.transform.position - this.transform.position;
            
            // var collidersClosestToMainorigin = hitInfo.collider.ClosestPoint(this.transform.position);
            var collidersClosestToMainorigin = hitInfo.collider.transform.position;
            
            
            Vector3 AC = collidersClosestToMainorigin - this.transform.position;

            Vector3 crossResult = Vector3.Cross(AC,AB);

            Debug.DrawLine(this.transform.position,this.transform.position + AB,Color.red,5);
            Debug.DrawLine(this.transform.position,this.transform.position + AC,Color.red,4);
            
            if (crossResult.z < 0.000001f)
            {
               
               return;   
            }
              
         }
         
         ////////////////////////////////////////////////////////////////////////////////////////////////////////////
         myRay.direction = Vector3.left;
         
         RaycastHit hitInfo2 = new RaycastHit();
         
         hit = Physics.Raycast(myRay,out hitInfo2,1.0f,MovementControlLayerMaskValue);
         
         if (hit)
         {

            Vector3 AB = c.transform.position - this.transform.position;
            
            // var collidersClosestToMainorigin = hitInfo2.collider.ClosestPoint(this.transform.position);

            var collidersClosestToMainorigin = hitInfo2.collider.transform.position;
            Vector3 AC = collidersClosestToMainorigin - this.transform.position;

            Vector3 crossResult = Vector3.Cross(AC,AB);

            Debug.DrawLine(this.transform.position,this.transform.position + AB,Color.red,5);
            Debug.DrawLine(this.transform.position,this.transform.position + AC,Color.red,4);
            
            if (crossResult.z < 0.000001f)
            {
               Debug.DrawRay(myRay.origin, myRay.direction, Color.green, 4);
               return;   
            }
              
         }
         
         ////////////////////////////////////////////////////////////////////////////////////////////////////////////
         myRay.direction = Vector3.right;
         
         RaycastHit hitInfo3 = new RaycastHit();
         Debug.DrawRay(myRay.origin, myRay.direction, Color.green, 4);
         
         hit = Physics.Raycast(myRay,out hitInfo3,1.0f,MovementControlLayerMaskValue);
         
         if (hit)
         {

            Vector3 AB = c.transform.position - this.transform.position;
            
            //var collidersClosestToMainorigin = hitInfo3.collider.ClosestPoint(this.transform.position);
            var collidersClosestToMainorigin = hitInfo3.collider.transform.position;

            Vector3 AC = collidersClosestToMainorigin - this.transform.position;

            Vector3 crossResult = Vector3.Cross(AC,AB);

            Debug.DrawLine(this.transform.position,this.transform.position + AB,Color.red,5);
            Debug.DrawLine(this.transform.position,this.transform.position + AC,Color.red,4);
            
            if (crossResult.z < 0.000001f)
            {
               Debug.DrawRay(myRay.origin, myRay.direction, Color.green, 4);
               return;   
            }
              
         }
         
         ////////////////////////////////////////////////////////////////////////////////////////////////////////////
          myRay.direction = Vector3.up;
         
         RaycastHit hitInfo4 = new RaycastHit();
         
         hit = Physics.Raycast(myRay,out hitInfo4,1.0f,MovementControlLayerMaskValue);
         
         if (hit)
         {

            Vector3 AB = c.transform.position - this.transform.position;
            
            // var collidersClosestToMainorigin = hitInfo4.collider.ClosestPoint(this.transform.position);
            var collidersClosestToMainorigin = hitInfo4.collider.transform.position;

            Vector3 AC = collidersClosestToMainorigin - this.transform.position;

            Vector3 crossResult = Vector3.Cross(AC,AB);

            Debug.DrawLine(this.transform.position,this.transform.position + AB,Color.red,5);
            Debug.DrawLine(this.transform.position,this.transform.position + AC,Color.red,4);
            
            
            
            if (crossResult.z < 0.000001f)
            {
               Debug.DrawRay(myRay.origin, myRay.direction, Color.green, 4);
               return;   
            }
              
         }
         
         ////////////////////////////////////////////////////////////////////////////////////////////////////////////
          
      }
      
      transform.Rotate(0, 0, 90, rotSpace);
   }

   
   private void RotateRight()
   {
      bool hit;
      for (int cindex = 0; cindex < childrenCount; cindex++)
      {
         Collider c = ChildrenColliders[cindex];
         
         //c.transform.RotateAround(this.transform.position,Vector3.forward,-45.0f);
         myRay.origin = c.transform.position;
         ////////////////////////////////////////////////////////////////////////////////////////////////////////////
         myRay.direction = Vector3.down;
         
         RaycastHit hitInfo = new RaycastHit();
         
         hit = Physics.Raycast(myRay,out hitInfo,1.0f,MovementControlLayerMaskValue);
         
         if (hit)
         {

            Vector3 AB = c.transform.position - this.transform.position;
            
            // var collidersClosestToMainorigin = hitInfo.collider.ClosestPoint(this.transform.position);
            var collidersClosestToMainorigin = hitInfo.collider.transform.position;
            
            
            Vector3 AC = collidersClosestToMainorigin - this.transform.position;

            Vector3 crossResult = Vector3.Cross(AB,AC);

            Debug.DrawLine(this.transform.position,this.transform.position + AB,Color.red,5);
            Debug.DrawLine(this.transform.position,this.transform.position + AC,Color.red,4);
            
            if (crossResult.z < 0.000001f)
            {
               
               return;   
            }
              
         }
         
         ////////////////////////////////////////////////////////////////////////////////////////////////////////////
         myRay.direction = Vector3.left;
         
         RaycastHit hitInfo2 = new RaycastHit();
         
         hit = Physics.Raycast(myRay,out hitInfo2,1.0f,MovementControlLayerMaskValue);
         
         if (hit)
         {

            Vector3 AB = c.transform.position - this.transform.position;
            
            // var collidersClosestToMainorigin = hitInfo2.collider.ClosestPoint(this.transform.position);

            var collidersClosestToMainorigin = hitInfo2.collider.transform.position;
            Vector3 AC = collidersClosestToMainorigin - this.transform.position;

            Vector3 crossResult = Vector3.Cross(AB,AC);

            Debug.DrawLine(this.transform.position,this.transform.position + AB,Color.red,5);
            Debug.DrawLine(this.transform.position,this.transform.position + AC,Color.red,4);
            
            if (crossResult.z < 0.000001f)
            {
               Debug.DrawRay(myRay.origin, myRay.direction, Color.green, 4);
               return;   
            }
              
         }
         
         ////////////////////////////////////////////////////////////////////////////////////////////////////////////
         myRay.direction = Vector3.right;
         
         RaycastHit hitInfo3 = new RaycastHit();
         Debug.DrawRay(myRay.origin, myRay.direction, Color.green, 4);
         
         hit = Physics.Raycast(myRay,out hitInfo3,1.0f,MovementControlLayerMaskValue);
         
         if (hit)
         {

            Vector3 AB = c.transform.position - this.transform.position;
            
            //var collidersClosestToMainorigin = hitInfo3.collider.ClosestPoint(this.transform.position);
            var collidersClosestToMainorigin = hitInfo3.collider.transform.position;

            Vector3 AC = collidersClosestToMainorigin - this.transform.position;

            Vector3 crossResult = Vector3.Cross(AB,AC);

            Debug.DrawLine(this.transform.position,this.transform.position + AB,Color.red,5);
            Debug.DrawLine(this.transform.position,this.transform.position + AC,Color.red,4);
            
            if (crossResult.z < 0.000001f)
            {
               Debug.DrawRay(myRay.origin, myRay.direction, Color.green, 4);
               return;   
            }
              
         }
         
         ////////////////////////////////////////////////////////////////////////////////////////////////////////////
          myRay.direction = Vector3.up;
         
         RaycastHit hitInfo4 = new RaycastHit();
         
         hit = Physics.Raycast(myRay,out hitInfo4,1.0f,MovementControlLayerMaskValue);
         
         if (hit)
         {

            Vector3 AB = c.transform.position - this.transform.position;
            
            // var collidersClosestToMainorigin = hitInfo4.collider.ClosestPoint(this.transform.position);
            var collidersClosestToMainorigin = hitInfo4.collider.transform.position;

            Vector3 AC = collidersClosestToMainorigin - this.transform.position;

            Vector3 crossResult = Vector3.Cross(AB,AC);

            Debug.DrawLine(this.transform.position,this.transform.position + AB,Color.red,5);
            Debug.DrawLine(this.transform.position,this.transform.position + AC,Color.red,4);
            
            
            
            if (crossResult.z < 0.000001f)
            {
               Debug.DrawRay(myRay.origin, myRay.direction, Color.green, 4);
               return;   
            }
              
         }
         
         ////////////////////////////////////////////////////////////////////////////////////////////////////////////
          
      }
      
      transform.Rotate(0, 0, -90, rotSpace);
   }
   
   private void RotateForward()
   {
      bool hit;
      for (int cindex = 0; cindex < childrenCount; cindex++)
      {
         Collider c = ChildrenColliders[cindex];
         
         //c.transform.RotateAround(this.transform.position,Vector3.forward,-45.0f);
         myRay.origin = c.transform.position;
         
         
         myRay.direction = Vector3.forward;
         hit = Physics.Raycast(myRay, 1.0f,MovementControlLayerMaskValue);

         if (hit)
         {
            Debug.DrawRay(myRay.origin, myRay.direction, Color.green, 4);
            return;
            
         }
         
         myRay.direction = Vector3.back;
         hit = Physics.Raycast(myRay, 1.0f,MovementControlLayerMaskValue);

         if (hit)
         {
            Debug.DrawRay(myRay.origin, myRay.direction, Color.green, 4);
            return;
            
         }

         myRay.direction = Vector3.up;
         hit = Physics.Raycast(myRay, 1.0f,MovementControlLayerMaskValue);

         if (hit)
         {
            Debug.DrawRay(myRay.origin, myRay.direction, Color.green, 4);
            return;
            
         }

         
         myRay.direction = Vector3.down;
         hit = Physics.Raycast(myRay, 1.0f,MovementControlLayerMaskValue);

         if (hit)
         {
            Debug.DrawRay(myRay.origin, myRay.direction, Color.green, 4);
            return;
            
         }

     
         
            

         
      }
      
      transform.Rotate(90, 0, 0, rotSpace);
   }
   
   
   private void RotateBack()
   {
      bool hit;
      for (int cindex = 0; cindex < childrenCount; cindex++)
      {
         Collider c = ChildrenColliders[cindex];
         
         //c.transform.RotateAround(this.transform.position,Vector3.forward,-45.0f);
         myRay.origin = c.transform.position;
         
         
         myRay.direction = Vector3.back;
         hit = Physics.Raycast(myRay, 1.0f,MovementControlLayerMaskValue);

         if (hit)
         {
            Debug.DrawRay(myRay.origin, myRay.direction, Color.green, 4);
            return;
            
         }
         
         myRay.direction = Vector3.forward;
         hit = Physics.Raycast(myRay, 1.0f,MovementControlLayerMaskValue);

         if (hit)
         {
            Debug.DrawRay(myRay.origin, myRay.direction, Color.green, 4);
            return;
            
         }
         
        

         myRay.direction = Vector3.up;
         hit = Physics.Raycast(myRay, 1.0f,MovementControlLayerMaskValue);

         if (hit)
         {
            Debug.DrawRay(myRay.origin, myRay.direction, Color.green, 4);
            return;
            
         }

         
         myRay.direction = Vector3.down;
         hit = Physics.Raycast(myRay, 1.0f,MovementControlLayerMaskValue);

         if (hit)
         {
            Debug.DrawRay(myRay.origin, myRay.direction, Color.green, 4);
            return;
            
         }

     
         
            

         
      }
      
      transform.Rotate(-90, 0, 0, rotSpace);
   }



   public void Update()
   {
      if (Input.GetKeyDown(KeyCode.LeftArrow))
      {
         MoveLeft();
      
      }
      if (Input.GetKeyDown(KeyCode.DownArrow) )
      {
            
       MoveDown();
      }
        
        
      if (Input.GetKeyDown(KeyCode.UpArrow) )
      {
         MoveUp();
      }
        
      if (Input.GetKeyDown(KeyCode.RightArrow)  )
      {
         MoveRight();
      }
      
      if (Input.GetKeyDown(KeyCode.L))
      {
         transform.Translate(0,0,-1,Space.World);
      
      }
      
      if (Input.GetKeyDown(KeyCode.O))
      {
         transform.Translate(0,0,1,Space.World);
      
      }
      
      if (Input.GetKeyDown(KeyCode.M))
      {
         switchRotSpace();
      }
      if (Input.GetKeyDown(KeyCode.A))
      {
        // transform.Rotate(0, 0, 90, rotSpace);
        RotateLeft();
      }
      if (Input.GetKeyDown(KeyCode.D))
      {
       RotateRight();
      }
      if (Input.GetKeyDown(KeyCode.S))
      {
         RotateBack();
      }
        
      if (Input.GetKeyDown(KeyCode.W))
      {
        RotateForward();
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


   // private void OnTriggerEnter(Collider other)
   // {
   //
   //    //other.transform.root.child => carptiginin kupleri
   //   HitDirection carptigi = ReturnDirection(other.gameObject, this.gameObject);
   //   HitDirection buRigidBodysiOlan = ReturnDirection(this.gameObject,other.gameObject);
   //   canMove = false;
   //
   // }
}