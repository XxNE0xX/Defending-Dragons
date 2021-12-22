using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Cannon : MonoBehaviour
{
    [SerializeField] private Transform target;
    [SerializeField] private float cannonWidth = 0.5f;
    [SerializeField] private float cannonHeight = 0.25f;
    [SerializeField] private float forceFactor = 1f;
    [Tooltip("Check if the cannon is on the right side of the castle.")]
    [SerializeField] private bool isRight;

    [SerializeField] private TextMeshPro readyText;
    [SerializeField] private CannonReloadTextAnimation _reloadTextAnimation;

    private CannonballsManager _cannonballsManager;
    private CannonPathDrawer _cannonPathDrawer;

    private Cannonball _currentCannonball;
    private bool _loaded;
    private bool _playerNear;

    private Vector3 _cannonballSpawnPosition;
    private float _cannonballFallTime;
    
    public bool Loaded => _loaded;

    private void Awake()
    {
        _cannonballsManager = GetComponentInParent<CannonsParent>().cannonballsManager;
        _cannonPathDrawer = GetComponentInChildren<CannonPathDrawer>(true);
        
        // Determine the spawn position of the cannonball
        _cannonballSpawnPosition = transform.position;
        if (isRight)
        {
            _cannonballSpawnPosition.x += cannonWidth + _cannonballsManager.CannonballWidth;
        }
        else
        {
            _cannonballSpawnPosition.x -= cannonWidth + _cannonballsManager.CannonballWidth;
        }
        _cannonballSpawnPosition.y += cannonHeight / 2;
    }

    /// <summary>
    /// Spawns a cannonball and aims it to the target position
    /// </summary>
    public void Shoot()
    {
        if (_loaded)
        {
            _currentCannonball.Shoot(_cannonballSpawnPosition,
                VelocityCalculator(_cannonballsManager.CannonballsGravity));

            _currentCannonball = null;
            _loaded = false;
            _reloadTextAnimation.gameObject.SetActive(true);
            readyText.gameObject.SetActive(false);
        }
        else
        {
            _reloadTextAnimation.ShowWarningAnimation();
            Debug.Log("Cannon is not loaded!");
        }
    }

    public void Load(Cannonball cannonball)
    {
        if (!_loaded)
        {
            _currentCannonball = cannonball;
            _currentCannonball.Loaded();

            _loaded = true;
            _reloadTextAnimation.gameObject.SetActive(false);
            readyText.gameObject.SetActive(true);
            readyText.color = Statics.GetColorFromEnemyColor(cannonball.EnemyColor);
        }
        else
        {
            Debug.Log("The cannon is already loaded.");
        }
    }

    private void ShowPath(bool yes)
    {
        // Showing the path
        if (yes)
        {
            if (!_cannonPathDrawer.HasBeenSetup) // If the path hasn't been setup before
            {
                float gravityScale = _cannonballsManager.CannonballsGravity;
                // Initial setup to place the objects in place
                _cannonPathDrawer.Setup(gravityScale,
                                        FindFallTime(gravityScale), 
                                        VelocityCalculator(gravityScale),
                                        _cannonballSpawnPosition,
                                        target.position);
            }
            _cannonPathDrawer.gameObject.SetActive(true);
            target.GetComponent<SpriteRenderer>().enabled = true;
        }
        // Hiding the path
        else
        {
            Debug.Log(_cannonPathDrawer.gameObject);
            _cannonPathDrawer.gameObject.SetActive(false);
            target.GetComponent<SpriteRenderer>().enabled = false;
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            _playerNear = true;
            ShowPath(true);
            if (!_loaded)
            {
                _reloadTextAnimation.gameObject.SetActive(true);
            }
        }
    }
    
    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            _playerNear = false;
            ShowPath(false);
            _reloadTextAnimation.gameObject.SetActive(false);
        }
    }

    private float FindFallTime(float gravityScale)
    {
        gravityScale *= Mathf.Abs(Physics2D.gravity.y);
        float t2 = 2 * (_cannonballSpawnPosition.y / gravityScale);
        return Mathf.Sqrt(t2);
    }

    /// <summary>
    /// Calculates the amount of needed force based on the movement equation of the cannonball
    /// </summary>
    /// <param name="mass"> mass of the cannonball</param>
    /// <param name="gravityScale"> the gravityScale that is being applied to the cannonball</param>
    /// <param name="spawnPosition"> starting position of the cannonball</param>
    /// <returns></returns>
    private Vector2 VelocityCalculator(float gravityScale)
    {
        float Vx0 = (target.position.x - _cannonballSpawnPosition.x) / FindFallTime(gravityScale);
        return new Vector2(Vx0, 0);
    }
}
