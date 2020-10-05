using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

 
public class Platform : MonoBehaviour
{
     [SerializeField] 
     public PlatformDimensions PlatDims;
     public LineRenderer LinesRenderer;

     public T3DConfiguration t3dConfiguration;
    // Start is called before the first frame update
    void Start()
    {
       
        PlatformDrawer pd = new PlatformDrawer(t3dConfiguration, LinesRenderer);
        
        pd.DrawPlatform(new PlatformDimensions()
        {
            Width =PlatDims.Width,
            Depth = PlatDims.Depth,
            Height = PlatDims.Height
        });
    }

    
}
