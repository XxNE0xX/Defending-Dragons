using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CannonballChildCollider : ChildCollider
{
    private void Awake()
    {
        // 32 is maximum value of the layermasks
        for (int i = 0; i < 32; i++)
        {
            if (LayerMask.NameToLayer("CastleObstacles") != i &&
                LayerMask.NameToLayer("LadderTop") != i &&
                LayerMask.NameToLayer("FoodEntrance") != i)  // To prevent falling into the food entrance system
            {
                Physics2D.IgnoreLayerCollision(LayerMask.NameToLayer("CannonballChildCollider"), i);
            }
        }
    }
}
