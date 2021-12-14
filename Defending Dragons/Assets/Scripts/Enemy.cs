using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{

    private SpriteRenderer _spriteRenderer;
    private EnemyType _enemyType;
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
    public EnemyType EnemyType
    {
        get => _enemyType;
        set
        {
            _enemyType = value;
            _spriteRenderer.color = _enemyType switch
            {
                EnemyType.Blue => new Color32(0, 204, 255, 255),
                EnemyType.Red => new Color32(255, 76, 81, 255),
                EnemyType.Yellow => Color.yellow,
                EnemyType.Green => new Color32(102, 255, 76, 255),
                EnemyType.Purple => new Color32(255, 0, 255, 255),
                EnemyType.Orange => new Color32(255, 116, 0, 255),
                EnemyType.Black => new Color32(130, 130, 130, 255),
                _ => Color.black
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
                                             Statics.EnemyVerticalOffset, 0);
        }
        else if (_enemyMoveDirection == EnemyMoveDirection.MarchRight)
        {
            transform.position = new Vector3(-Statics.ScreenEdgeX - Statics.EnemySpawnOffset, 
                                             Statics.EnemyVerticalOffset, 0);
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
        transform.position = new Vector3(Statics.DefaultPoolPositionX, Statics.EnemyVerticalOffset, 0);
        
        // Resetting the properties of the enemy when backing to the pool 
        EnemyType = EnemyType.Default;
        EnemyStatus = EnemyStatus.Default;
        EnemyMoveDirection = EnemyMoveDirection.Default;

        _motionManager.Moving = false;
    }

    private void StartAttacking()
    {
        // Start damaging the castle
    }

}
