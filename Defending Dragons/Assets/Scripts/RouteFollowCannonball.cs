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
        InputManager();
    }
    
    private void InputManager()
    {
        // Read Inputs only if the game is running
        if (Statics.IsGamePaused) return;
        
        if (Input.GetAxisRaw("ConveyorCannonball") > 0)
        {
            IncreaseTFactor();
        } else if (Input.GetAxisRaw("ConveyorCannonball") < 0)
        {
            DecreaseTFactor();
        }
    }
}
