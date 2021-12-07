using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemiesManager : MonoBehaviour
{
    private EnemyGenerator _enemyGenerator;
    private GameObject _enemiesPool;
    private List<Enemy> _idleEnemies;
    private List<Enemy> _activeEnemies;

    private void Awake()
    {
        _enemiesPool = GameObject.FindWithTag("EnemiesPool");
        _enemyGenerator = new EnemyGenerator();
        _enemyGenerator.Init();
        _idleEnemies = new List<Enemy>();
        _enemyGenerator.GenerateEnemies(_idleEnemies, _enemiesPool, Statics.BaseEnemiesCountInPool);
        _activeEnemies = new List<Enemy>();
    }

    /// <summary>
    /// Spawns a single enemy
    /// </summary>
    /// <param name="enemyType"> The type of the enemy.</param>
    /// <param name="enemyMoveDirection"> The direction of the enemy movement.</param>
    /// <returns> The enemy object that has been setup and is ready.</returns>
    public Enemy SpawnAnEnemy(EnemyType enemyType, EnemyMoveDirection enemyMoveDirection)
    {
        // Debug.Log("SpawnAnEnemy in EnemiesManager!");
        Enemy chosenEnemy;
        
        // Choose an enemy from the idle enemies stack
        // if there is at least one idle enemy, we choose it and continue
        if (_idleEnemies.Count > 0)
        {
            chosenEnemy = _idleEnemies[_idleEnemies.Count - 1];
            _idleEnemies.RemoveAt(_idleEnemies.Count - 1);
        }

        // otherwise, if there is no idle enemy, we spawn some more to the pool and then choose one
        else
        {
            _enemyGenerator.GenerateEnemies(_idleEnemies, _enemiesPool, Statics.AddMoreEnemiesToPoolCount);
            // Debug.Log("Idle enemies count: " + _idleEnemies.Count);
            chosenEnemy = _idleEnemies[_idleEnemies.Count - 1];
            _idleEnemies.RemoveAt(_idleEnemies.Count - 1);
        }
        
        // Setting the enemy type and its direction based on the inputs
        chosenEnemy.EnemyType = enemyType;
        chosenEnemy.EnemyMoveDirection = enemyMoveDirection;
        
        // Continue with the local settings that are needed to be set in Enemy object, including starting its movement
        chosenEnemy.Spawn();
        
        // Adding the chosen enemy to the list of the active enemies
        _activeEnemies.Add(chosenEnemy);

        
        // Debug.Log("Active enemies count: " + _activeEnemies.Count);

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
            _idleEnemies.Add(enemy);
            
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
    
}
