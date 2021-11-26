using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyManager : MonoBehaviour
{
    private EnemyGenerator _enemyGenerator;
    private List<Enemy> _idleEnemies;

    private void Awake()
    {
        _enemyGenerator = new EnemyGenerator();
        _enemyGenerator.Init();
        _idleEnemies = _enemyGenerator.SpawnEnemies(50);
    }
    
}
