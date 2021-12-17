using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CannonShooting : MonoBehaviour
{
    [SerializeField] private Transform target;
    [SerializeField] private float cannonWidth = 0.5f;
    [SerializeField] private float cannonHeight = 0.25f;
    [SerializeField] private float forceFactor = 425f;
    [Tooltip("Check if the cannon is on the right side of the castle.")]
    [SerializeField] private bool isRight;

    private CannonballsManager _cannonballsManager;
    private bool _playerInRange = false;

    private void Awake()
    {
        _cannonballsManager = GetComponentInParent<CannonsParent>().cannonballsManager;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            _playerInRange = true;
        }
    }
    
    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            _playerInRange = false;
        }
    }

    private void Update()
    {
        if (Input.GetButtonUp("Fire1") && _playerInRange)
        {
            Shoot();
        }
    }

    private void Shoot()
    {
        Vector3 position = transform.position;
        if (isRight)
        {
            position.x += cannonWidth + _cannonballsManager.CannonballWidth;
        }
        else
        {
            position.x -= cannonWidth + _cannonballsManager.CannonballWidth;
        }

        position.y += cannonHeight / 2;
        float force = ForceCalculator(_cannonballsManager.CannonballWeight, _cannonballsManager.CannonballsGravity,
            position);
        _cannonballsManager.SpawnACannonball(EnemyColor.Red, position, new Vector2(force * forceFactor, 0));
    }

    private float ForceCalculator(float mass, float gravity, Vector3 spawnPosition)
    {
        float t2 = 2 * (spawnPosition.y / gravity);
        float force = mass * (target.position.x - spawnPosition.x) / t2;
        print("t: " + Mathf.Sqrt(t2));
        return force;
    }
}
