using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField] private GameManager gameManager;
    private EnemiesManager _enemiesManager;
    private string _levelName;
    
    [Serializable]
    public class IEList
    {
        public InputEntry[] ies;
    }
        
    [Serializable]
    public class InputEntry
    {
        public float time;
        public int color;
        public string direction;
        public int size;
    }

    private IEList _enemiesList = new IEList();

    public void Init(EnemiesManager enemiesManager, string levelName)
    {
        _enemiesManager = enemiesManager;
        _levelName = levelName;
    }

    public void StartWorking()
    {
        // StartCoroutine(SpawnFunction(1, EnemyColor.Green, EnemyMoveDirection.MarchLeft, 2));
        // StartCoroutine(SpawnFunction(4, EnemyColor.Red, EnemyMoveDirection.MarchRight, 2));
        ReadFile();
        gameManager.SetTotalEnemies(_enemiesList.ies.Length);
        foreach (var ie in _enemiesList.ies)
        {
            StartCoroutine(SpawnFunction(ie.time, ParseColor(ie.color), ParseDirection(ie.direction), ie.size));
        }
    }
    
    private IEnumerator SpawnFunction(float time, EnemyColor color, EnemyMoveDirection direction, int size)
    {
        yield return new WaitForSeconds(time);
 
        _enemiesManager.SpawnAnEnemy(color, direction, size);
    }
    
    private void ReadFile()
    {
        TextAsset file = Resources.Load<TextAsset>("LevelsData/Level" + _levelName);
        _enemiesList = JsonUtility.FromJson<IEList>(file.text);
    }

    private EnemyColor ParseColor(int color)
    {
        EnemyColor ret = color switch
        {
            1 => EnemyColor.Blue,
            2 => EnemyColor.Red,
            3 => EnemyColor.Yellow,
            4 => EnemyColor.Green,
            5 => EnemyColor.Purple,
            6 => EnemyColor.Orange,
            7 => EnemyColor.Black,
            _ => EnemyColor.Default
        };
        return ret;
    }

    private EnemyMoveDirection ParseDirection(string direction)
    {
        EnemyMoveDirection ret = direction switch
        {
            "MarchLeft" => EnemyMoveDirection.MarchLeft,
            "MarchRight" => EnemyMoveDirection.MarchRight,
            "Random" => (EnemyMoveDirection)Random.Range(1, 3),
            _ => (EnemyMoveDirection)Random.Range(1, 3)
        };

        return ret;
    }

}