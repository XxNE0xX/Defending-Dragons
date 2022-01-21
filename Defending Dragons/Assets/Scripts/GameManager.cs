using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField] private EnemiesManager enemiesManager;
    [SerializeField] private CannonballsManager cannonballsManager;
    [SerializeField] private DragonSpawner dragonSpawner;
    [SerializeField] private float dragonSpawnDelay = 0.1f;
    [SerializeField] private Castle castle;
    [SerializeField] private TextMeshProUGUI enemiesCountUIText;
    [SerializeField] private TextMeshProUGUI castleHealthText;

    private void Start()
    {
        castleHealthText.SetText("x" + castle.Health);
        StartCoroutine(SpawnTempFunction(1, 0, EnemyColor.Green, EnemyMoveDirection.MarchLeft, 2));
        StartCoroutine(SpawnTempFunction(4, 0, EnemyColor.Red, EnemyMoveDirection.MarchRight, 2));
        StartCoroutine(SpawnDragons());
    }

    private void Update()
    {
        castleHealthText.SetText("x" + castle.Health);
        InputManager();
    }

    private void InputManager()
    {
        if (Input.GetButtonDown("Cancel"))
        {
            Statics.IsGamePaused = !Statics.IsGamePaused;
        }
    }


    IEnumerator SpawnTempFunction(float time, int index, EnemyColor color, EnemyMoveDirection direction, int size)
    {
        yield return new WaitForSeconds(time);
 
        enemiesManager.SpawnAnEnemy(color, direction, size);
    }
    
    IEnumerator SpawnDragons()
    {
        yield return new WaitForSeconds(dragonSpawnDelay);
 
        dragonSpawner.SpawnDragons(3);
    }
}
