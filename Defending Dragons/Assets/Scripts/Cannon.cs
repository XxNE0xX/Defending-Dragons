using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cannon : MonoBehaviour
{
    [SerializeField] private Transform target;
    [SerializeField] private float cannonWidth = 0.5f;
    [SerializeField] private float cannonHeight = 0.25f;
    [SerializeField] private float forceFactor = 425f;
    [Tooltip("Check if the cannon is on the right side of the castle.")]
    [SerializeField] private bool isRight;

    private CannonballsManager _cannonballsManager;

    private Cannonball _currentCannonball;
    private bool _loaded;

    public bool Loaded => _loaded;

    private void Awake()
    {
        _cannonballsManager = GetComponentInParent<CannonsParent>().cannonballsManager;
    }

    /// <summary>
    /// Spawns a cannonball and aims it to the target position
    /// </summary>
    public void Shoot()
    {
        if (_loaded)
        {
            // Determine the spawn position of the cannonball
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
            _currentCannonball.Shoot(position, new Vector2(force * forceFactor, 0));

            _loaded = false;
            _currentCannonball = null;
        }
        else
        {
            Debug.Log("Cannon is not loaded!");
        }
    }

    public void Load(Cannonball cannonball)
    {
        if (!_loaded)
        {
            _currentCannonball = cannonball;
            _loaded = true;
            _currentCannonball.Loaded();
        }
        else
        {
            Debug.Log("The cannon is already loaded.");
        }
    }

    /// <summary>
    /// Calculates the amount of needed force based on the movement equation of the cannonball
    /// </summary>
    /// <param name="mass"> mass of the cannonball</param>
    /// <param name="gravity"> the gravity that is being applied to the cannonball</param>
    /// <param name="spawnPosition"> starting position of the cannonball</param>
    /// <returns></returns>
    private float ForceCalculator(float mass, float gravity, Vector3 spawnPosition)
    {
        float t2 = 2 * (spawnPosition.y / gravity);
        float force = mass * (target.position.x - spawnPosition.x) / t2;
        return force;
    }
}
