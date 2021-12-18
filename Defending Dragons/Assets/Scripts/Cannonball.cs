using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;


/// <summary>
/// This class is self destructive;
/// upon being despawned, it calls the manager and removes itself from the list
/// while resetting its properties. 
/// </summary>
public class Cannonball : MonoBehaviour
{
    
    private Tilemap _groundTiles;
    private EnemiesManager _enemiesManager;
    private CannonballsManager _cannonballsManager;

    private SpriteRenderer _spriteRenderer;
    private Rigidbody2D _rb;
    
    private EnemyColor _enemyColor;

    /// <summary>
    /// Necessary for finding the tile it has collided with.
    /// </summary>
    public Tilemap GroundTiles
    {
        set => _groundTiles = value;
    }
    
    public EnemiesManager EnemiesManager
    {
        set => _enemiesManager = value;
    }
    
    public CannonballsManager CannonballsManager
    {
        set => _cannonballsManager = value;
    }

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

    private void Awake()
    {
        _rb = GetComponent<Rigidbody2D>();
        _spriteRenderer = GetComponent<SpriteRenderer>();
    }

    void OnTriggerEnter2D(Collider2D collider) {
        if (collider.gameObject.CompareTag("Ground")) // Upon hitting the ground start the explosion sequence
        {
            // Debug.Log("The cannonball " + gameObject.name + " has collided with the ground.");
            Explode();
            Despawn();
        }
        
    }

    /// <summary>
    /// Finds the collided tile, and calculates the center of the tile.
    /// Afterwards informs the enemiesManager for destroying possible enemies.
    /// </summary>
    private void Explode()
    {
        Vector3 belowTilePosition = transform.position - new Vector3(0f, Statics.GROUND_TILES_SIZE / 3, 0f);
        Vector3Int explosionTilePos = _groundTiles.WorldToCell(belowTilePosition);
        // Uncomment for debugging, the collided tile will become red
        // _groundTiles.SetTileFlags(explosionTilePos, TileFlags.None);
        // _groundTiles.SetColor(explosionTilePos, Color.red);
        Vector3 explosionCenterPosition = _groundTiles.CellToWorld(explosionTilePos);
        // Debug.Log("tile pos: " + explosionCenterPosition);
        explosionCenterPosition = new Vector3(explosionCenterPosition.x + Statics.GROUND_TILES_SIZE / 2, 0, 0);
        _enemiesManager.ExplosionOnPosition(explosionCenterPosition, 
            Statics.GROUND_TILES_SIZE / 2 + Statics.THRESHOLD_MARGIN_FOR_EXPLOSION, _enemyColor);
    }
    
    /// <summary>
    /// Setting the position of the object to the given position.
    /// Setting the gravity value to non zero.
    /// </summary>
    public void Spawn(Vector3 position, float gravity, float mass, Vector2 initialForce)
    {
        transform.position = position;
        _rb.gravityScale = gravity;
        _rb.mass = mass;
        _rb.AddForce(initialForce);
    }

    /// <summary>
    /// Upon dispawning, the position and rotation are reset.
    /// Also the gravity and possible velocity needs to be set to zero to stop further falling.
    /// </summary>
    private void Despawn()
    {
        transform.position = new Vector3(Statics.DefaultPoolPositionX, Statics.PoolVerticalOffset, 0);
        transform.rotation = Quaternion.identity;

        // Resetting the properties of the cannonball when backing to the pool 
        EnemyColor = EnemyColor.Default;
        
        // Stopping the fall by resetting the gravity and velocity
        _rb.gravityScale = 0f;
        _rb.velocity = Vector3.zero;

        // Returning the object to the pool
        _cannonballsManager.DespawnCannonball(this);
    }
}
