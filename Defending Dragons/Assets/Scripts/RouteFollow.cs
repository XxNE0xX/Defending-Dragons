// Code structure by Alexander Zotov

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class RouteFollow : MonoBehaviour
{
    protected Route[] Routes;

    private Vector2 _objectPosition;

    protected float _objectHeight;

    protected Conveyor Conveyor;
    
    private int _tFactor;
    protected int StepsCountInRoute;
    protected float TParamStepsSize;
    protected bool PositionInitialized;

    private void FixedUpdate()
    {
        GoToTFactor(_tFactor);
    }

    public void SetInitialTFactor(int tFactor)
    {
        _tFactor = tFactor;
        PositionInitialized = true;
    }

    protected void IncreaseTFactor()
    {
        if (_tFactor + 1 < Routes.Length * StepsCountInRoute)
        {
            _tFactor++;
        }
        else
        {
            _tFactor = 0;
        }
    }
    
    protected void DecreaseTFactor()
    {
        if (_tFactor - 1 >= 0)
        {
            _tFactor--;
        }
        else
        {
            _tFactor = Routes.Length * StepsCountInRoute;
        }
    }

    private void GoToTFactor(int tFactor)
    {
        int routeIndex = 0;
        if (tFactor % StepsCountInRoute == 0 && tFactor != 0)
        {
            routeIndex = (tFactor / StepsCountInRoute) - 1;
        }
        else
        {
            routeIndex = Mathf.FloorToInt((float)tFactor / StepsCountInRoute);
        }
        int offset = tFactor - routeIndex * StepsCountInRoute;
        
        GoByRoute(routeIndex, offset);
    }

    private void GoByRoute(int routeIndex, int offset)
    {
        Vector2 p0 = Routes[routeIndex].transform.GetChild(0).position;
        Vector2 p1 = Routes[routeIndex].transform.GetChild(1).position;
        Vector2 p2 = Routes[routeIndex].transform.GetChild(2).position;
        Vector2 p3 = Routes[routeIndex].transform.GetChild(3).position;

        float tParam = TParamStepsSize * offset;
        
        _objectPosition = Mathf.Pow(1 - tParam, 3) * p0 + 3 * 
            Mathf.Pow(1 - tParam, 2) * tParam * p1 + 3 * (1 - tParam) * 
            Mathf.Pow(tParam, 2) * p2 + Mathf.Pow(tParam, 3) * p3;
        
        // Add half of the object's height so it feels it's on the platform
        _objectPosition.y += _objectHeight / 2;
        transform.position = _objectPosition;
    }
}
