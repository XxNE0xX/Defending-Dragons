using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private CharacterController2D controller;
    [SerializeField] private float runSpeed = 0.7f;
    [SerializeField] private float sprintValueMultiplier = 1.75f;
    
    private float _horizontalMove = 0f;
    private bool _jump = false;
    private float _speedModifier = 1f;

    private void Update()
    {
        _horizontalMove = Input.GetAxisRaw("Horizontal") * runSpeed * _speedModifier;

        if (Input.GetButtonDown("Jump"))
        {
            _jump = true;
        }

        if (Input.GetButtonDown("Sprint"))
        {
            _speedModifier = sprintValueMultiplier;
        }
        else if (Input.GetButtonUp("Sprint")){
            _speedModifier = 1f;
        }
    }

    private void FixedUpdate()
    {
        controller.Move(_horizontalMove, false, _jump);
        _jump = false;
    }
}
