#region

using System;
using System.Collections.Generic;
using JetBrains.Annotations;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Serialization;

#endregion

public class RotationAndPosition
{
   public Quaternion Rot { get; set; }
   public Vector3 Pos { get; set; }
}

public class GivenMover : MonoBehaviour
{
   [CanBeNull] public GameObject[] Cubes;
   public bool canMove = true;

  
   [CanBeNull] private Collider[] ChildrenColliders;
   private int childrenCount;
   private float lastTimeRun = 0.0f;
   private int DefaultLayerMask;
   private Ray sideMovementControlRay;
   private Ray RayForwardMoveControl;
   private Space rotSpace;
   private int RotationControlChildIndexStartValue = 1;
   
   private readonly Vector3 rotBackVector = new Vector3(-45.0f, 0, 0);
   private readonly Vector3 rotStepForwardVector = new Vector3(45.0f, 0, 0);
   private readonly Vector3 rotStepRightVector = new Vector3(0, 0, -45.0f);
   private readonly Vector3 rotStepScrewLeftVector = new Vector3(0, -45.0f, 0);
   private readonly Vector3 rotStepScrewRightVector = new Vector3(0, 45.0f, 0);
   private readonly Vector3 rotStepLeftVector = new Vector3(0, 0, 45.0f);

 //  private Action<Vector3,Vector3> RotateMe;
   
   private Quaternion startRotation = new Quaternion();

   private float waitBeforeMoveTime = 1.0f;

   //private Action drawContsForRotatingRight;
   public Vector3 LocalRotationCenterPosition;

   private Collider CenterCube;

   private float StepAngleValue = 45.0f;

   // private Vector3 getRotationAxisAround_Z_Custom_Rotation()
   // {
   //    { }
   //    return (transform.position + LocalRotationCenterPosition + Vector3.forward) - (transform.position + LocalRotationCenterPosition);
   // }

   private Vector3 RotationCenter
   {
      get
      {
         return transform.position + LocalRotationCenterPosition;
      }
      set
      {
         
      }
   }



   public void Start()
   {
      Debug.Log("this.name:" + name);
      // var s = GetComponent("GivenMover");
      // Destroy(s);
      RotationCenter = transform.position + LocalRotationCenterPosition;
    

      
      rotSpace = Space.World;
      childrenCount = Cubes.Length;
      RayForwardMoveControl = new Ray();
      RayForwardMoveControl.direction = Vector3.forward;
      setChildrenColliders(childrenCount);
      DefaultLayerMask = LayerMask.GetMask("Default");

      
   }
   
   private void setChildrenColliders(int size)
   {
      List<Collider> cList = new List<Collider>(size);

      foreach (GameObject cube in Cubes)
      {
         cList.Add(cube.GetComponent<Collider>());
      }

      ChildrenColliders = cList.ToArray();

      CenterCube = ChildrenColliders[0];
   }
   
   
   private bool ShouldStop()
   {
      for (int cindex = 0; cindex < childrenCount; cindex++)
      {
         GameObject childBoxEach = Cubes[cindex];

         RayForwardMoveControl.origin = Cubes[cindex].transform.position;

         // free fall
         bool isHit = Physics.Raycast(RayForwardMoveControl, 1.0f,DefaultLayerMask);

         if (isHit) // digerlerini bekleme dön hemen
         {
            //Debug.DrawRay(RayForwardMoveControl.origin, RayForwardMoveControl.direction, Color.green, 34444);
            return true;
         }
      }

      return false;
   }


   public void Update()
   {
      if (canMove == false)
      {
         return;
      }
   
   
      // release object, testing todo delete
   
   
      float now = Time.time;
      float timeDif = now - lastTimeRun;
      if (timeDif >= waitBeforeMoveTime)
      {
         if (ShouldStop())
         {
            canMove = false;
            return;
         }
   
         transform.Translate(0, 0, 1, Space.World);
         lastTimeRun = now;
      }
      
      
      
      if (Input.GetKeyDown(KeyCode.LeftArrow))
      {
         MoveLeft();
      }

      if (Input.GetKeyDown(KeyCode.DownArrow))
      {
         MoveDown();
      }


      if (Input.GetKeyDown(KeyCode.UpArrow))
      {
         MoveUp();
      }

      if (Input.GetKeyDown(KeyCode.RightArrow))
      {
         MoveRight();
      }

      if (Input.GetKeyDown(KeyCode.L))
      {
         transform.Translate(0, 0, -1, Space.World);
      }

      if (Input.GetKeyDown(KeyCode.O))
      {
         transform.Translate(0, 0, 1, Space.World);
      }

      if (Input.GetKeyDown(KeyCode.M))
      {
         switchRotSpace();
      }

      if (Input.GetKeyDown(KeyCode.A))
      {
       RotateAndAdjust_RotPointAtTranslated(Vector3.forward);
      }

      if (Input.GetKeyDown(KeyCode.D))
      {
         //RotateRight();
         RotateAndAdjust_RotPointAtTranslated(Vector3.back);
      }
      
      if (Input.GetKeyDown(KeyCode.S))
      {
         RotateAndAdjust_RotPointAtTranslated(Vector3.left);
      }

      if (Input.GetKeyDown(KeyCode.W))
      {
         RotateAndAdjust_RotPointAtTranslated(Vector3.right);
      }

      if (Input.GetKeyDown(KeyCode.Q))
      {
         RotateAndAdjust_RotPointAtTranslated(Vector3.down);
      }

      if (Input.GetKeyDown(KeyCode.E))
      {
         RotateAndAdjust_RotPointAtTranslated(Vector3.up);
      }
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

 
   private void RotateAndAdjust_RotPointAtTranslated(Vector3 aroundAxis) //stepRotateVector: defaultnull
   {
      Quaternion startingRot = transform.rotation;
      
      for (int iaci = 1; iaci <= 2; iaci++)
      {
         
        transform.RotateAround(RotationCenter,aroundAxis, StepAngleValue );
         
         for (int childIndex = RotationControlChildIndexStartValue; childIndex < childrenCount; childIndex++)
         {
            Collider childBoxCollider = ChildrenColliders[childIndex];

            var colls = Physics.OverlapBox(childBoxCollider.transform.position, new Vector3(0.3f, 0.3f, 0.3f), childBoxCollider.transform.rotation,
               DefaultLayerMask);
              
            if (colls.Length>0)
            {
               Debug.DrawLine(CenterCube.transform.position,childBoxCollider.transform.position,Color.red,10);
               
                 GameObject overlappedBox =Instantiate(childBoxCollider.gameObject,childBoxCollider.transform.position,childBoxCollider.transform.rotation);
                 overlappedBox.GetComponent<BoxCollider>().enabled = false;
             
               transform.rotation = startingRot;
               return;
            }
         }
      }
      
     
   }

   // spawner olarak MainController kullanılacak
   // MainController: given object spawn edecek
   // GivenMover (bu sınıf) given object kontrolü playerController olacak
   // sağa sola tuşlarını bu dinleyecek
   // moveleft moveright henüz yapılmadı, onlarda da raycast kontrol yapılabilir (trigger çözüm olmazsa)


   private void MoveLeft()
   {
      for (int cindex = 0; cindex < childrenCount; cindex++)
      {
         Collider c = ChildrenColliders[cindex];

         //c.transform.RotateAround(this.transform.position,Vector3.forward,-45.0f);
         sideMovementControlRay.origin = c.transform.position;
         sideMovementControlRay.direction = Vector3.left;
         bool hit = Physics.Raycast(sideMovementControlRay, 1.0f, DefaultLayerMask);

         if (hit)
         {
            //Debug.DrawRay(sideMovementControlRay.origin, sideMovementControlRay.direction, Color.green, 4);
            return;
         }
      }

      transform.Translate(-1, 0, 0, Space.World);
   }

   private void MoveDown()
   {
      for (int cindex = 0; cindex < childrenCount; cindex++)
      {
         Collider c = ChildrenColliders[cindex];

         //c.transform.RotateAround(this.transform.position,Vector3.forward,-45.0f);
         sideMovementControlRay.origin = c.transform.position;
         sideMovementControlRay.direction = Vector3.down;
         bool hit = Physics.Raycast(sideMovementControlRay, 1.0f, DefaultLayerMask);

         if (hit)
         {
         //   Debug.DrawRay(sideMovementControlRay.origin, sideMovementControlRay.direction, Color.green, 4);
            return;
         }
      }

      transform.Translate(0, -1, 0, Space.World);
   }

   private void MoveUp()
   {
      for (int cindex = 0; cindex < childrenCount; cindex++)
      {
         Collider c = ChildrenColliders[cindex];

         //c.transform.RotateAround(this.transform.position,Vector3.forward,-45.0f);
         sideMovementControlRay.origin = c.transform.position;
         sideMovementControlRay.direction = Vector3.up;
         bool hit = Physics.Raycast(sideMovementControlRay, 1.0f, DefaultLayerMask);

         if (hit)
         {
          //  Debug.DrawRay(sideMovementControlRay.origin, sideMovementControlRay.direction, Color.green, 4);
            return;
         }
      }

      transform.Translate(0, 1, 0, Space.World);
   }

   private void MoveRight()
   {
      for (int cindex = 0; cindex < childrenCount; cindex++)
      {
         Collider c = ChildrenColliders[cindex];

         //c.transform.RotateAround(this.transform.position,Vector3.forward,-45.0f);
         sideMovementControlRay.origin = c.transform.position;
         sideMovementControlRay.direction = Vector3.right;
         bool hit = Physics.Raycast(sideMovementControlRay, 1.0f, DefaultLayerMask);

         if (hit)
         {
          //  Debug.DrawRay(sideMovementControlRay.origin, sideMovementControlRay.direction, Color.green, 4);
            return;
         }
      }

      transform.Translate(1, 0, 0, Space.World);
   }

   

 
   

   private void RotateRight()
   {
      transform.Rotate(rotStepRightVector, rotSpace);
   }

   private void RotateForward()
   {
      transform.Rotate(rotStepForwardVector, rotSpace);
   }

   private void RotateBack()
   {
      //  transform.Rotate(-90, 0, 0, rotSpace);
      transform.Rotate(rotBackVector, rotSpace);
   }

   public void RotateScrewRight()
   {
      transform.Rotate(rotStepScrewRightVector, rotSpace);
   }

   public void RotateScrewLeft()
   {
      transform.Rotate(rotStepScrewLeftVector, rotSpace);
   }

   
}