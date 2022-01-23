using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConveyorCannonball : Conveyor
{
    [SerializeField] private GameObject CWRotationIndicator;
    [SerializeField] private GameObject CCWRotationIndicator;

    private void Start()
    {
        CWRotationIndicator.SetActive(false);
        CCWRotationIndicator.SetActive(false);
    }

    private void Update()
    {
        InputManager();
    }
    
    private void InputManager()
    {
        // Read Inputs only if the game is running
        if (Statics.IsGamePaused) return;
        
        if (Input.GetAxisRaw("ConveyorCannonball") > 0)
        {
            CWRotationIndicator.SetActive(true);
        } else if (Input.GetAxisRaw("ConveyorCannonball") < 0)
        {
            CCWRotationIndicator.SetActive(true);
        }
        else
        {
            CWRotationIndicator.SetActive(false);
            CCWRotationIndicator.SetActive(false);
        }
    }
}
