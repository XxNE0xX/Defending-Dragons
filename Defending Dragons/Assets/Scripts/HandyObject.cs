using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class HandyObject : MonoBehaviour
{
    
    protected Rigidbody2D _rb;

    protected float _defaultGravity;

    public void PickUp()
    {
        // Stopping the fall by resetting the gravity and velocity
        _rb.gravityScale = 0f;
        _rb.velocity = Vector3.zero;
    }

    public void Drop(Vector3 force)
    {
        _rb.gravityScale = _defaultGravity;
        _rb.AddForce(force);
    }
}
