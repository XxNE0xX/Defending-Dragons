using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Castle : MonoBehaviour
{
    [SerializeField] private int initialHealth = 10000;
    [SerializeField] private GameManager gameManager;
    [SerializeField] private HealthBar healthBar;
    private int _health;

    private bool _gameOverPopup;
    
    public int Health => _health;

    private void Start()
    {
        _health = initialHealth;
        healthBar.SetMaxHealth(initialHealth);
    }

    private void Update()
    {
        if (_health < 0 && !_gameOverPopup)
        {
            Debug.Log("Game Over");
            _gameOverPopup = true;
            gameManager.GameOver();
        }
    }

    /// <summary>
    /// It is supposed to be called by enemies when they are damaging the castle.
    /// </summary>
    /// <param name="damageAmount"> How much health is going to be reduced. </param>
    public void Damage(int damageAmount)
    {
        _health -= damageAmount;
        healthBar.SetHealth(_health);
    }
}
