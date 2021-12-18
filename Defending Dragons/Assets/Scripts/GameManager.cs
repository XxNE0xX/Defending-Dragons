using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField] private EnemiesManager enemiesManager;
    [SerializeField] private CannonballsManager cannonballsManager;
    

    private void Start()
    {

        StartCoroutine(SpawnTempFunction(1, 0, EnemyColor.Green, EnemyMoveDirection.MarchLeft));
        StartCoroutine(SpawnTempFunction(4, 0, EnemyColor.Red, EnemyMoveDirection.MarchRight));

    }
    
    
    IEnumerator SpawnTempFunction(float time, int index, EnemyColor color, EnemyMoveDirection direction)
    {
        yield return new WaitForSeconds(time);
 
        enemiesManager.SpawnAnEnemy(color, direction);
    }
}
