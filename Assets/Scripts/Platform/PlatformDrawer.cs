#region

using System.Collections.Generic;

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
    

      List<PlatformLineSegment> totalSegments = new List<PlatformLineSegment>(0);

      PlatformLineSegment[] startFrameSegments = ConstructStartFramePositions(dims);

      PlatformLineSegment[] leftSideFramePositions = ConstructSideFramesPositions_Left(dims);
      PlatformLineSegment[] rightSideFramePositions = ConstructSideFramesPositions_Right(dims);
      PlatformLineSegment[] bottomFrame = ConstructBottomFrame(dims);
      PlatformLineSegment[] bottomGridLines = ConstructBottomGrid(dims);
      PlatformLineSegment[] leftSideGridLines = ConstructLeftSideGrid(dims);
      PlatformLineSegment[] rightSideGridLines = ConstructRighSideGrid(dims);
      PlatformLineSegment[] rightUpGridLines = ConstructUpSideGrid(dims);
      PlatformLineSegment[] rightDownGridLines = ConstructDownSideGrid(dims);
     

      totalSegments.AddRange(startFrameSegments);
      totalSegments.AddRange(leftSideFramePositions);
      totalSegments.AddRange(rightSideFramePositions);
      totalSegments.AddRange(bottomFrame);
      totalSegments.AddRange(bottomGridLines);
      totalSegments.AddRange(leftSideGridLines);
      totalSegments.AddRange(rightSideGridLines);
      totalSegments.AddRange(rightUpGridLines);
      totalSegments.AddRange(rightDownGridLines);

      return totalSegments;
   
   }


   private PlatformLineSegment[] ConstructStartFramePositions(PlatformDimensions dims)
   {
      float unit = _conf.OneUnit;
      float yatayMove = unit * dims.Width / 2.0f;
      float dikeyMove = unit * dims.Height / 2.0f;

      PlatformLineSegment[] StartFramePoss = new PlatformLineSegment[4];

      PlatformVertex solUstStart = new PlatformVertex
      {
         x = -yatayMove,
         y = dikeyMove,
         z = 0
      };

      PlatformVertex sagUst = new PlatformVertex
      {
         x = yatayMove,
         y = dikeyMove,
         z = 0
      };

      PlatformVertex sagAlt = new PlatformVertex
      {
         x = yatayMove,
         y = -dikeyMove,
         z = 0
      };

      PlatformVertex solAlt = new PlatformVertex
      {
         x = -yatayMove,
         y = -dikeyMove,
         z = 0
      };

      PlatformLineSegment ust = new PlatformLineSegment
      {
         Start = solUstStart,
         End = sagUst
      };

      PlatformLineSegment sag = new PlatformLineSegment
      {
         Start = sagUst,
         End = sagAlt
      };

      PlatformLineSegment alt = new PlatformLineSegment
      {
         Start = sagAlt,
         End = solAlt
      };

      PlatformLineSegment sol = new PlatformLineSegment
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
      int howManyHorizontalLine = (dims.Height / (int) unit) - 1;
      int howManyVerticalLine = (dims.Width / (int) unit) - 1;

      PlatformLineSegment[] BottomTotalGrid = new PlatformLineSegment[howManyHorizontalLine + howManyVerticalLine];
      int totalIndex = 0;

      // taban frame'in sol ustu
      float solUstX = -(unit * dims.Width / 2.0f);
      float solUstY = (unit * dims.Height / 2.0f);


      // yatay segmentler
      float endX_H = solUstX + (unit * dims.Width);
      for (int horIndex = 1; horIndex <= howManyHorizontalLine; horIndex++)
      {
         float startY = solUstY - (horIndex * unit);


         PlatformLineSegment seg = new PlatformLineSegment
         {
            Start = new PlatformVertex
            {
               x = solUstX,
               y = startY,
               z = derinlik
            },
            End = new PlatformVertex
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

         PlatformLineSegment seg = new PlatformLineSegment
         {
            Start = new PlatformVertex
            {
               x = startX,
               y = solUstY,
               z = derinlik
            },
            End = new PlatformVertex
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

      PlatformVertex solUstStart = new PlatformVertex
      {
         x = -yatayMove,
         y = dikeyMove,
         z = derinlikMove
      };

      PlatformVertex sagUst = new PlatformVertex
      {
         x = yatayMove,
         y = dikeyMove,
         z = derinlikMove
      };

      PlatformVertex sagAlt = new PlatformVertex
      {
         x = yatayMove,
         y = -dikeyMove,
         z = derinlikMove
      };

      PlatformVertex solAlt = new PlatformVertex
      {
         x = -yatayMove,
         y = -dikeyMove,
         z = derinlikMove
      };

      PlatformLineSegment ust = new PlatformLineSegment
      {
         Start = solUstStart,
         End = sagUst
      };


      PlatformLineSegment alt = new PlatformLineSegment
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
         new PlatformLineSegment
         {
            Start = new PlatformVertex {x = -yatayMove, y = dikeyMove, z = 0.0f},
            End = new PlatformVertex {x = -yatayMove, y = dikeyMove, z = derinlikMove}
         },
         new PlatformLineSegment
         {
            Start = new PlatformVertex {x = -yatayMove, y = dikeyMove, z = derinlikMove},
            End = new PlatformVertex {x = -yatayMove, y = -dikeyMove, z = derinlikMove}
         },
         new PlatformLineSegment
         {
            Start = new PlatformVertex {x = -yatayMove, y = -dikeyMove, z = derinlikMove},
            End = new PlatformVertex {x = -yatayMove, y = -dikeyMove, z = 0.0f}
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
         new PlatformLineSegment
         {
            Start = new PlatformVertex {x = yatayMove, y = dikeyMove, z = 0.0f},
            End = new PlatformVertex {x = yatayMove, y = dikeyMove, z = derinlikMove}
         },
         new PlatformLineSegment
         {
            Start = new PlatformVertex {x = yatayMove, y = dikeyMove, z = derinlikMove},
            End = new PlatformVertex {x = yatayMove, y = -dikeyMove, z = derinlikMove}
         },
         new PlatformLineSegment
         {
            Start = new PlatformVertex {x = yatayMove, y = -dikeyMove, z = derinlikMove},
            End = new PlatformVertex {x = yatayMove, y = -dikeyMove, z = 0.0f}
         }
      };


      return RightBoundsPoss;
   }
   
   private PlatformLineSegment[] ConstructLeftSideGrid(PlatformDimensions dims)
   {
      float unit = _conf.OneUnit;
      float derinlik = unit * dims.Depth;
      int howManyHorizontalLine = (dims.Height / (int) unit) - 1;
      int howManyVerticalLine = (dims.Depth / (int) unit) - 1;

      PlatformLineSegment[] TotalLeftSideGrid = new PlatformLineSegment[howManyHorizontalLine + howManyVerticalLine];
      int totalIndex = 0;

      // taban frame'in sol ustu
      float solUstX = -(unit * dims.Width / 2.0f);
      float solUstY = (unit * dims.Height / 2.0f);
       
      for (int horIndex = 1; horIndex <= howManyHorizontalLine; horIndex++)
      {
         float startY = solUstY - (horIndex * unit);


         PlatformLineSegment seg = new PlatformLineSegment
         {
            Start = new PlatformVertex
            {
               x = solUstX,
               y = startY,
               z = 0
            },
            End = new PlatformVertex
            {
               x = solUstX,
               y = startY,
               z = derinlik
            }
         };
         TotalLeftSideGrid[totalIndex] = seg;
         totalIndex++;
      }
      //dikey segmentler

      float endY_V = solUstY - (unit * dims.Height);

      for (int vertIndex = 1; vertIndex <= howManyVerticalLine; vertIndex++)
      {
         float startZEndZ =  (vertIndex * unit);

         PlatformLineSegment seg = new PlatformLineSegment
         {
            Start = new PlatformVertex
            {
               x = solUstX,
               y = solUstY,
               z = startZEndZ
            },
            End = new PlatformVertex
            {
               x = solUstX,
               y = endY_V,
               z = startZEndZ
            }
         };
         TotalLeftSideGrid[totalIndex] = seg;
         totalIndex++;
      }


      return TotalLeftSideGrid;
   }

   private PlatformLineSegment[] ConstructRighSideGrid(PlatformDimensions dims)
   {
      float unit = _conf.OneUnit;
      float derinlik = unit * dims.Depth;
      int howManyHorizontalLine = (dims.Height / (int) unit) - 1;
      int howManyVerticalLine = (dims.Depth / (int) unit) - 1;

      PlatformLineSegment[] TotalLeftSideGrid = new PlatformLineSegment[howManyHorizontalLine + howManyVerticalLine];
      int totalIndex = 0;

      // taban frame'in sol ustu
      float sagUstX = (unit * dims.Width / 2.0f);
      float sagUstY = (unit * dims.Height / 2.0f);
       
      for (int horIndex = 1; horIndex <= howManyHorizontalLine; horIndex++)
      {
         float startY = sagUstY - (horIndex * unit);


         PlatformLineSegment seg = new PlatformLineSegment
         {
            Start = new PlatformVertex
            {
               x = sagUstX,
               y = startY,
               z = 0
            },
            End = new PlatformVertex
            {
               x = sagUstX,
               y = startY,
               z = derinlik
            }
         };
         TotalLeftSideGrid[totalIndex] = seg;
         totalIndex++;
      }
      //dikey segmentler

      float endY_V = sagUstY - (unit * dims.Height);

      for (int vertIndex = 1; vertIndex <= howManyVerticalLine; vertIndex++)
      {
         float startZEndZ =  (vertIndex * unit);

         PlatformLineSegment seg = new PlatformLineSegment
         {
            Start = new PlatformVertex
            {
               x = sagUstX,
               y = sagUstY,
               z = startZEndZ
            },
            End = new PlatformVertex
            {
               x = sagUstX,
               y = endY_V,
               z = startZEndZ
            }
         };
         TotalLeftSideGrid[totalIndex] = seg;
         totalIndex++;
      }


      return TotalLeftSideGrid;
   }
   private PlatformLineSegment[] ConstructUpSideGrid(PlatformDimensions dims)
   {
      float unit = _conf.OneUnit;
      float derinlik = unit * dims.Depth;
      int howManyHorizontalLine = (dims.Depth / (int) unit) - 1;
      int howManyVerticalLine = (dims.Height / (int) unit) - 1;

      PlatformLineSegment[] TotalLeftSideGrid = new PlatformLineSegment[howManyHorizontalLine + howManyVerticalLine];
      int totalIndex = 0;

      // taban frame'in sol ustu
      float solUstX = -(unit * dims.Width / 2.0f);
      float sagUstX = (unit * dims.Width / 2.0f);
      float solUstY = (unit * dims.Height / 2.0f);
       
      for (int horIndex = 1; horIndex <= howManyHorizontalLine; horIndex++)
      {
         float der =  (horIndex * unit);


         PlatformLineSegment seg = new PlatformLineSegment
         {
            Start = new PlatformVertex
            {
               x = solUstX,
               y = solUstY,
               z = der
            },
            End = new PlatformVertex
            {
               x = sagUstX,
               y = solUstY,
               z = der
            }
         };
         TotalLeftSideGrid[totalIndex] = seg;
         totalIndex++;
      }
      //dikey segmentler

       

      for (int vertIndex = 1; vertIndex <= howManyVerticalLine; vertIndex++)
      {
         float xVal =solUstX +  (vertIndex * unit);

         PlatformLineSegment seg = new PlatformLineSegment
         {
            Start = new PlatformVertex
            {
               x = xVal,
               y = solUstY,
               z = 0
            },
            End = new PlatformVertex
            {
               x = xVal,
               y = solUstY,
               z = derinlik
            }
         };
         TotalLeftSideGrid[totalIndex] = seg;
         totalIndex++;
      }


      return TotalLeftSideGrid;
   }
   
   private PlatformLineSegment[] ConstructDownSideGrid(PlatformDimensions dims)
   {
      float unit = _conf.OneUnit;
      float derinlik = unit * dims.Depth;
      int howManyHorizontalLine = (dims.Depth / (int) unit) - 1;
      int howManyVerticalLine = (dims.Height / (int) unit) - 1;

      PlatformLineSegment[] TotalLeftSideGrid = new PlatformLineSegment[howManyHorizontalLine + howManyVerticalLine];
      int totalIndex = 0;

      // taban frame'in sol ustu
      float solAltX = -(unit * dims.Width / 2.0f);
      float sagAltX = (unit * dims.Width / 2.0f);
      float solAltY = -(unit * dims.Height / 2.0f);
       
      for (int horIndex = 1; horIndex <= howManyHorizontalLine; horIndex++)
      {
         float der =  (horIndex * unit);


         PlatformLineSegment seg = new PlatformLineSegment
         {
            Start = new PlatformVertex
            {
               x = solAltX,
               y = solAltY,
               z = der
            },
            End = new PlatformVertex
            {
               x = sagAltX,
               y = solAltY,
               z = der
            }
         };
         TotalLeftSideGrid[totalIndex] = seg;
         totalIndex++;
      }
      //dikey segmentler

       

      for (int vertIndex = 1; vertIndex <= howManyVerticalLine; vertIndex++)
      {
         float xVal =solAltX +  (vertIndex * unit);

         PlatformLineSegment seg = new PlatformLineSegment
         {
            Start = new PlatformVertex
            {
               x = xVal,
               y = solAltY,
               z = 0
            },
            End = new PlatformVertex
            {
               x = xVal,
               y = solAltY,
               z = derinlik
            }
         };
         TotalLeftSideGrid[totalIndex] = seg;
         totalIndex++;
      }


      return TotalLeftSideGrid;
   }

  
}