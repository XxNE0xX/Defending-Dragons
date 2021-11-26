using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyGenerator : MonoBehaviour
{
    public Enemy enemyPrefab;
    private const int ENEMIES_COUNT = 50; 
    private List<Enemy> idleEnemies;

    private void SpawnEnemies()
    {
        for (int i = 0; i < ENEMIES_COUNT; i++)
        {
            Enemy enemy = Instantiate(enemyPrefab);
            enemy.name = "Enemy" + i;
            enemy.transform.position.Set(-5, 0, 0);
            idleEnemies.Add(enemy);
        }
    }
    
    // Start is called before the first frame update
    void Start()
    {
        idleEnemies = new List<Enemy>();
        enemyPrefab = Resources.Load<Enemy>("Prefabs/Enemy");
        SpawnEnemies();
    }

    private Enemy GrabIdleEnemy()
    {
        return new Enemy();
    }

    private void RunEnemy(Enemy enemy)
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
