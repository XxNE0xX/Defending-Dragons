using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class CannonballsManager : MonoBehaviour
{
    private CannonballGenerator _cannonballGenerator;
    [SerializeField] private GameObject cannonballsPool;
    [SerializeField] private Tilemap groundTiles;
    [SerializeField] private EnemiesManager enemiesManager;
    private List<GameObject> _idleCannonballs;
    private List<Cannonball> _activeCannonballs;
    
    private void Awake()
    {
        _cannonballGenerator = new CannonballGenerator();
        _cannonballGenerator.Init();
        _idleCannonballs = new List<GameObject>();
        GeneratePoolObjects(Statics.BaseCannonballsCountInPool);
        _activeCannonballs = new List<Cannonball>();
    }

    /// <summary>
    /// Generates some entities to be added to the pool.
    /// Also some necessary fields of the cannonballs will be set for further actions.
    /// [CannonballManager = self], [GroundTiles], [EnemiesManager]
    /// </summary>
    /// <param name="count"> The amount of spawned entities.</param>
    private void GeneratePoolObjects(int count)
    {
        _cannonballGenerator.GenerateObjects(_idleCannonballs, cannonballsPool, count);
        foreach (GameObject go in _idleCannonballs)
        {
            Cannonball cannonball = go.GetComponent<Cannonball>();
            cannonball.CannonballsManager = this;
            cannonball.GroundTiles = groundTiles;
            cannonball.EnemiesManager = enemiesManager;
        }
    }
    
    /// <summary>
    /// Spawns a single enemy
    /// </summary>
    /// <returns> The cannonball object that has been setup and is ready.</returns>
    public Cannonball SpawnACannonball(EnemyColor color, Vector3 position)
    {
        Cannonball chosenCannonball;
        
        // Choose a cannonball from the idle cannonballs stack
        // if there is at least one idle cannonball, we choose it and continue
        if (_idleCannonballs.Count > 0)
        {
            chosenCannonball = _idleCannonballs[_idleCannonballs.Count - 1].GetComponent<Cannonball>();
            _idleCannonballs.RemoveAt(_idleCannonballs.Count - 1);
        }

        // otherwise, if there is no idle cannonball, we spawn some more to the pool and then choose one
        else
        {
            GeneratePoolObjects(Statics.AddMoreCannonballsToPoolCount);
            chosenCannonball = _idleCannonballs[_idleCannonballs.Count - 1].GetComponent<Cannonball>();
            _idleCannonballs.RemoveAt(_idleCannonballs.Count - 1);
        }

        chosenCannonball.EnemyColor = color;
        chosenCannonball.Spawn(position);
        
        // Adding the chosen cannonball to the list of the active cannonballs
        _activeCannonballs.Add(chosenCannonball);

        return chosenCannonball;
    }

    /// <summary>
    /// Returns the given object to the idle pool.
    /// </summary>
    /// <param name="cannonball"> The target object</param>
    public void DespawnCannonball(Cannonball cannonball)
    {
        // We need to despawn the cannonball only if it is active
        if (_activeCannonballs.Contains(cannonball))
        {
            // Removing the cannonball from the active stack and piling it up on the idle stack
            _activeCannonballs.Remove(cannonball);
            _idleCannonballs.Add(cannonball.gameObject);

            Debug.Log("The cannonball with transform InstanceID: " + cannonball.transform.GetInstanceID() + 
                      " despawned.");
        }
        // Logging error if something tries to despawn an cannonball twice
        else
        {
            Debug.LogError("The cannonball with transform InstanceID: " + cannonball.transform.GetInstanceID() + 
                           " has already been despawned.");
        }
    }
}
