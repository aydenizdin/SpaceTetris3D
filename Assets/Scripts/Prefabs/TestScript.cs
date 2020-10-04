using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestScript : MonoBehaviour
{
private float tt=1f;
private float sonOkunan = 0f;
    // Start is called before the first frame update
 
    

    // Update is called once per frame
    void Update()
    {
        float dif = Time.time -sonOkunan;

        if (dif >= tt)
        {
            transform.Translate(0,0,0.2f,Space.World);
            sonOkunan = Time.time;
        }

    }
}
