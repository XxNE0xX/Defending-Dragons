using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class CannonReloadTextAnimation : MonoBehaviour
{
    private TextMeshPro _textMesh;
    private float _animationTimer;
    
    private void Awake()
    {
        _textMesh = GetComponent<TextMeshPro>();
    }

    private void Update()
    {
        ColorManager();
    }

    public void ShowWarningAnimation()
    {
        _animationTimer = Statics.RELOAD_TEXT_BLINKING_TIME;
    }
    
    private void ColorManager()
    {
        if (_animationTimer * 1000 % 200 >= 100)
        {
            _textMesh.color = Color.red;
        }
        
        else
        {
            _textMesh.color = Color.white;
        }
        
        _animationTimer -= Time.deltaTime;
    }
    
}
