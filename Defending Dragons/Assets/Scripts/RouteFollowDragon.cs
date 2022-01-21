using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RouteFollowDragon : RouteFollow
{
    
    private void Awake()
    {
        SpriteRenderer spriteRenderer = GetComponent<SpriteRenderer>();
        _objectHeight = spriteRenderer.size.y;
        
        // We need to call this after the conveyor awake, so we don't get a null object...
        Conveyor = transform.GetComponentInParent<ConveyorDragon>();
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
        
        if (Input.GetAxisRaw("ConveyorDragon") > 0)
        {
            IncreaseTFactor();
        } else if (Input.GetAxisRaw("ConveyorDragon") < 0)
        {
            DecreaseTFactor();
        }
    }
}
