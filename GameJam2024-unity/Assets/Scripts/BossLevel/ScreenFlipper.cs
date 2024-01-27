using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScreenFlipper : MonoBehaviour
{
    public float bufferArea = 0.5f;
    void LateUpdate()
    {
        KeepOnScreen();
    }

    void KeepOnScreen()
    {
        var pos = transform.position;
        var viewportPos = Camera.main.WorldToViewportPoint(pos);
        if (viewportPos.x > 1 + bufferArea || viewportPos.x < 0 - bufferArea)
        {
            pos.x = pos.x * -0.9f;
        }
        if (viewportPos.y > 1 + bufferArea || viewportPos.y < 0- bufferArea)
        {
            pos.y = pos.y * -0.9f;
        }
        transform.position = pos;
    
    }

}
