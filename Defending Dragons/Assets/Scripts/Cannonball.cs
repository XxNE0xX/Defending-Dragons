using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class Cannonball : MonoBehaviour
{
    
    [SerializeField] private Tilemap groundTiles;
    
    private Rigidbody2D _rb;

    private void Awake()
    {
        _rb = GetComponent<Rigidbody2D>();
    }

    void OnCollisionEnter2D(Collision2D collision) {
        if (collision.gameObject.CompareTag("Ground"))
        {
            Debug.Log("The cannonball " + gameObject.name + " has collided with the ground.");
            Vector3 belowTilePosition = transform.position - new Vector3(0f, 0.3f, 0f);
            Vector3Int explosionTilePos = groundTiles.WorldToCell(belowTilePosition);
            groundTiles.SetTileFlags(explosionTilePos, TileFlags.None);
            groundTiles.SetColor(explosionTilePos, Color.red);
        }
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
