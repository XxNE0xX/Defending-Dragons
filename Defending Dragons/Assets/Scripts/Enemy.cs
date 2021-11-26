using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{

    private EnemyType _enemyType;
    
    public EnemyType EnemyType
    {
        get => _enemyType;
        set
        {
            _enemyType = value;
            gameObject.GetComponentInChildren<SpriteRenderer>().color = _enemyType switch
            {
                EnemyType.T1 => Color.cyan,
                EnemyType.T2 => Color.magenta,
                _ => gameObject.GetComponentInChildren<SpriteRenderer>().color
            };
        }
    }

}
