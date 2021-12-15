using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField] private EnemiesManager enemiesManager;
    [SerializeField] private CannonballsManager cannonballsManager;
    private List<Enemy> _enemiesList;
    

    private void Awake()
    {
        _enemiesList = new List<Enemy>();
    }

    void Start()
    {

        _enemiesList.Add(enemiesManager.SpawnAnEnemy(EnemyColor.Red, EnemyMoveDirection.MarchLeft));
        _enemiesList.Add(enemiesManager.SpawnAnEnemy(EnemyColor.Blue, EnemyMoveDirection.MarchRight));

        // cannonballsManager.SpawnACannonball(EnemyColor.Default, new Vector3(6, 3, 0));
        
        // StartCoroutine(DespawnTempFunction(10, 0));
        // StartCoroutine(DespawnTempFunction(12, 1));

    }

    
    void Update()
    {
        
    }
    
    IEnumerator DespawnTempFunction(float time, int index)
    {
        yield return new WaitForSeconds(time);
 
        enemiesManager.DespawnAnEnemy(_enemiesList[index]);
    }
}
