using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Food : HandyObject
{

    private FoodsManager _foodsManager;
    
    private void Awake()
    {

        _rb = GetComponent<Rigidbody2D>();
        _rb.gravityScale = 0f;
    }

    public void Spawn(FoodsManager foodsManager, float gravity)
    {
        _foodsManager = foodsManager;
        _rb.gravityScale = gravity;
        _defaultGravity = gravity;

        
    }

    public void Despawn()
    {
        _foodsManager.DespawnAFood(this);
        
        _foodsManager = null;
        
        transform.position = new Vector3(Statics.DefaultPoolPositionX, Statics.PoolVerticalOffset, 0);
        transform.rotation = Quaternion.identity;
        
        // Stopping the fall by resetting the gravity and velocity
        _rb.gravityScale = 0f;
        _rb.velocity = Vector3.zero;
    }
}
