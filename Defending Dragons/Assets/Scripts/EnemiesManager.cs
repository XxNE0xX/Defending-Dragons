using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class EnemiesManager : MonoBehaviour
{
    
    [SerializeField] private GameObject enemiesPool;
    [SerializeField] private Castle castle;

    [SerializeField] private float damageIntervals = 1f;
    
    private EnemyGenerator _enemyGenerator;
    private List<GameObject> _idleEnemies;
    private List<Enemy> _activeEnemies;

    public Castle Castle => castle;
    
    public float DamageIntervals => damageIntervals;

    private void Awake()
    {
        _enemyGenerator = new EnemyGenerator();
        _enemyGenerator.Init();
        _idleEnemies = new List<GameObject>();
        _enemyGenerator.GenerateObjects(_idleEnemies, enemiesPool, Statics.BaseEnemiesCountInPool);
        _activeEnemies = new List<Enemy>();
    }

    /// <summary>
    /// Spawns a single enemy
    /// </summary>
    /// <param name="enemyColor"> The type of the enemy.</param>
    /// <param name="enemyMoveDirection"> The direction of the enemy movement.</param>
    /// <returns> The enemy object that has been setup and is ready.</returns>
    public Enemy SpawnAnEnemy(EnemyColor enemyColor, EnemyMoveDirection enemyMoveDirection)
    {
        // Debug.Log("SpawnAnEnemy in EnemiesManager!");
        Enemy chosenEnemy;
        
        // Choose an enemy from the idle enemies stack
        // if there is at least one idle enemy, we choose it and continue
        if (_idleEnemies.Count > 0)
        {
            chosenEnemy = _idleEnemies[_idleEnemies.Count - 1].GetComponent<Enemy>();
            _idleEnemies.RemoveAt(_idleEnemies.Count - 1);
        }

        // otherwise, if there is no idle enemy, we spawn some more to the pool and then choose one
        else
        {
            _enemyGenerator.GenerateObjects(_idleEnemies, enemiesPool, Statics.AddMoreEnemiesToPoolCount);
            // Debug.Log("Idle enemies count: " + _idleEnemies.Count);
            chosenEnemy = _idleEnemies[_idleEnemies.Count - 1].GetComponent<Enemy>();
            _idleEnemies.RemoveAt(_idleEnemies.Count - 1);
        }
        
        // Setting the enemy type and its direction based on the inputs
        chosenEnemy.EnemyColor = enemyColor;
        chosenEnemy.EnemyMoveDirection = enemyMoveDirection;
        
        // Continue with the local settings that are needed to be set in Enemy object, including starting its movement
        chosenEnemy.Spawn(this);
        
        // Adding the chosen enemy to the list of the active enemies
        _activeEnemies.Add(chosenEnemy);

        // Set the parent as the current enemy manager
        chosenEnemy.transform.SetParent(transform);

        return chosenEnemy;
    }

    /// <summary>
    /// Despawns a single enemy
    /// </summary>
    /// <param name="enemy"> The enemy instance that needs to be despawned.</param>
    public void DespawnAnEnemy(Enemy enemy)
    {
        // We need to despawn the enemy only if it is active
        if (_activeEnemies.Contains(enemy))
        {
            // Removing the enemy from the active stack and piling it up on the idle stack
            _activeEnemies.Remove(enemy);
            _idleEnemies.Add(enemy.gameObject);
            
            // Set the parent as the enemy pool again
            enemy.transform.SetParent(enemiesPool.transform);
            
            // Finalizing the process by calling the local despawn
            enemy.Despawn();
            
            Debug.Log("The enemy with transform InstanceID: " + enemy.transform.GetInstanceID() + 
                           " despawned.");
        }
        // Logging error if something tries to despawn an enemy twice
        else
        {
            Debug.LogError("The enemy with transform InstanceID: " + enemy.transform.GetInstanceID() + 
                           " has already been despawned.");
        }
    }

    /// <summary>
    /// The function is supposed to be called by the cannonballs upon hitting the ground.
    /// </summary>
    /// <param name="position"> The center of the explosion, probably center of a ground tile.</param>
    /// <param name="radius"> The margin that needs to be checked for possible present enemies.</param>
    /// <param name="color"> The color of the cannonball hitting the ground to kill the corresponding enemies.</param>
    public void ExplosionOnPosition(Vector3 position, float radius, EnemyColor color)
    {
        List<Enemy> possibleKilledEnemies = FindEnemiesByPosition(position, radius);
        foreach (Enemy possibleKilledEnemy in possibleKilledEnemies)
        {
            if (color == possibleKilledEnemy.EnemyColor)    // Enemy is killed
            {
                DespawnAnEnemy(possibleKilledEnemy);
            }
            else    // Enemy Survives
            {
                Debug.Log("The " + color + " cannonball can't kill a " + possibleKilledEnemy.EnemyColor + " enemy!");
            }
        }
    }

    /// <summary>
    /// A greedy search function looking for enemies in a circular plain from a given position.
    /// </summary>
    /// <param name="position"> The center of the circle.</param>
    /// <param name="radius"> The radius of the circle.</param>
    /// <returns></returns>
    private List<Enemy> FindEnemiesByPosition(Vector3 position, float radius)
    {
        List<Enemy> presentEnemies = new List<Enemy>();


        foreach (Enemy enemy in _activeEnemies)
        {
            var enemyPosition = enemy.transform.position;
            float distance = Mathf.Pow(radius, 2) - 
                             (Mathf.Pow((position.x - enemyPosition.x), 2) + 
                              Mathf.Pow((position.y - enemyPosition.y), 2));

            if (distance > Mathf.Epsilon)
            {
                // Debug.Log(enemy.name + " is inside");
                presentEnemies.Add(enemy);
            }
            else if (Mathf.Approximately(distance, 0))
            {
                // Debug.Log(enemy.name + " is on the circumference");
                presentEnemies.Add(enemy);
            }
            else
            {
                // Debug.Log(enemy.name + " is outside");
            }
        }
        
        
        return presentEnemies;
    }
    
}
