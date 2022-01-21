using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Castle : MonoBehaviour
{
    [SerializeField] private int initialHealth = 10000;
    private int _health;

    private bool _gameOverPopup;
    
    public int Health => _health;

    private void Awake()
    {
        _health = initialHealth;
    }

    private void Update()
    {
        if (_health < 0 && !_gameOverPopup)
        {
            Debug.Log("Game Over");
            _gameOverPopup = true;
        }
    }

    /// <summary>
    /// It is supposed to be called by enemies when they are damaging the castle.
    /// </summary>
    /// <param name="damageAmount"> How much health is going to be reduced. </param>
    public void Damage(int damageAmount)
    {
        _health -= damageAmount;
    }
}
