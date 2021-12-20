using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class DragonSpawner : MonoBehaviour
{
    
    private Cannonball _pfCannonball;
    private GameObject prefab;

    private int _maxTFactor;
    
    private void Start()
    {
        prefab = GameAssets.I.pfDragon.gameObject;

        Conveyor conveyor = GetComponentInParent<ConveyorDragon>();

        _maxTFactor = (int)((1 / conveyor.TParamStepsSize) * conveyor.Routes.Length);
    }
    
    public void SpawnDragons(int amount)
    {
        if (amount > 3 || amount < 1)
        {
            Debug.LogError("Wrong number of dragons passed to the spawner.");
            return;
        }

        int step = _maxTFactor / amount;


        for (int i = 1; i <= amount; i++)
        {
            GameObject go = Instantiate(prefab, transform, true);
            Dragon dragon = go.GetComponent<Dragon>();
            dragon.Color = (EnemyColor) i;
            go.name = "Dragon_" + dragon.Color;

            RouteFollowDragon rfDragon = go.GetComponent<RouteFollowDragon>();
            rfDragon.SetInitialTFactor((i - 1) * step);
        }
        

    }
}