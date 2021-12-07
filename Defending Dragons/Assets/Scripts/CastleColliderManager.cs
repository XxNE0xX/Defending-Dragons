using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CastleColliderManager : MonoBehaviour
{

    private BoxCollider2D _castleCollider2D;
    
    void Awake()
    {
        _castleCollider2D = GetComponentInChildren<BoxCollider2D>();

    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
