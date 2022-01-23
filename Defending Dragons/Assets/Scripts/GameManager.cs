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
    [SerializeField] private GameObject pauseMenu;

    public bool IsVictory { get; private set; }

    public bool IsLost { get; private set; }

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

        IsVictory = false;
        IsLost = false;
    }

    private void Update()
    {
        InputManager();
    }

    private void InputManager()
    {
        // Only pause the game when it's not already paused, victory condition, or lose condition
        if (Input.GetButtonDown("Cancel") && !Statics.IsGamePaused && !IsVictory && !IsLost)
        {
            Time.timeScale = 0;
            Statics.IsGamePaused = true;
            pauseMenu.SetActive(true);
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
        Statics.IsGamePaused = true;
        IsVictory = true;
        victoryPanel.SetActive(true);

        int nextSceneIndex = SceneManager.GetActiveScene().buildIndex + 1;
        // We want to make sure that the maximum allowed level reaches the scene before the final victory scene
        if (nextSceneIndex > PlayerPrefs.GetInt("currentLevel") && nextSceneIndex < SceneManager.sceneCountInBuildSettings - 1)
        {
            PlayerPrefs.SetInt("currentLevel", nextSceneIndex);
        }
        // Set completed level to correspond with the currently passed level
        PlayerPrefs.SetInt("completedLevel", SceneManager.GetActiveScene().buildIndex);
    }

    public void GameOver()
    {
        Time.timeScale = 0;
        Statics.IsGamePaused = true;
        IsLost = true;
        lostPanel.SetActive(true);
    }
}
