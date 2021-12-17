using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Castle : MonoBehaviour
{
    [SerializeField] private int initialHealth = 10000;
    private int _health;

    private void Awake()
    {
        _health = initialHealth;
    }

    public void Damage(int damageAmount)
    {
        _health -= damageAmount;
    }
}
