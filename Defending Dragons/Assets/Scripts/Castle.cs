using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Castle : MonoBehaviour
{
    [SerializeField] private float initialHealth;
    private float _health;

    private void Awake()
    {
        _health = initialHealth;
    }
}
