using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    private EnemiesManager _enemiesManager;
    private List<Enemy> _enemiesList;

    private void Awake()
    {
        _enemiesManager = GameObject.FindWithTag("EnemiesManager").GetComponent<EnemiesManager>();
        
        _enemiesList = new List<Enemy>();
    }

    void Start()
    {

        // _enemiesList.Add(_enemiesManager.SpawnAnEnemy(EnemyType.T2, EnemyMoveDirection.MarchLeft));
        // _enemiesList.Add(_enemiesManager.SpawnAnEnemy(EnemyType.T1, EnemyMoveDirection.MarchRight));
        
        // StartCoroutine(DespawnTempFunction(7, 0));
        // StartCoroutine(DespawnTempFunction(5, 1));

    }

    
    void Update()
    {
        
    }
    
    IEnumerator DespawnTempFunction(float time, int index)
    {
        yield return new WaitForSeconds(time);
 
        _enemiesManager.DespawnAnEnemy(_enemiesList[index]);
    }
}
