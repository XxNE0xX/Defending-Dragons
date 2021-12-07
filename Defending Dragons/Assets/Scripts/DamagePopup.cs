using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using Random = UnityEngine.Random;

public class DamagePopup : MonoBehaviour
{

    private TextMeshPro _textMesh;
    private Color _textColor;
    private float _disappearTimer;
    private Vector3 _moveVector;

    private static int _sortingOrder;

    /// <summary>
    /// A self-contained function to create a damage popup
    /// </summary>
    /// <param name="position"> The position of the popup </param>
    /// <param name="damageAmount"> The damage amount that is going to be shown. </param>
    /// <param name="toLeft"> Determines whether the popup should move towards left or right while fading </param>
    /// <returns></returns>
    public static DamagePopup Create(Vector3 position, int damageAmount, bool toLeft)
    {
        DamagePopup pfDamagePopup = GameAssets.I.pfDamagePopup;
        DamagePopup dmgPopup = Instantiate(pfDamagePopup, position, Quaternion.identity);
        dmgPopup.Setup(damageAmount, toLeft);

        return dmgPopup;
    }
    
    private void Awake()
    {
        _textMesh = GetComponent<TextMeshPro>();
    }

    /// <summary>
    /// Setting up the text that is going to be shown in the text mesh
    /// </summary>
    /// <param name="damageAmount"> The text </param>
    /// <param name="toLeft"> Determines whether the popup should move towards left or right while fading</param>
    private void Setup(int damageAmount, bool toLeft)
    {
        _textMesh.SetText(damageAmount.ToString());
        // The color is determined based on the damage value
        _textMesh.color = CalculateColorBasedOnDamage(damageAmount);
        _textColor = _textMesh.color;
        _disappearTimer = Statics.DisappearTimerMax;

        // The popup movement while fading
        if (toLeft)
            _moveVector = new Vector3(-.7f, 1) * Random.Range(0.5f, 1f);
        else
            _moveVector = new Vector3(.7f, 1) * Random.Range(0.5f, 1f);
        
        // Making sure that newer popups always stack over the old ones
        _sortingOrder++;
        _textMesh.sortingOrder = _sortingOrder;
    }

    private void Update()
    {
        transform.position += _moveVector * Time.deltaTime;
        _moveVector -= _moveVector * (0.8f * Time.deltaTime);

        TimerManager();
        
    }

    /// <summary>
    /// Managing the size scaling of the popup, and the time of its disappearance.
    /// </summary>
    private void TimerManager()
    {
        float increaseScaleAmount = 0.5f;
        float decreaseScaleAmount = 1f;
        
        if (_disappearTimer > Statics.DisappearTimerMax / 2)
        {
            // First Half of the popup's life
            // Increasing the size of the popup
            transform.localScale += Vector3.one * (increaseScaleAmount * Time.deltaTime);
        }
        
        else
        {
            // Second Half of the popup's life
            // Decreasing the size of the popup
            transform.localScale -= Vector3.one * (decreaseScaleAmount * Time.deltaTime);
        }
        
        _disappearTimer -= Time.deltaTime;
        if (_disappearTimer < 0)
        {
            // Starting to disappear
            float disappearSpeed = 3f;
            _textColor.a -= disappearSpeed * Time.deltaTime;
            _textMesh.color = _textColor;
            if (_textColor.a < 0)
            {
                Destroy(gameObject);
            }
        }
    }

    private Color32 CalculateColorBasedOnDamage(int damageAmount)
    {
        int greenColorRange = Statics.MaxDamagePopupGreen - Statics.MinDamagePopupGreen;
        int dmgRange = Statics.MaxEnemyDamage - Statics.MinEnemyDamage;
        int g = Statics.MaxDamagePopupGreen - 
                (greenColorRange * (damageAmount - Statics.MinEnemyDamage) / dmgRange);
        
        // We want the color to be a shade of yellow-orange
        return new Color32(255, (byte)g, 0, 255);
    }
}
