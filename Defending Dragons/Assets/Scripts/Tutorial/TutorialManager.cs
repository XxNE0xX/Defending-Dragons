using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialManager : MonoBehaviour
{
    [SerializeField] private GameObject[] popUps;
    [SerializeField] private GameManager gameManager;
    [SerializeField] private EnemySpawner enemySpawner;
    [SerializeField] private float finalPopupCoolDown = 2f;

    private int _popUpIndex;

    // private bool _climbed;
    // private bool _pickedFood;
    // private bool _droppedFood;
    // private bool _feedDragon;
    // private bool _createdCannonball;
    // private bool _blowDragon;
    // private bool _cannonballPicked;
    // private bool _cannonLoaded;
    // private bool _cannonShot;
    
    public bool Climbed { private get; set; }
    public bool PickedFood { private get; set; }
    public bool DroppedFood { private get; set; }
    public bool FeedDragon { private get; set; }
    public bool CreatedCannonball { private get; set; }
    public bool BlowDragon { private get; set; }
    public bool CannonballPicked { private get; set; }
    public bool CannonLoaded { private get; set; }
    public bool CannonShot { private get; set; }

    private void Awake()
    {
        gameManager.IsTutorial = true;
    }

    private void Update()
    {

        for (int i = 0; i < popUps.Length; i++)
        {
            if (i == _popUpIndex)
            {
                popUps[_popUpIndex].SetActive(true);
            }
            else
            {
                popUps[i].SetActive(false);
            }
        }
        // Tutorial start
        // First popup: moving left and right
        if (_popUpIndex == 0)
        {
            if (!Mathf.Approximately(Input.GetAxisRaw("P1Horizontal"), 0f))
            {
                _popUpIndex++;
            }
        }
        // Second popup: jumping
        else if (_popUpIndex == 1)
        {
            if (Input.GetButtonDown("Jump"))
            {
                _popUpIndex++;
            }
        }
        // Third popup: climbing ladders
        else if (_popUpIndex == 2)
        {
            if (Climbed)
            {
                _popUpIndex++;
            }
        }
        // Fourth popup: picking up food
        else if (_popUpIndex == 3)
        {
            if (PickedFood)
            {
                _popUpIndex++;
            }
        }
        // Fifth popup: dropping food for the dragon
        else if (_popUpIndex == 4)
        {
            if (DroppedFood)
            {
                _popUpIndex++;
            }
        }
        
        // Sixth popup: moving dragon
        else if (_popUpIndex == 5)
        {
            if (!Mathf.Approximately(Input.GetAxisRaw("ConveyorDragon"), 0f))
            {
                _popUpIndex++;
            }
        }
        
        // Seventh popup: feeding the dragon
        else if (_popUpIndex == 6)
        {
            if (FeedDragon)
            {
                _popUpIndex++;
            }
        }
        
        // Eighth popup: creating cannonball
        else if (_popUpIndex == 7)
        {
            if (CreatedCannonball)
            {
                _popUpIndex++;
            }
        }
        
        // Ninth popup: moving cannonball
        else if (_popUpIndex == 8)
        {
            if (!Mathf.Approximately(Input.GetAxisRaw("ConveyorCannonball"), 0f))
            {
                _popUpIndex++;
            }
        }
        
        // Tenth popup: dragon blowing
        else if (_popUpIndex == 9)
        {
            if (BlowDragon)
            {
                _popUpIndex++;
            }
        }
        
        // Eleventh popup: picking up cannonball
        else if (_popUpIndex == 10)
        {
            if (CannonballPicked)
            {
                _popUpIndex++;
            }
        }
        
        // Twelfth popup: loading cannon
        else if (_popUpIndex == 11)
        {
            if (CannonLoaded)
            {
                _popUpIndex++;
            }
        }
        
        // Thirteenth popup: shooting cannon
        else if (_popUpIndex == 12)
        {
            if (CannonShot)
            {
                _popUpIndex++;
            }
        }

        // Tutorial end! spawn the enemy
        else if (_popUpIndex == 13)
        {
            if (finalPopupCoolDown <= 0)
            {
                enemySpawner.StartWorking();
                _popUpIndex++;
            }
            else
            {
                finalPopupCoolDown -= Time.deltaTime;
            }
        }
    }
}
