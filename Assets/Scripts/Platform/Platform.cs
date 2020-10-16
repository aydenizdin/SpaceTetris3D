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

    

   // Start is called before the first frame update
   void Start()
   {
     // GameObject instanceGrid = Instantiate(GridPref, Vector3.zero, GridPref.transform.rotation);

      PlatformDrawer pd = new PlatformDrawer(t3dConfiguration);
      
      ///instanceGrid.GetComponent<Renderer>().sharedMaterial.SetFloat("_LineSize"0.7f); // mesela erişim
      /// 
      linestorender=  pd.ConstructPlatformVertices(new PlatformDimensions()
      {
         
         Width = PlatDims.Width,
         Depth = PlatDims.Depth,
         Height = PlatDims.Height
      });
 
      CreateLineMaterial();
       
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
         GL.Vertex3(eachSegment.Start.x,eachSegment.Start.y,eachSegment.Start.z);
         // Another vertex at edge of circle
         GL.Vertex3(eachSegment.End.x,eachSegment.End.y,eachSegment.End.z);
      }
      
      GL.End();
      GL.PopMatrix();
   }
}