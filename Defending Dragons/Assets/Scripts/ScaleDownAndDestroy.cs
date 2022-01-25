using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScaleDownAndDestroy : MonoBehaviour
{
    [SerializeField] private float lifeSpan = 4f;
    [SerializeField] private float reductionRate = 0.06f;


    private void Update () 
    {
        if (lifeSpan <= 0)
        {
            transform.localScale -= Vector3.one * reductionRate;
            if (transform.localScale.x <= 0 || transform.localScale.y <= 0)
            {
                Destroy(gameObject);
            }
        }
        else
        {
            lifeSpan -= Time.deltaTime;
        }
    }
}
