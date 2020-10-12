using System;
using System.Collections.Generic;
using System.Linq;
using UnityEditor.Experimental.GraphView;
using UnityEngine;

public class PlatformDrawer
{
   private LineRenderer _lineRenderer;
   private T3DConfiguration _conf;
   
   public PlatformDrawer(T3DConfiguration conf, LineRenderer lRenderer)
   {
      _conf = conf;
      _lineRenderer = lRenderer;
      _lineRenderer.widthMultiplier = 0.01f;
   }

   public void DrawPlatform(PlatformDimensions dims)
   {
      //Test
      //     Vector3[] positions = new Vector3[3] { new Vector3(0, 0, 0), new Vector3(-1, 1, 0), new Vector3(1, 1, 0) };

      //_lineRenderer.loop = true;
      //   _lineRenderer.SetPositions(positions);

      List<Vector3> totalPositions = new List<Vector3>(0);

      Vector3[] startFramePositions = ConstructStartFramePositions(dims);
      Vector3[] leftSideFramePositions = ConstructSideFramesPositions_Left(dims);
      Vector3[] rightSideFramePositionsd = ConstructSideFramesPositions_Right(dims);
      List<Vector3> bottomLines = ConstructBottomLines(dims);
      
      totalPositions.AddRange(startFramePositions);
      totalPositions.AddRange(leftSideFramePositions);
      totalPositions.AddRange(rightSideFramePositionsd);
      totalPositions.AddRange(bottomLines);
      
      _lineRenderer.positionCount = totalPositions.Count;
      _lineRenderer.SetPositions(totalPositions.ToArray());
      
      
   }

   private  Vector3[]ConstructStartFramePositions(PlatformDimensions dims)
   {
      float yatayMove = _conf.OneUnit * dims.Width / 2.0f;
      float dikeyMove = _conf.OneUnit * dims.Height / 2.0f;

      Vector3[] StartFramePoss = new Vector3[]
      {
         new Vector3(-yatayMove, dikeyMove, 0),
         new Vector3(yatayMove, dikeyMove, 0),
         new Vector3(yatayMove, -dikeyMove, 0),
         new Vector3(-yatayMove, -dikeyMove, 0),
         //new Vector3(-yatayMove, dikeyMove, 0) // start frame ile çakışıyor, kaldırıldı
      };

      return StartFramePoss;
   }

   private  Vector3[] ConstructSideFramesPositions_Left(PlatformDimensions dims)
   {
      float yatayMove = _conf.OneUnit * dims.Width / 2.0f;
      float dikeyMove = _conf.OneUnit * dims.Height / 2.0f;
      float derinlikMove = _conf.OneUnit * dims.Depth;
      
      Vector3[] SideFrPoss = new Vector3[]
      {
         new Vector3(-yatayMove, dikeyMove, 0),
         new Vector3(-yatayMove, dikeyMove, derinlikMove),
         new Vector3(-yatayMove, -dikeyMove, derinlikMove),
         new Vector3(-yatayMove, -dikeyMove, 0),
         new Vector3(-yatayMove, dikeyMove, 0)
      };

      return SideFrPoss;
   }
   
   private  Vector3[] ConstructSideFramesPositions_Right(PlatformDimensions dims)
   {
      float yatayMove = _conf.OneUnit * dims.Width / 2.0f;
      float dikeyMove = _conf.OneUnit * dims.Height / 2.0f;
      float derinlikMove = _conf.OneUnit * dims.Depth;
      
      Vector3[] SideFrPoss = new Vector3[]
      {
         new Vector3(yatayMove, dikeyMove, 0),
         new Vector3(yatayMove, dikeyMove, derinlikMove),
         new Vector3(yatayMove, -dikeyMove, derinlikMove),
         new Vector3(yatayMove, -dikeyMove, 0),
       //  new Vector3(yatayMove, dikeyMove, 0)
      };

      return SideFrPoss;
   }
   
   private    List<Vector3>  ConstructBottomLines(PlatformDimensions dims)
   {
      float yatayMove = _conf.OneUnit * dims.Width / 2.0f;
      float dikeyMove = _conf.OneUnit * dims.Height / 2.0f;
      float derinlikMove = _conf.OneUnit * dims.Depth;
      
      
         
      List<Vector3> bottomPos = new List<Vector3>();

      for (int i = 0; i <= dims.Height; i++)
      {
         Vector3 soldakiPos = new Vector3(-yatayMove , dikeyMove-(_conf.OneUnit*i), derinlikMove);
         Vector3 sagdakiPos = new Vector3(yatayMove, dikeyMove-(_conf.OneUnit*i), derinlikMove);
         
         bottomPos.Add(soldakiPos);
    
         bottomPos.Add(sagdakiPos);
         
      //   bottomPos.Add(soldakiPos);
      }
            

      return bottomPos;
   }
   
}