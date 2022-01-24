using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class AlertPopup : MonoBehaviour
{
    [SerializeField] private float lifeSpan = 2f;
    [SerializeField] private float maxAlpha = 1f;
    [SerializeField] private float minAlpha = 0;
    [SerializeField] private int steps = 20;
    private TextMeshPro _text;
    private SpriteRenderer _sr;
    

    private void Awake()
    {
        _text = GetComponentInChildren<TextMeshPro>();
        _sr = GetComponent<SpriteRenderer>();
    }

    private void OnEnable()
    {
        StartCoroutine(Fade());
    }
    
    IEnumerator Fade()
    {
        Color tc = _text.color;
        Color src = _sr.color;
        float step = ((maxAlpha - minAlpha) / lifeSpan) / steps;
        for (float alpha = maxAlpha; alpha >= minAlpha; alpha -= step)
        {
            tc.a = alpha;
            src.a = alpha;
            _text.color = tc;
            _sr.color = src;
            yield return new WaitForSeconds(lifeSpan / steps);
        }
        this.gameObject.SetActive(false);
    }

}
