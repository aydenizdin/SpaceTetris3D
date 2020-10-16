#region

using System;
using System.Collections.Generic;
using UnityEngine;

#endregion

public struct PlatformVertex
{
   public float x;
   public float y;
   public float z;
}

public struct PlatformLineSegment
{
   public PlatformVertex Start;
   public PlatformVertex End;
}

public class PlatformDrawer
{
   private readonly T3DConfiguration _conf;


   public PlatformDrawer(T3DConfiguration conf)
   {
      _conf = conf;
   }

  


   public List<PlatformLineSegment> ConstructPlatformVertices(PlatformDimensions dims)
   {
      //Test
      //     Vector3[] positions = new Vector3[3] { new Vector3(0, 0, 0), new Vector3(-1, 1, 0), new Vector3(1, 1, 0) };

      //_lineRenderer.loop = true;
      //   _lineRenderer.SetPositions(positions);

      List<PlatformLineSegment> totalSegments = new List<PlatformLineSegment>(0);

      PlatformLineSegment[] startFrameSegments = ConstructStartFramePositions(dims);
      
      PlatformLineSegment[] leftSideFramePositions = ConstructSideFramesPositions_Left(dims);
      // Vector3[] rightSideFramePositionsd = ConstructSideFramesPositions_Right(dims);
      // List<Vector3> bottomHorLines = ConstructBottomHorizLines(dims);
      // List<Vector3> bottomVertLines = ConstructBottomVertLines(dims);


      totalSegments.AddRange(startFrameSegments);
      totalSegments.AddRange(leftSideFramePositions);

      return totalSegments;
      // totalPositions.AddRange(leftSideFramePositions);
      // totalPositions.AddRange(rightSideFramePositionsd);
      // totalPositions.AddRange(bottomHorLines);
      // totalPositions.AddRange(bottomVertLines);
   }

   private PlatformLineSegment[] ConstructStartFramePositions(PlatformDimensions dims)
   {
      float unit = _conf.OneUnit;
      float yatayMove = _conf.OneUnit * dims.Width / 2.0f;
      float dikeyMove = _conf.OneUnit * dims.Height / 2.0f;

      PlatformLineSegment[] StartFramePoss = new PlatformLineSegment[4];

      PlatformVertex solUstStart = new PlatformVertex()
      {
         x = -yatayMove,
         y = dikeyMove,
         z = 0
      };

      PlatformVertex sagUst = new PlatformVertex()
      {
         x = yatayMove,
         y = dikeyMove,
         z = 0
      };

      PlatformVertex sagAlt = new PlatformVertex()
      {
         x = yatayMove,
         y = -dikeyMove,
         z = 0
      };

      PlatformVertex solAlt = new PlatformVertex()
      {
         x = -yatayMove,
         y = -dikeyMove,
         z = 0
      };

      PlatformLineSegment ust = new PlatformLineSegment()
      {
         Start = solUstStart,
         End = sagUst
      };

      PlatformLineSegment sag = new PlatformLineSegment()
      {
         Start = sagUst,
         End = sagAlt
      };

      PlatformLineSegment alt = new PlatformLineSegment()
      {
         Start = sagAlt,
         End = solAlt
      };

      PlatformLineSegment sol = new PlatformLineSegment()
      {
         Start = solAlt,
         End = solUstStart
      };

      StartFramePoss[0] = ust;
      StartFramePoss[1] = sag;
      StartFramePoss[2] = alt;
      StartFramePoss[3] = sol;


      return StartFramePoss;
   }


   private PlatformLineSegment[] ConstructSideFramesPositions_Left(PlatformDimensions dims)
   {
      float yatayMove = _conf.OneUnit * dims.Width / 2.0f;
      float dikeyMove = _conf.OneUnit * dims.Height / 2.0f;
      float derinlikMove = _conf.OneUnit * dims.Depth;
 

      PlatformLineSegment[] LeftBoundPositions =
      {
         new PlatformLineSegment()
         {
            Start = new PlatformVertex() {x = -yatayMove, y = dikeyMove, z = 0.0f},
            End = new PlatformVertex() {x = -yatayMove, y = dikeyMove, z = derinlikMove}
         },
         new PlatformLineSegment()
         {
            Start = new PlatformVertex() {x = -yatayMove, y = dikeyMove, z = derinlikMove},
            End = new PlatformVertex() {x = -yatayMove, y = -dikeyMove, z = derinlikMove}
         },
         new PlatformLineSegment()
         {
            Start = new PlatformVertex() {x = -yatayMove, y = -dikeyMove, z = derinlikMove},
            End = new PlatformVertex() {x = -yatayMove, y = -dikeyMove, z = 0.0f}
         }
      };
      
      

      return LeftBoundPositions;
   }

   private Vector3[] ConstructSideFramesPositions_Right(PlatformDimensions dims)
   {
      float yatayMove = _conf.OneUnit * dims.Width / 2.0f;
      float dikeyMove = _conf.OneUnit * dims.Height / 2.0f;
      float derinlikMove = _conf.OneUnit * dims.Depth;

      Vector3[] SideFrPoss =
      {
         new Vector3(yatayMove, -dikeyMove, 0),
         new Vector3(yatayMove, -dikeyMove, derinlikMove),
         new Vector3(yatayMove, dikeyMove, derinlikMove),
         new Vector3(yatayMove, dikeyMove, 0)
      };

      return SideFrPoss;
   }

   private List<Vector3> ConstructBottomHorizLines(PlatformDimensions dims)
   {
      float yatayMove = _conf.OneUnit * dims.Width / 2.0f;
      float dikeyMove = _conf.OneUnit * dims.Height / 2.0f;
      float derinlikMove = _conf.OneUnit * dims.Depth;


      List<Vector3> bottomPos = new List<Vector3>();
      bottomPos.Add(new Vector3(-yatayMove, dikeyMove, 0));
      bottomPos.Add(new Vector3(-yatayMove, dikeyMove, derinlikMove));

      for (int i = 0; i <= dims.Height; i++)
      {
         Vector3 soldakiPos = new Vector3(-yatayMove, dikeyMove - (_conf.OneUnit * i), derinlikMove);
         Vector3 sagdakiPos = new Vector3(yatayMove, dikeyMove - (_conf.OneUnit * i), derinlikMove);

         bottomPos.Add(soldakiPos);

         bottomPos.Add(sagdakiPos);

         bottomPos.Add(soldakiPos);
      }


      return bottomPos;
   }


   private List<Vector3> ConstructBottomVertLines(PlatformDimensions dims)
   {
      float yatayMove = _conf.OneUnit * dims.Width / 2.0f;
      float dikeyMove = _conf.OneUnit * dims.Height / 2.0f;
      float derinlikMove = _conf.OneUnit * dims.Depth;


      List<Vector3> bottomPos = new List<Vector3>();

      for (int i = 1; i < dims.Height; i++)
      {
         Vector3 yuk = new Vector3(-yatayMove + (_conf.OneUnit * i), dikeyMove, derinlikMove);
         Vector3 asag = new Vector3(-yatayMove + (_conf.OneUnit * i), -dikeyMove, derinlikMove);

         bottomPos.Add(yuk);

         bottomPos.Add(asag);

         bottomPos.Add(yuk);
      }


      return bottomPos;
   }
}