using System.Collections;
using System.Collections.Generic;
using System.Linq.Expressions;
using UnityEngine;
using UnityEngine.Tilemaps;

public class CannonballsManager : MonoBehaviour
{
    private CannonballGenerator _cannonballGenerator;
    [SerializeField] private GameObject cannonballsPool;
    [SerializeField] private Tilemap groundTiles;
    [SerializeField] private EnemiesManager enemiesManager;
    [SerializeField] private int baseCannonballsCountInPool = 15;
    [SerializeField] private int addMoreCannonballsToPoolCount = 5;
    [SerializeField] private float cannonballsGravity = 2f; 
    [SerializeField] private float cannonballWidth = 0.3125f;
    [SerializeField] private float cannonballWeight = 0.5f;
    
    private List<GameObject> _idleCannonballs;
    private List<Cannonball> _activeCannonballs;

    public float CannonballsGravity => cannonballsGravity;
    public float CannonballWidth => cannonballWidth;
    public float CannonballWeight => cannonballWeight;

    private void Awake()
    {
        _cannonballGenerator = new CannonballGenerator();
        _cannonballGenerator.Init();
        _idleCannonballs = new List<GameObject>();
        GeneratePoolObjects(baseCannonballsCountInPool);
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
            cannonball.GetComponent<Rigidbody2D>().mass = cannonballWeight;
        }
    }
    
    /// <summary>
    /// Spawns a single enemy
    /// </summary>
    /// <returns> The cannonball object that has been setup and is ready.</returns>
    public Cannonball SpawnACannonball(EnemyColor color, Vector3 position, Vector2 initialForce)
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
            GeneratePoolObjects(addMoreCannonballsToPoolCount);
            chosenCannonball = _idleCannonballs[_idleCannonballs.Count - 1].GetComponent<Cannonball>();
            _idleCannonballs.RemoveAt(_idleCannonballs.Count - 1);
        }

        chosenCannonball.EnemyColor = color;
        chosenCannonball.Spawn(position, cannonballsGravity, cannonballWeight, initialForce);
        
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
