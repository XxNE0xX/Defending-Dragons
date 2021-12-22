using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameAssets : MonoBehaviour
{
    private static GameAssets _i;

    public static GameAssets I
    {
        get
        {
            if (_i == null)
            {
                _i = Instantiate(Resources.Load<GameAssets>("Prefabs/GameAssets"));
            }

            return _i;
        }
    }

    public Enemy pfEnemy;
    public Cannonball pfCannonball;
    public DamagePopup pfDamagePopup;
    public Food pfFoodTrigger;
    public Dragon pfDragon;
    public Transform pfPathCircle;
}


