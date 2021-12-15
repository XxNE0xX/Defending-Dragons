using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CannonballGenerator : PoolGenerator
{
    
    private Cannonball _pfCannonball;
    
    /// <summary>
    /// Initializes the generator.
    /// </summary>
    public new void Init()
    {
        _pfCannonball = GameAssets.I.pfCannonball;
        base.prefab = _pfCannonball.gameObject;
    }
    
    /// <summary>
    /// Fills the enemies object pool for later usage.
    /// </summary>
    /// <param name="idleCannonballs"> The list that keeps spawned objects.</param>
    /// <param name="cannonballsPool"> The object of the pool for enemies.</param>
    /// <param name="cannonballsCount"> Number of enemies that are going to be in the pool.</param>
    public void GenerateObjects(List<GameObject> idleCannonballs, GameObject cannonballsPool, int cannonballsCount)
    {
        base.GenerateObjects(idleCannonballs, cannonballsPool, cannonballsCount, "Cannonball");
    }
}
