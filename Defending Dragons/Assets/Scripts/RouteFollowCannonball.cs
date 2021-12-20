using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RouteFollowCannonball : RouteFollow
{
    
    private void Start()
    {
        SpriteRenderer spriteRenderer = GetComponent<SpriteRenderer>();
        _objectHeight = spriteRenderer.size.y;
        
        Conveyor = transform.GetComponentInParent<ConveyorCannonball>();
        Routes = Conveyor.Routes;

        TParamStepsSize = Conveyor.TParamStepsSize;
        StepsCountInRoute = (int)(1 / TParamStepsSize);
    }

    private void Update()
    {
        if (!PositionInitialized)
        {
            SetInitialTFactor(0);
        }
        if (Input.GetAxisRaw("ConveyorCannonball") > 0)
        {
            IncreaseTFactor();
        } else if (Input.GetAxisRaw("ConveyorCannonball") < 0)
        {
            DecreaseTFactor();
        }
    }
}
