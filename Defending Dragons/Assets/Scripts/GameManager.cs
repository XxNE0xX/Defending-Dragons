using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Serialization;

public class GameManager : MonoBehaviour
{
    [SerializeField] private EnemiesManager enemiesManager;
    [SerializeField] private CannonballsManager cannonballsManager;
    [SerializeField] private DragonSpawner dragonSpawner;
    [SerializeField] private float dragonSpawnDelay = 0.1f;
    [SerializeField] private Castle castle;
    [SerializeField] private TextMeshProUGUI enemiesCountUIText;
    [FormerlySerializedAs("_enemySpawner")] [SerializeField] private EnemySpawner enemySpawner;
    [SerializeField] private GameObject victoryPanel;
    [SerializeField] private GameObject lostPanel;

    private string _levelName;
    private int _killedEnemiesCount;
    private int _totalEnemies;

    private void Start()
    {
        // The level is set based on the name of the scene
        _levelName = SceneManager.GetActiveScene().name.Replace("Level", "");
        enemySpawner.Init(enemiesManager, _levelName);
        enemySpawner.StartWorking();
        StartCoroutine(SpawnDragons());
    }

    private void Update()
    {
        InputManager();
    }

    private void InputManager()
    {
        if (Input.GetButtonDown("Cancel"))
        {
            Time.timeScale = Statics.IsGamePaused ? 1 : 0;
            Statics.IsGamePaused = !Statics.IsGamePaused;
        }
    }

    public void SetTotalEnemies(int count)
    {
        _totalEnemies = count;
        UpdateEnemiesCountHUD();
    }

    public void EnemyKilled()
    {
        _killedEnemiesCount++;
        UpdateEnemiesCountHUD();
        if (_killedEnemiesCount == _totalEnemies)
        {
            Victory();
        }
    }

    private void UpdateEnemiesCountHUD()
    {
        enemiesCountUIText.SetText(_killedEnemiesCount + "/" + _totalEnemies);
    }

    private IEnumerator SpawnDragons()
    {
        yield return new WaitForSeconds(dragonSpawnDelay);
 
        dragonSpawner.SpawnDragons(_levelName);
    }

    private void Victory()
    {
        Time.timeScale = 0;
        victoryPanel.SetActive(true);
    }

    public void GameOver()
    {
        Time.timeScale = 0;
        lostPanel.SetActive(true);
    }
}
