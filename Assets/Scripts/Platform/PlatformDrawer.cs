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
      PlatformLineSegment[] rightSideFramePositions = ConstructSideFramesPositions_Right(dims);
      PlatformLineSegment[] bottomFrame = ConstructBottomFrame(dims);
      PlatformLineSegment[] bottomHorLines = ConstructBottomGrid(dims);
      // Vector3[] rightSideFramePositionsd = ConstructSideFramesPositions_Right(dims);
      // List<Vector3> bottomHorLines = ConstructBottomHorizLines(dims);
      // List<Vector3> bottomVertLines = ConstructBottomVertLines(dims);


      totalSegments.AddRange(startFrameSegments);
      totalSegments.AddRange(leftSideFramePositions);
      totalSegments.AddRange(rightSideFramePositions);
      totalSegments.AddRange(bottomFrame);
      totalSegments.AddRange(bottomHorLines);

      return totalSegments;
      // totalPositions.AddRange(leftSideFramePositions);
      // totalPositions.AddRange(rightSideFramePositionsd);
      // totalPositions.AddRange(bottomHorLines);
      // totalPositions.AddRange(bottomVertLines);
   }

   private PlatformLineSegment[] ConstructStartFramePositions(PlatformDimensions dims)
   {
      float unit = _conf.OneUnit;
      float yatayMove = unit * dims.Width / 2.0f;
      float dikeyMove = unit * dims.Height / 2.0f;

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

   private PlatformLineSegment[] ConstructBottomGrid(PlatformDimensions dims)
   {
      float unit = _conf.OneUnit;
      float derinlik = unit * dims.Depth;
      int howManyHorizontalLine = ((int) dims.Height / (int) unit) - 1;
      int howManyVerticalLine = ((int) dims.Width / (int) unit) - 1;
      
      PlatformLineSegment[] BottomTotalGrid = new PlatformLineSegment[howManyHorizontalLine+howManyVerticalLine];
      int totalIndex = 0;
      
      // taban frame'in sol ustu
      float solUstX = -(unit * dims.Width / 2.0f);
      float solUstY = (unit * dims.Height / 2.0f);

  
      
      // yatay segmentler
      float endX_H = solUstX + (unit * dims.Width);
      for (int horIndex = 1; horIndex <= howManyHorizontalLine; horIndex++)
      {
         float startY = solUstY - (horIndex * unit);
       

         PlatformLineSegment seg = new PlatformLineSegment()
         {
            Start = new PlatformVertex()
            {
               x = solUstX,
               y = startY,
               z = derinlik
            },
            End = new PlatformVertex()
            {
               x = endX_H,
               y = startY,
               z = derinlik
            }
         };
         BottomTotalGrid[totalIndex] = seg;
         totalIndex++;
      }
      //dikey segmentler

      float endY_V = solUstY - (unit * dims.Height);

      for (int vertIndex = 1; vertIndex <= howManyVerticalLine; vertIndex++)
      {
         float startX = solUstX + (vertIndex * unit);

         PlatformLineSegment seg = new PlatformLineSegment()
         {
            Start = new PlatformVertex()
            {
               x = startX,
               y = solUstY,
               z = derinlik
            },
            End = new PlatformVertex()
            {
               x = startX,
               y = endY_V,
               z = derinlik
            }
         };
         BottomTotalGrid[totalIndex] = seg;
         totalIndex++;
      }


      return BottomTotalGrid;

   }

   private PlatformLineSegment[] ConstructBottomFrame(PlatformDimensions dims)
   {
      float unit = _conf.OneUnit;
      float yatayMove = unit * dims.Width / 2.0f;
      float dikeyMove = unit * dims.Height / 2.0f;
      float derinlikMove = unit * dims.Depth;

      PlatformLineSegment[] BottomPoss = new PlatformLineSegment[2];

      PlatformVertex solUstStart = new PlatformVertex()
      {
         x = -yatayMove,
         y = dikeyMove,
         z = derinlikMove
      };

      PlatformVertex sagUst = new PlatformVertex()
      {
         x = yatayMove,
         y = dikeyMove,
         z = derinlikMove
      };

      PlatformVertex sagAlt = new PlatformVertex()
      {
         x = yatayMove,
         y = -dikeyMove,
         z = derinlikMove
      };

      PlatformVertex solAlt = new PlatformVertex()
      {
         x = -yatayMove,
         y = -dikeyMove,
         z = derinlikMove
      };

      PlatformLineSegment ust = new PlatformLineSegment()
      {
         Start = solUstStart,
         End = sagUst
      };


      PlatformLineSegment alt = new PlatformLineSegment()
      {
         Start = sagAlt,
         End = solAlt
      };


      BottomPoss[0] = ust;
      BottomPoss[1] = alt;


      return BottomPoss;
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

   private PlatformLineSegment[] ConstructSideFramesPositions_Right(PlatformDimensions dims)
   {
      float yatayMove = _conf.OneUnit * dims.Width / 2.0f;
      float dikeyMove = _conf.OneUnit * dims.Height / 2.0f;
      float derinlikMove = _conf.OneUnit * dims.Depth;


      PlatformLineSegment[] RightBoundsPoss =
      {
         new PlatformLineSegment()
         {
            Start = new PlatformVertex() {x = yatayMove, y = dikeyMove, z = 0.0f},
            End = new PlatformVertex() {x = yatayMove, y = dikeyMove, z = derinlikMove}
         },
         new PlatformLineSegment()
         {
            Start = new PlatformVertex() {x = yatayMove, y = dikeyMove, z = derinlikMove},
            End = new PlatformVertex() {x = yatayMove, y = -dikeyMove, z = derinlikMove}
         },
         new PlatformLineSegment()
         {
            Start = new PlatformVertex() {x = yatayMove, y = -dikeyMove, z = derinlikMove},
            End = new PlatformVertex() {x = yatayMove, y = -dikeyMove, z = 0.0f}
         }
      };


      return RightBoundsPoss;
   }
}