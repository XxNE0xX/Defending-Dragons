using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{

    private SpriteRenderer _spriteRenderer;
    private EnemyType _enemyType;
    private EnemyMoveDirection _enemyMoveDirection;
    private EnemyMotionManager _motionManager;

    private void Awake()
    {
        _spriteRenderer = gameObject.GetComponentInChildren<SpriteRenderer>();
        _motionManager = gameObject.GetComponent<EnemyMotionManager>();
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
                EnemyType.T1 => Color.cyan,
                EnemyType.T2 => Color.magenta,
                _ => Color.red
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

    /// <summary>
    /// Setting the position of the object to the edge of the screen.
    /// Starting the enemy movement mechanisms.
    /// </summary>
    public void Spawn()
    {
        if (_enemyMoveDirection == EnemyMoveDirection.MarchLeft)
        {
            transform.position = new Vector3(Statics.ScreenEdgeX + Statics.EnemySpawnOffset, 0, 0);
        }
        else if (_enemyMoveDirection == EnemyMoveDirection.MarchRight)
        {
            transform.position = new Vector3(-Statics.ScreenEdgeX - Statics.EnemySpawnOffset, 0, 0);
        }
        
        _motionManager.Moving = true;
    }
    
    /// <summary>
    /// Sets the position of the object to the default pool position, out of players' sight;
    /// Stops the motion of the object;
    /// Resets the enemy type, for safety purposes to make sure default enemies are no where except in the pool.
    /// </summary>
    public void Despawn()
    {
        transform.position = new Vector3(Statics.DefaultPoolPositionX, 0, 0);
        _motionManager.Moving = false;
        EnemyType = EnemyType.Default;
    }

}
