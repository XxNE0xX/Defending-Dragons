using System.Collections.Generic;
using UnityEngine;

public class EnemyGenerator : PoolGenerator
{
    private Enemy _pfEnemy;
    
    /// <summary>
    /// Initializes the generator.
    /// </summary>
    public new void Init()
    {
        _pfEnemy = GameAssets.I.pfEnemy;
        base.prefab = _pfEnemy.gameObject;
    }

    /// <summary>
    /// Assigns a random type to the given enemy.
    /// </summary>
    /// <param name="enemy"> The enemy that its type is being decided.</param>
    private static void EnemyTypeRandomizer(Enemy enemy)
    {
        int rand = Random.Range(0, System.Enum.GetNames(typeof(EnemyColor)).Length);
        enemy.EnemyColor = (EnemyColor) rand;
    }

    /// <summary>
    /// Fills the enemies object pool for later usage.
    /// </summary>
    /// <param name="idleEnemies"> The list that keeps spawned objects.</param>
    /// <param name="enemiesPool"> The object of the pool for enemies.</param>
    /// <param name="enemiesCount"> Number of enemies that are going to be in the pool.</param>
    public void GenerateObjects(List<GameObject> idleEnemies, GameObject enemiesPool, int enemiesCount)
    {
        base.GenerateObjects(idleEnemies, enemiesPool, enemiesCount, "Enemy");
    }

}
