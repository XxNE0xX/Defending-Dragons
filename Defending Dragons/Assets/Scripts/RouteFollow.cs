// Code structure by Alexander Zotov

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RouteFollow : MonoBehaviour
{
    private Route[] _routes;

    private Vector2 _objectPosition;

    private float _objectHeight;

    private ConveyorDragon cd;
    
    private int _tFactor;
    private int _stepsCountInRoute;
    private float _tParamStepsSize;
    

    private void Awake()
    {
        SpriteRenderer spriteRenderer = GetComponent<SpriteRenderer>();
        _objectHeight = spriteRenderer.size.y;
    }

    private void Start()
    {
        cd = transform.GetComponentInParent<ConveyorDragon>();
        _routes = cd.Routes;

        _tParamStepsSize = cd.TParamStepsSize;
        _stepsCountInRoute = (int)(1 / _tParamStepsSize);
        
        GoToTFactor(0);
    }

    private void Update()
    {
        if (Input.GetKey(KeyCode.Q))
        {
            DecreaseTFactor();
        }
        
        if (Input.GetKey(KeyCode.E))
        {
            IncreaseTFactor();
        }
    }

    private void FixedUpdate()
    {
        GoToTFactor(_tFactor);
    }

    private void IncreaseTFactor()
    {
        if (_tFactor + 1 < _routes.Length * _stepsCountInRoute)
        {
            _tFactor++;
        }
        else
        {
            _tFactor = 0;
        }
    }
    
    private void DecreaseTFactor()
    {
        if (_tFactor - 1 >= 0)
        {
            _tFactor--;
        }
        else
        {
            _tFactor = _routes.Length * _stepsCountInRoute;
        }
    }

    private void GoToTFactor(int tFactor)
    {
        int routeIndex = 0;
        if (tFactor % _stepsCountInRoute == 0 && tFactor != 0)
        {
            routeIndex = (tFactor / _stepsCountInRoute) - 1;
        }
        else
        {
            routeIndex = Mathf.FloorToInt((float)tFactor / _stepsCountInRoute);
        }
        int offset = tFactor - routeIndex * _stepsCountInRoute;
        
        GoByRoute(routeIndex, offset);
    }

    private void GoByRoute(int routeIndex, int offset)
    {
        Vector2 p0 = _routes[routeIndex].transform.GetChild(0).position;
        Vector2 p1 = _routes[routeIndex].transform.GetChild(1).position;
        Vector2 p2 = _routes[routeIndex].transform.GetChild(2).position;
        Vector2 p3 = _routes[routeIndex].transform.GetChild(3).position;

        float tParam = _tParamStepsSize * offset;
        
        _objectPosition = Mathf.Pow(1 - tParam, 3) * p0 + 3 * 
            Mathf.Pow(1 - tParam, 2) * tParam * p1 + 3 * (1 - tParam) * 
            Mathf.Pow(tParam, 2) * p2 + Mathf.Pow(tParam, 3) * p3;
        
        // Add half of the dragon's height so it feels it's on the platform
        _objectPosition.y += _objectHeight / 2;
        transform.position = _objectPosition;
    }
}
