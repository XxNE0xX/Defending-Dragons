using System.Collections.Generic;
using UnityEngine;

public class FoodGenerator : PoolGenerator
{
    private Food _pfFoodTrigger;
    
    /// <summary>
    /// Initializes the generator.
    /// </summary>
    public new void Init()
    {
        _pfFoodTrigger = GameAssets.I.pfFoodTrigger;
        base.prefab = _pfFoodTrigger.gameObject;
    }

    /// <summary>
    /// Fills the foods object pool for later usage.
    /// </summary>
    /// <param name="idleFoods"> The list that keeps spawned objects.</param>
    /// <param name="foodsPool"> The object of the pool for enemies.</param>
    /// <param name="foodsCount"> Number of enemies that are going to be in the pool.</param>
    public void GenerateObjects(List<GameObject> idleFoods, GameObject foodsPool, int foodsCount)
    {
        base.GenerateObjects(idleFoods, foodsPool, foodsCount, "Food");
    }

}
