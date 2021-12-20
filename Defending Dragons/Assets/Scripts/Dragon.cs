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

    public EnemyColor Color
    {
        get => _color;
        set
        {
            _color = value;
            GetComponent<SpriteRenderer>().color = _color switch
            {
                EnemyColor.Blue => Statics.Blue,
                EnemyColor.Red => Statics.Red,
                EnemyColor.Yellow => Statics.Yellow,
                EnemyColor.Green => Statics.Green,
                EnemyColor.Purple => Statics.Purple,
                EnemyColor.Orange => Statics.Orange,
                EnemyColor.Black => Statics.Black,
                _ => Statics.DefaultColor
            };
        }
    }

    private void Update()
    {
        if (Input.GetButtonDown("Fire2") && _nearFood)
        {
            Feed();
        }

        if (Input.GetButtonDown("Fire2") && _nearCannonball)
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
        if (_strength > 0)
        {
            _closeCannonball.Power = _strength;
            _closeCannonball.EnemyColor = DetermineNewColor(_closeCannonball.EnemyColor);
            _strength = 0;
        }
        else
        {
            Debug.Log("Dragon needs to be fed at least once!");
        }
    }

    private EnemyColor DetermineNewColor(EnemyColor current)
    {

        // In the case it is just a power-up, return the current color
        if (_color == current)
            return _color;
        
        switch (current)
        {
            // If the cannonball has no color yet
            case EnemyColor.Default:
                return _color;
            // Blue variants
            case EnemyColor.Blue:
                if (_color == EnemyColor.Red)
                    return EnemyColor.Purple;
                else if (_color == EnemyColor.Yellow)
                    return EnemyColor.Green;
                break;
            // Yellow variants
            case EnemyColor.Yellow:
                if (_color == EnemyColor.Blue)
                    return EnemyColor.Green;
                else if (_color == EnemyColor.Red)
                    return EnemyColor.Orange;
                break;
            // Red variants
            case EnemyColor.Red:
                if (_color == EnemyColor.Blue)
                    return EnemyColor.Purple;
                else if (_color == EnemyColor.Yellow)
                    return EnemyColor.Orange;
                break;
            // Anything else should become black
            default:
                return EnemyColor.Black;
        }

        // For debug purposes
        return EnemyColor.Default;
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
