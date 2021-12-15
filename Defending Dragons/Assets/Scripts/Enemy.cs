using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{

    private SpriteRenderer _spriteRenderer;
    private EnemyColor _enemyColor;
    private EnemyMoveDirection _enemyMoveDirection;
    private EnemyStatus _status;
    private EnemyMotionManager _motionManager;
    private EnemyDetectionManager _enemyDetectionManager;


    private void Awake()
    {
        _spriteRenderer = GetComponent<SpriteRenderer>();
        _motionManager = GetComponent<EnemyMotionManager>();
        _enemyDetectionManager = GetComponent<EnemyDetectionManager>();
    }

    /// <summary>
    /// Upon setting the enemy type, its color will change accordingly
    /// </summary>
    public EnemyColor EnemyColor
    {
        get => _enemyColor;
        set
        {
            _enemyColor = value;
            _spriteRenderer.color = _enemyColor switch
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
    
    /// <summary>
    /// Upon setting the movement direction, the sprite will flip on its x-axis accordingly
    /// </summary>
    public EnemyMoveDirection EnemyMoveDirection
    {
        get => _enemyMoveDirection;
        set
        {
            _enemyMoveDirection = value;
            _spriteRenderer.flipX = _enemyMoveDirection switch
            {
                EnemyMoveDirection.MarchLeft => true,
                EnemyMoveDirection.MarchRight => false,
                _ => _spriteRenderer.flipX
            };
        }
    }
    
    public EnemyStatus EnemyStatus
    {
        get => _status;
        set
        {
            _status = value;
            if (value == EnemyStatus.Attacking)
            {
                StartAttacking();
                _spriteRenderer.color = Color.magenta;
            }
        }
    }

    /// <summary>
    /// Setting the position of the object to the edge of the screen.
    /// Starting the enemy movement mechanisms.
    /// </summary>
    public void Spawn()
    {
        if (_enemyMoveDirection == EnemyMoveDirection.MarchLeft)
        {
            transform.position = new Vector3(Statics.ScreenEdgeX + Statics.EnemySpawnOffset, 
                                             Statics.PoolVerticalOffset, 0);
        }
        else if (_enemyMoveDirection == EnemyMoveDirection.MarchRight)
        {
            transform.position = new Vector3(-Statics.ScreenEdgeX - Statics.EnemySpawnOffset, 
                                             Statics.PoolVerticalOffset, 0);
        }

        _motionManager.Moving = true;
        _status = EnemyStatus.Marching;
    }
    
    /// <summary>
    /// Sets the position of the object to the default pool position, out of players' sight;
    /// Stops the motion of the object;
    /// Resets the enemy type, for safety purposes to make sure default enemies are no where except in the pool.
    /// </summary>
    public void Despawn()
    {
        transform.position = new Vector3(Statics.DefaultPoolPositionX, Statics.PoolVerticalOffset, 0);
        
        // Resetting the properties of the enemy when backing to the pool 
        EnemyColor = EnemyColor.Default;
        EnemyStatus = EnemyStatus.Default;
        EnemyMoveDirection = EnemyMoveDirection.Default;

        _motionManager.Moving = false;
    }

    private void StartAttacking()
    {
        // Start damaging the castle
        Statics.LogWarningMethodNotImplemented("StartAttacking");
    }
}
