using System.Collections.Generic;
using UnityEngine;

public class EnemyGenerator
{
    private Enemy _enemyPrefab;
    
    /// <summary>
    /// Initialize the generator.
    /// </summary>
    public void Init()
    {
        _enemyPrefab = Resources.Load<Enemy>("Prefabs/Enemy");
    }

    /// <summary>
    /// Assign a random type to the given enemy.
    /// </summary>
    /// <param name="enemy"> The enemy that its type is being decided.</param>
    private static void EnemyTypeRandomizer(Enemy enemy)
    {
        int rand = Random.Range(0, System.Enum.GetNames(typeof(EnemyType)).Length);
        enemy.EnemyType = (EnemyType) rand;
    }

    /// <summary>
    /// Filling the enemies object pool for later usage.
    /// </summary>
    /// <param name="enemiesCount"> Number of enemies that are going to be in the pool. </param>
    public List<Enemy> SpawnEnemies(int enemiesCount)
    {
        List<Enemy> idleEnemies = new List<Enemy>();
        for (int i = 0; i < enemiesCount; i++)
        {
            Enemy enemy = Object.Instantiate(_enemyPrefab);
            EnemyTypeRandomizer(enemy);
            enemy.name = "Enemy" + i;
            enemy.transform.position = new Vector3(-5, 0, 0);
            idleEnemies.Add(enemy);
        }

        return idleEnemies;
    }

}
