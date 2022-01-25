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

    private bool _climbed;
    private bool _pickedFood;
    private bool _droppedFood;
    private bool _feedDragon;
    private bool _createdCannonball;
    private bool _blowDragon;
    private bool _cannonballPicked;
    private bool _cannonLoaded;
    private bool _cannonShot;
    
    public bool Climbed
    {
        set
        {
            if (_popUpIndex >= 2)
            {
                _climbed = true;
            }
        }
    }

    public bool PickedFood { set
    {
        if (_popUpIndex >= 3)
        {
            _pickedFood = value;
        }
    } }
    public bool DroppedFood { set
    {
        if (_popUpIndex >= 4)
        {
            _droppedFood = value;
        }
    } }
    public bool FeedDragon { set
    {
        if (_popUpIndex >= 6)
        {
            _feedDragon = value;
        }
    } }
    public bool CreatedCannonball { set => _createdCannonball = value; }
    public bool BlowDragon { set
    {
        if (_popUpIndex >= 9)
        {
            _blowDragon = value;
        }
    } }
    public bool CannonballPicked { set
    {
        if (_popUpIndex >= 10)
        {
            _cannonballPicked = value;
        }
    } }
    public bool CannonLoaded { set
    {
        if (_popUpIndex >= 11)
        {
            _cannonLoaded = value;
        }
    } }
    public bool CannonShot { set
    {
        if (_popUpIndex >= 12)
        {
            _cannonShot = value;
        }
    } }

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
            if (_climbed)
            {
                _popUpIndex++;
            }
        }
        // Fourth popup: picking up food
        else if (_popUpIndex == 3)
        {
            if (_pickedFood)
            {
                _popUpIndex++;
            }
        }
        // Fifth popup: dropping food for the dragon
        else if (_popUpIndex == 4)
        {
            if (_droppedFood)
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
            if (_feedDragon)
            {
                _popUpIndex++;
            }
        }
        
        // Eighth popup: creating cannonball
        else if (_popUpIndex == 7)
        {
            if (_createdCannonball)
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
            if (_blowDragon)
            {
                _popUpIndex++;
            }
        }
        
        // Eleventh popup: picking up cannonball
        else if (_popUpIndex == 10)
        {
            if (_cannonballPicked)
            {
                _popUpIndex++;
            }
        }
        
        // Twelfth popup: loading cannon
        else if (_popUpIndex == 11)
        {
            if (_cannonLoaded)
            {
                _popUpIndex++;
            }
        }
        
        // Thirteenth popup: shooting cannon
        else if (_popUpIndex == 12)
        {
            if (_cannonShot)
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
