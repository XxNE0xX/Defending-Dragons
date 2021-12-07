using System.Collections.Generic;
using UnityEngine;

public class EnemyGenerator
{
    private Enemy _pfEnemy;
    
    /// <summary>
    /// Initializes the generator.
    /// </summary>
    public void Init()
    {
        _pfEnemy = GameAssets.I.pfEnemy;
    }

    /// <summary>
    /// Assigns a random type to the given enemy.
    /// </summary>
    /// <param name="enemy"> The enemy that its type is being decided.</param>
    private static void EnemyTypeRandomizer(Enemy enemy)
    {
        int rand = Random.Range(0, System.Enum.GetNames(typeof(EnemyType)).Length);
        enemy.EnemyType = (EnemyType) rand;
    }

    /// <summary>
    /// Fills the enemies object pool for later usage.
    /// </summary>
    /// <param name="idleEnemies"> The list that keeps spawned objects.</param>
    /// <param name="enemiesPool"> The object of the pool for enemies.</param>
    /// <param name="enemiesCount"> Number of enemies that are going to be in the pool.</param>
    public void GenerateEnemies(List<Enemy> idleEnemies, GameObject enemiesPool, int enemiesCount)
    {
        for (int i = 0; i < enemiesCount; i++)
        {
            Enemy enemy = Object.Instantiate(_pfEnemy, enemiesPool.transform, true);
            enemy.name = "Enemy" + i;
            enemy.transform.position = new Vector3(Statics.DefaultPoolPositionX, 0, 0);
            idleEnemies.Add(enemy);
        }

    }

}
