using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dragon : MonoBehaviour
{
    private EnemyColor _color;
    private int _strength;

    private bool _nearFood;
    private Food _closeFood;
    private bool _nearCannonball;
    private Cannonball _closeCannonball;

    private void Update()
    {
        if (Input.GetButtonDown("Fire2") && _nearFood)
        {
            Feed();
        }

        if (Input.GetButtonDown("Fire3") && _nearCannonball)
        {
            Breathe();
        }
    }

    private void Feed()
    {
        // Despawn the food when it's eaten
        _closeFood.Despawn();
        _strength++;
    }

    private void Breathe()
    {
        _closeCannonball.Power = _strength;
        _strength = 0;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Cannonball"))
        {
            _nearCannonball = true;
            _closeCannonball = other.gameObject.GetComponent<Cannonball>();
        }
        if (other.gameObject.CompareTag("Food"))
        {
            _nearFood = true;
            _closeFood = other.gameObject.GetComponent<Food>();
        }
    }
    
    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Cannonball"))
        {
            _nearCannonball = false;
            _closeCannonball = null;
        }
        else if (other.gameObject.CompareTag("Food"))
        {
            _nearFood = false;
            _closeFood = null;
        }
    }
}
