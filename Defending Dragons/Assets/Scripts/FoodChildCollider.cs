using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FoodChildCollider : ChildCollider
{
    private void Awake()
    {
        // 32 is maximum value of the layermasks
        for (int i = 0; i < 32; i++)
        {
            if (LayerMask.NameToLayer("CastleObstacles") != i &&
                LayerMask.NameToLayer("LadderTop") != i &&
                LayerMask.NameToLayer("Default") != i)  // To prevent falling from top of the underground table
            {
                Physics2D.IgnoreLayerCollision(LayerMask.NameToLayer("FoodChildCollider"), i);
            }
        }
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.layer == LayerMask.NameToLayer("Default"))
        {
            GameObject TMGO = GameObject.FindWithTag("TutorialManager");
            if (TMGO != null)
            {
                TutorialManager tutorialManager = GameObject.FindWithTag("TutorialManager").GetComponent<TutorialManager>();
                if (tutorialManager != null)
                {
                    tutorialManager.DroppedFood = true;
                }
            }
            SFXManager.I.MeatFall();
        }
    }
}
