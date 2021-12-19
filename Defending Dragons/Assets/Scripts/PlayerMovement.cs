using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private CharacterController2D controller;
    [SerializeField] private float moveSpeed = 0.5f;
    [SerializeField] private float sprintValueMultiplier = 1.75f;
    [SerializeField] private float defaultGravity = 3f;
    [SerializeField] private float gravityOnLadder = 2f;
    
    private Rigidbody2D _rb;
    
    private float _horizontalMove = 0f;
    private float _verticalMove = 0f;

    private bool _onLadder = false;
    private float _speedModifier = 1f;

    private void Awake()
    {
        _rb = GetComponent<Rigidbody2D>();
        _rb.gravityScale = defaultGravity;
    }

    private void Update()
    {
        _horizontalMove = Input.GetAxisRaw("Horizontal") * moveSpeed * _speedModifier;
        _verticalMove = Input.GetAxisRaw("Ascend") * moveSpeed * Convert.ToInt32(_onLadder);

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
        controller.Move(_horizontalMove, _verticalMove, false);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Ladder"))
        {
            _onLadder = true;
            Physics2D.IgnoreLayerCollision(LayerMask.NameToLayer("Player"), LayerMask.NameToLayer("CastlePlatforms"));
            _rb.gravityScale = gravityOnLadder;
        }
    }
    
    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Ladder"))
        {
            _onLadder = false;
            Physics2D.IgnoreLayerCollision(LayerMask.NameToLayer("Player"), LayerMask.NameToLayer("CastlePlatforms"), _onLadder);
            _rb.gravityScale = defaultGravity;
        }
    }
}
