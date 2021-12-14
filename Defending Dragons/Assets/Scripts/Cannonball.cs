using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cannonball : MonoBehaviour
{
    private Rigidbody2D _rb;

    private void Awake()
    {
        _rb = GetComponent<Rigidbody2D>();
    }

    void OnCollisionEnter2D(Collision2D collision) {
        Debug.Log(collision.gameObject);
        if (collision.gameObject.CompareTag("Ground"))
        {
            Debug.Log("The cannonball " + gameObject.name + " has collided with the ground.");
            _rb.gravityScale = 0f;
        }
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
