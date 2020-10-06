using System;
using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using UnityEngine;
using UnityEngine.Serialization;

public class GivenMover : MonoBehaviour
{
   private enum HitDirection { None, Top, Bottom, Forward, Back, Left, Right }
   
   private HitDirection ReturnDirection( GameObject Object, GameObject ObjectHit ){
         
      HitDirection hitDirection = HitDirection.None;
      RaycastHit MyRayHit;
      Vector3 direction = ( Object.transform.position - ObjectHit.transform.position ).normalized;
      Ray MyRay = new Ray( ObjectHit.transform.position, direction );
         
      if ( Physics.Raycast( MyRay, out MyRayHit ) ){
                 
         if ( MyRayHit.collider != null ){
                 
            Vector3 MyNormal = MyRayHit.normal;
            MyNormal = MyRayHit.transform.TransformDirection( MyNormal );
                 
            if( MyNormal == MyRayHit.transform.up ){ hitDirection = HitDirection.Top; }
            if( MyNormal == -MyRayHit.transform.up ){ hitDirection = HitDirection.Bottom; }
            if( MyNormal == MyRayHit.transform.forward ){ hitDirection = HitDirection.Forward; }
            if( MyNormal == -MyRayHit.transform.forward ){ hitDirection = HitDirection.Back; }
            if( MyNormal == MyRayHit.transform.right ){ hitDirection = HitDirection.Right; }
            if( MyNormal == -MyRayHit.transform.right ){ hitDirection = HitDirection.Left; }
         }    
      }
      return hitDirection;
   }
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

    public float waitBeforeMoveTime = 1.0f;
    [FormerlySerializedAs("elapsedTime")] public float lastTimeRun = 0.0f;


    private GameObject[] Children;
    
    public void Start()
    {
      List<GameObject> 
       ChildrenGameObjects = new List<GameObject>(0);

       int cc = this.transform.childCount;

       for (int i = 0; i < cc; i++)
       {
          Transform child = this.transform.GetChild(i);
          ChildrenGameObjects.Add(child.gameObject);
       }

       Children = ChildrenGameObjects.ToArray();
    }
    
    //todo delete 
    private bool forceStop = false;
    
    

    private bool ShouldStop()
    {
       int cc = Children.Length;
       
       Ray MyRay = new Ray();
       
       for (int childIndex = 0; childIndex < cc; childIndex++)
       {
          GameObject childBoxEach = Children[childIndex];
           
          MyRay.origin = childBoxEach.transform.position;
          MyRay.direction = Vector3.forward;

          bool isHit = Physics.Raycast(MyRay, 1.0f);
          
          
          if (isHit == true) // digerlerini bekleme dön hemen
          {
             Debug.DrawRay(MyRay.origin,MyRay.direction,Color.green,34444);
             return true;
          }
       }

       return false;

    }

    public void Update()
    {
       if (Input.GetKeyDown(KeyCode.G))
       {
          forceStop = !forceStop;

       }
       
       if (forceStop == true)
       {
          return;
       }

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
          
          transform.Translate(0,0,1,Space.World);
          lastTimeRun = now;
       }
    }

    public bool canMove = true;
   

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
