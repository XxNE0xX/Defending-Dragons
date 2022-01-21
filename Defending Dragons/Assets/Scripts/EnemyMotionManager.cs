using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMotionManager : MonoBehaviour
{
    private bool _moving;
    
    private Enemy _enemy;
    private float _localSpeed;

    /// <summary>
    /// Upon setting the moving value, the speed of the object updates accordingly
    /// </summary>
    public bool Moving
    {
        get => _moving;

        set
        {
            _moving = value;
            if (_moving)
            {
                _localSpeed = _enemy.EnemyMoveDirection switch
                {
                    EnemyMoveDirection.MarchLeft => -Statics.EnemySpeed,
                    EnemyMoveDirection.MarchRight => Statics.EnemySpeed,
                    _ => Statics.EnemySpeed
                };
            }
            else
            {
                _localSpeed = 0f;
            }
        }
    }

    public void CollisionWithCastle()
    {
        _localSpeed = 0;
    }

    private void Awake()
    {
        _enemy = GetComponent<Enemy>();
    }

    private void Update()
    {
        // Only move if the game is not paused
        if (!Statics.IsGamePaused)
        {
            // If the object is moving, the position updates respectively
            if (_moving)
            {
                transform.position += Vector3.right * (_localSpeed * Time.deltaTime);
            }
        }
    }
    
    
}
