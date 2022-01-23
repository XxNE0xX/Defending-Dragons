using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PeriodicFade : MonoBehaviour
{
    [SerializeField] private float period = 1f;
    [SerializeField] private float maxAlpha = 1f;
    [SerializeField] private float minAlpha = 0.3f;
    [SerializeField] private int steps = 10;
    private SpriteRenderer sr;

    private void Awake()
    {
        sr = GetComponent<SpriteRenderer>();
    }

    private void OnEnable()
    {
        StartCoroutine(Fade());
    }

    private void OnDisable()
    {
        StopCoroutine(Fade());
    }

    IEnumerator Fade()
    {
        while (true)
        {
            Color c = sr.color;
            float step = ((maxAlpha - minAlpha) / (period / 2)) / steps;
            for (float alpha = maxAlpha; alpha >= minAlpha; alpha -= step)
            {
                c.a = alpha;
                sr.color = c;
                yield return new WaitForSeconds((period / 2) / steps);
            }
            for (float alpha = minAlpha; alpha <= maxAlpha; alpha += step)
            {
                c.a = alpha;
                sr.color = c;
                yield return new WaitForSeconds((period / 2) / steps);
            }
        }
    }
}
