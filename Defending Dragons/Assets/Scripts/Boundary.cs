using System;
using UnityEngine;

public class Boundary : MonoBehaviour
{
    
    private static Camera _mainCam;
    private static float _width;
    private static float _height;
    
    private void Awake()
    {
        _mainCam = Camera.main;
        FindBoundaries();
        SetEdgeX();
    }

    /// <summary>
    /// Finds screen's width and height in world reference
    /// </summary>
    private void FindBoundaries()
    {
        // Viewport maximum value is 1, therefore we need to divide it by the offset
        _width = 1 / (_mainCam.WorldToViewportPoint(new Vector3(1, 1, 0)).x - 0.5f);
        _height = 1 / (_mainCam.WorldToViewportPoint(new Vector3(1, 1, 0)).y - 0.5f);
    }

    /// <summary>
    /// Sets the X value of the screen edge in the Statics class.
    /// </summary>
    private void SetEdgeX()
    {
        Statics.ScreenEdgeX = _width / 2;
    } 
}