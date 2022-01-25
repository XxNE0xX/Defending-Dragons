using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Serialization;

public class FoodsManager : MonoBehaviour
{
    
    [SerializeField] private GameObject foodsPool;
    [SerializeField] private float foodsGravityScale = 0.5f;
    [SerializeField] private bool allowMoreThanOneFood = false;
    [SerializeField] private int maxAllowedFood = 3;
    [SerializeField] private int baseFoodsCountInPool = 1;
    [SerializeField] private int addMoreFoodsToPoolCount = 4;
    [SerializeField] private AlertPopup maxFoodAlert;
    
    private FoodGenerator _foodGenerator;
    private List<GameObject> _idleFoods;
    private List<Food> _activeFoods;

    private void Awake()
    {
        _foodGenerator = new FoodGenerator();
        _foodGenerator.Init();
        _idleFoods = new List<GameObject>();
        _foodGenerator.GenerateObjects(_idleFoods, foodsPool, baseFoodsCountInPool);
        _activeFoods = new List<Food>();
    }

    /// <summary>
    /// Spawns a single food
    /// </summary>
    /// <returns> The parent wrapper of food object that has been setup and is ready.</returns>
    public Food SpawnAFood()
    {
        // Debug.Log("SpawnAFood in EnemiesManager!");
        Food chosenFood = null;
        
        // Choose an enemy from the idle foods stack
        // if there is at least one idle food, we choose it and continue
        if (_idleFoods.Count > 0 && _activeFoods.Count < maxAllowedFood)
        {
            chosenFood = _idleFoods[_idleFoods.Count - 1].GetComponent<Food>();
            _idleFoods.RemoveAt(_idleFoods.Count - 1);
            // Continue with the local settings that are needed to be set in food object, including its gravity and ground
            chosenFood.Spawn(this, foodsGravityScale);
            // Adding the chosen food to the list of the active foods
            _activeFoods.Add(chosenFood);
        }

        // otherwise, if there is no idle food, we spawn some more to the pool and then choose one
        else if (allowMoreThanOneFood && _activeFoods.Count < maxAllowedFood)
        {
            _foodGenerator.GenerateObjects(_idleFoods, foodsPool, addMoreFoodsToPoolCount);
            // Debug.Log("Idle enemies count: " + _idleFoods.Count);
            chosenFood = _idleFoods[_idleFoods.Count - 1].GetComponent<Food>();
            _idleFoods.RemoveAt(_idleFoods.Count - 1);
            // Continue with the local settings that are needed to be set in food object, including its gravity and ground
            chosenFood.Spawn(this, foodsGravityScale);
            // Adding the chosen food to the list of the active foods
            _activeFoods.Add(chosenFood);
        }
        else
        {
            // We can't generate more food before consuming at least one!
            Debug.Log("Can't get more food before consuming at least one!");
            maxFoodAlert.gameObject.SetActive(true);
        }

        return chosenFood;
    }

    /// <summary>
    /// Despawns a single food
    /// </summary>
    /// <param name="food"> The food instance that needs to be despawned.</param>
    public void DespawnAFood(Food food)
    {
        // We need to despawn the food only if it is active
        if (_activeFoods.Contains(food))
        {
            // Removing the food from the active stack and piling it up on the idle stack
            _activeFoods.Remove(food);
            _idleFoods.Add(food.gameObject);
        }
        // Logging error if something tries to despawn a food twice
        else
        {
            // Debug.LogError("The food with transform InstanceID: " + food.transform.GetInstanceID() + 
            //                " has already been despawned.");
        }
    }
    
}
