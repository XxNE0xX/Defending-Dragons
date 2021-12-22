using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CannonPathDrawer : MonoBehaviour
{
    [SerializeField] private int numberOfPoints = 5;
    
    private GameObject[] _points;

    public bool HasBeenSetup { get; private set; }

    public void Setup(float gravityScale, float fallTime, Vector2 speed, Vector3 spawnPosition, Vector3 target)
    {
        _points = new GameObject[numberOfPoints];

        Transform circlePrefab = GameAssets.I.pfPathCircle;

        for (int i = 0; i < numberOfPoints; i++)
        {
            // float time = (fallTime * i) / numberOfPoints;
            // float x = (target.x - spawnPosition.x) * i / numberOfPoints + spawnPosition.x;
            float y = spawnPosition.y * i / numberOfPoints;
            Vector3 position = GetPositionForY(gravityScale, y, speed, spawnPosition);
            _points[i] = Instantiate(circlePrefab, position, Quaternion.identity).gameObject;
            _points[i].transform.SetParent(transform);
        }

        HasBeenSetup = true;
    }

    /// <summary>
    /// Calculating the projectile place at the given time 't'
    /// </summary>
    /// <param name="gravityScale"> The scale of the gravity which with the object falls</param>
    /// <param name="t"> time</param>
    /// <param name="speed"> The initial speed of the projectile</param>
    /// <param name="spawnPosition"> The starting position of the projectile</param>
    /// <returns></returns>
    private Vector3 GetPositionForTime(float gravityScale, 
                                        float t, 
                                        Vector2 speed, 
                                        Vector3 spawnPosition)
    {
        Vector3 acc = Physics2D.gravity * gravityScale;
        return (0.5f * acc * t * t) + (Vector3)(speed * t) + spawnPosition;
    }
    
    private Vector3 GetPositionForX(float gravityScale, 
                                    float x, 
                                    Vector2 speed,
                                    Vector3 spawnPosition)
    {
        float t = (x - spawnPosition.x) / speed.x;
        return GetPositionForTime(gravityScale, t, speed, spawnPosition);
    }
    
    private Vector3 GetPositionForY(float gravityScale, 
                                    float y, 
                                    Vector2 speed,
                                    Vector3 spawnPosition)
    {
        float t = Mathf.Sqrt(-2 * y / (gravityScale * Physics2D.gravity.y));
        return GetPositionForTime(gravityScale, t, speed, spawnPosition);
    }
}
