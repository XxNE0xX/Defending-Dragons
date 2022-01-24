using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomDragonRoar : MonoBehaviour
{
    [SerializeField] private float coolDown = 10f;
    private float _lastRoarDiff = 4f;
    void Update()
    {
        if (_lastRoarDiff <= 0f)
        {
            float chance = Random.Range(0f, 1f);
            if (chance < 0.01f)
            {
                _lastRoarDiff = coolDown;
                SFXManager.I.DragonRoar();
            }
        }

        else
        {
            _lastRoarDiff -= Time.deltaTime;
        }
    }
}
