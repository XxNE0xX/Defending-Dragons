using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyDetectionManager : MonoBehaviour
{
    
    private EnemyMotionManager _motionManager;
    private Enemy _enemy;
    
    private void Awake()
    {
        _motionManager = GetComponent<EnemyMotionManager>();
        _enemy = GetComponent<Enemy>();
    }

    void OnTriggerEnter2D(Collider2D collider) {
        if (collider.gameObject.CompareTag("Castle"))
        {
            Debug.Log("The enemy " + gameObject.name + " has collided with the castle.");
            InformOtherComponents();
        }
        
    }

    private void InformOtherComponents()
    {
        _motionManager.CollisionWithCastle();
        _enemy.EnemyStatus = EnemyStatus.Attacking;
    }

}
