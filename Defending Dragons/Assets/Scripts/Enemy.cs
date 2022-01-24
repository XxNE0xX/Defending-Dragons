using System;
using System.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class Enemy : MonoBehaviour
{

    private SpriteRenderer _spriteRenderer;
    private int _enemySize;
    private EnemyColor _enemyColor;
    private EnemyMoveDirection _enemyMoveDirection;
    private EnemyStatus _status;
    private EnemyMotionManager _motionManager;
    private EnemyDetectionManager _enemyDetectionManager;

    private EnemiesManager _enemiesManager;
    private Castle _castle;
    

    private void Awake()
    {
        _spriteRenderer = GetComponent<SpriteRenderer>();
        _motionManager = GetComponent<EnemyMotionManager>();
        _enemyDetectionManager = GetComponent<EnemyDetectionManager>();
    }

    public int EnemySize
    {
        get => _enemySize;
        set
        {
            _enemySize = value;
            Sprite[] NPCsAtlas = Resources.LoadAll<Sprite>("Sprites/NPCs");
            _spriteRenderer.sprite = _enemySize switch
            {
                1 => NPCsAtlas.Single(s => s.name == "Enemy_1"),
                2 => NPCsAtlas.Single(s => s.name == "Enemy_2"),
                _ => NPCsAtlas.Single(s => s.name == "Enemy_1")
            };
        }
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
                PrepareForAttacking();
            }
        }
    }

    /// <summary>
    /// Setting the position of the object to the edge of the screen.
    /// Starting the enemy movement mechanisms.
    /// </summary>
    public void Spawn(EnemiesManager enemiesManager)
    {

        _enemiesManager = enemiesManager;
        
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

        _enemiesManager = null;
        
        transform.position = new Vector3(Statics.DefaultPoolPositionX, Statics.PoolVerticalOffset, 0);

        // Resetting the properties of the enemy when backing to the pool 
        EnemyColor = EnemyColor.Default;
        EnemyStatus = EnemyStatus.Default;
        EnemyMoveDirection = EnemyMoveDirection.Default;

        _motionManager.Moving = false;
        
        // Stop attacking the castle
        CancelInvoke(nameof(Attack));
    }


    /// <summary>
    /// The function that prepares the enemy for damaging the castle
    /// </summary>
    private void PrepareForAttacking()
    {
        // Getting the castle object from the parent
        _castle = _enemiesManager.Castle;
        // Start damaging the castle
        InvokeRepeating(nameof(Attack), 0.2f, _enemiesManager.DamageIntervals);
    }

    /// <summary>
    /// Determines a random damage value and reduce the health of the castle;
    /// along with creating the right damage popup.
    /// </summary>
    private void Attack()
    {
        // Only attack if the game is running
        if (Statics.IsGamePaused) return;
        
        // If the enemy was marching to the right, the damage is supposed to pop up behind his head, therefore true
        bool toLeft = _enemyMoveDirection == EnemyMoveDirection.MarchRight;

        int damageAmount;
        switch (_enemySize)
        {
            case 1:
                damageAmount = Random.Range(Statics.MinEnemy1Damage, Statics.MaxEnemy1Damage);
                SFXManager.I.SwordOnDoor();
                break;
            case 2:
                damageAmount = Random.Range(Statics.MinEnemy2Damage, Statics.MaxEnemy2Damage);
                SFXManager.I.BatteringRam();
                break;
            default:
                damageAmount = Random.Range(Statics.MinEnemy1Damage, Statics.MaxEnemy1Damage);
                SFXManager.I.SwordOnDoor();
                break;
        }

        Vector3 damagePopupPosition = transform.position;

        if (toLeft)
        {
            damagePopupPosition.x += 0.5f;
        }
        else
        {
            damagePopupPosition.x -= 0.5f;
        }

        _castle.Damage(damageAmount);
        DamagePopup.Create(damagePopupPosition, damageAmount, toLeft, _enemySize);
    }

}
