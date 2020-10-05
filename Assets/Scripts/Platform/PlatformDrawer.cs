using UnityEngine;

public class PlatformDrawer
{
   private LineRenderer _lineRenderer;
   private T3DConfiguration _conf;
   
   public PlatformDrawer(T3DConfiguration conf, LineRenderer lRenderer)
   {
      _conf = conf;
      _lineRenderer = lRenderer;
   }
   public void DrawPlatform(PlatformDimensions dims)
   {
      //Test
 //     Vector3[] positions = new Vector3[3] { new Vector3(0, 0, 0), new Vector3(-1, 1, 0), new Vector3(1, 1, 0) };
      
      //_lineRenderer.loop = true;
   //   _lineRenderer.SetPositions(positions);

      Vector3[] startFramePositions = ConstructStartFramePositions(dims);

      _lineRenderer.positionCount = 4;
      _lineRenderer.SetPositions(startFramePositions);
   }

   private Vector3[] ConstructStartFramePositions(PlatformDimensions dims)
   {
      float yatayMove = _conf.OneUnit * dims.Width / 2.0f;
      float dikeyMove = _conf.OneUnit * dims.Height / 2.0f;
      
        Vector3[] StartFramePoss = new Vector3[]
        {
           new Vector3(-yatayMove,dikeyMove,0),
           new Vector3(yatayMove,dikeyMove,0),
           new Vector3(yatayMove,-dikeyMove,0),
           new Vector3(-yatayMove,-dikeyMove,0)
           
        };

        return StartFramePoss;

   }
}