using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UIElements;


public class Platform : MonoBehaviour
{
   private List<PlatformLineSegment> linestorender; // todo temp bunu sil deneme gl line rendering

   /// <summary>
   /// ////////////
   /// </summary>
   [SerializeField] public PlatformDimensions PlatDims;


   public T3DConfiguration t3dConfiguration;

   public BoxCollider LeftBoundaryCollider;
   public BoxCollider RightBoundaryCollider;
   public BoxCollider TopBoundaryCollider;
   public BoxCollider BottomBoundaryCollider;
   public BoxCollider DepthBoundaryCollider;


   // Start is called before the first frame update
   void Start()
   {
      // GameObject instanceGrid = Instantiate(GridPref, Vector3.zero, GridPref.transform.rotation);

      PlatformDrawer pd = new PlatformDrawer(t3dConfiguration);

      ///instanceGrid.GetComponent<Renderer>().sharedMaterial.SetFloat("_LineSize"0.7f); // mesela erişim
      /// 
      linestorender = pd.ConstructPlatformVertices(new PlatformDimensions()
      {
         Width = PlatDims.Width,
         Depth = PlatDims.Depth,
         Height = PlatDims.Height
      });

      CreateLineMaterial();

      setBoundaryColliders();
   }


   private void setBoundaryColliders()
   {
      float halfunit = t3dConfiguration.OneUnit / 2.0f;
      // left boundary
      LeftBoundaryCollider.transform.position = new Vector3(-((PlatDims.Width / 2.0f) + halfunit), 0.0f, 0.0f);
      RightBoundaryCollider.transform.position = new Vector3(((PlatDims.Width / 2.0f) + halfunit), 0.0f, 0.0f);
      TopBoundaryCollider.transform.position = new Vector3(0.0f, ((PlatDims.Height / 2.0f) + halfunit), 0.0f);
      BottomBoundaryCollider.transform.position = new Vector3(0.0f, -((PlatDims.Height / 2.0f) + halfunit), 0.0f);
      DepthBoundaryCollider.transform.position = new Vector3(0.0f, 0.0f, PlatDims.Depth+halfunit);


      Vector3 left_centerposnew = new Vector3(-(PlatDims.Depth / 2.0f), 0.0f, 0.0f);
      LeftBoundaryCollider.center = left_centerposnew;
      Vector3 left_sizevector = new Vector3(PlatDims.Depth, PlatDims.Height, 1.0f);
      LeftBoundaryCollider.size = left_sizevector;

      // right boundary
      Vector3 right_centerposnew = new Vector3((PlatDims.Depth / 2.0f), 0.0f, 0.0f);
      RightBoundaryCollider.center = right_centerposnew;
      Vector3 right_sizevector = new Vector3(PlatDims.Depth, PlatDims.Height, 1.0f);
      RightBoundaryCollider.size = right_sizevector;

      // top boundary
      Vector3 top_centerposnew = new Vector3(0.0f, (PlatDims.Depth / 2.0f), 0.0f);
      TopBoundaryCollider.center = top_centerposnew;
      Vector3 top_sizevector = new Vector3(PlatDims.Width, PlatDims.Depth, 1.0f);
      TopBoundaryCollider.size = top_sizevector;

      // bottom boundary
      Vector3 bottom_centerposnew = new Vector3(0.0f, (PlatDims.Depth / 2.0f), 0.0f);
      BottomBoundaryCollider.center = bottom_centerposnew;
      Vector3 bottom_sizevector = new Vector3(PlatDims.Width, PlatDims.Depth, 1.0f);
      BottomBoundaryCollider.size = bottom_sizevector;

      // depth boundary
      Vector3 depth_centerposnew = new Vector3(0.0f, 0.0f, 0.0f);
      DepthBoundaryCollider.center = depth_centerposnew;
      Vector3 depth_sizevector = new Vector3(PlatDims.Width, PlatDims.Height, 1.0f);
      DepthBoundaryCollider.size = depth_sizevector;
   }


   // static Material lineMaterial;


   private void CreateLineMaterial()
   {
      // lineMaterial = GetComponent<Renderer>().material;
      // lineMaterial.hideFlags = HideFlags.HideAndDontSave;
      // lineMaterial.shader.hideFlags = HideFlags.HideAndDontSave;
      //
      //
      // lineMaterial.SetPass(0);
      //   mat.SetPass(0);
   }

   // Will be called after all regular rendering is done
   public void OnRenderObject()
   {
      // Apply the line material
      // lineMaterial.SetPass(0);

      //GetComponent<Renderer>().sharedMaterial.SetPass(0);
      GL.PushMatrix();
      // Set transformation matrix for drawing to
      // match our transform
      GL.MultMatrix(transform.localToWorldMatrix);

      // Draw lines
      GL.Begin(GL.LINES);
      // for (int segmentIndex = 1; segmentIndex < linestorender.Count; ++segmentIndex)
      foreach (PlatformLineSegment eachSegment in linestorender)
      {
         // GL.Color(Color.magenta);

         // One vertex at transform position
         GL.Vertex3(eachSegment.Start.x, eachSegment.Start.y, eachSegment.Start.z);
         // Another vertex at edge of circle
         GL.Vertex3(eachSegment.End.x, eachSegment.End.y, eachSegment.End.z);
      }

      GL.End();
      GL.PopMatrix();
   }
}