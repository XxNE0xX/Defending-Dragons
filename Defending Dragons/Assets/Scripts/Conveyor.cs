using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Conveyor : MonoBehaviour
{
    [SerializeField] protected float tParamStepsSize = 0.005f;

    private Route[] routes;
    
    public Route[] Routes => routes;
    public float TParamStepsSize => tParamStepsSize;
    
    private void Awake()
    {
        routes = transform.GetComponentsInChildren<Route>();
    }
}
