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

        _enemiesList.Add(_enemiesManager.SpawnAnEnemy(EnemyColor.Red, EnemyMoveDirection.MarchLeft));
        _enemiesList.Add(_enemiesManager.SpawnAnEnemy(EnemyColor.Blue, EnemyMoveDirection.MarchRight));
        
        StartCoroutine(DespawnTempFunction(10, 0));
        StartCoroutine(DespawnTempFunction(12, 1));

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
