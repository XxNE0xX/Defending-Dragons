using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Dragon : MonoBehaviour
{
    private EnemyColor _color;
    private int _strength;

    private bool _nearFood;
    private Food _closeFood;
    private bool _nearCannonball;
    private Cannonball _closeCannonball;
    
    [SerializeField] private GameObject blowText;
    [SerializeField] private GameObject eatText;
    [SerializeField] private GameObject strengthText;

    private TutorialManager _tutorialManager;
    
    private string _blow = "Blow?";
    private string _hungry = "I am hungry!";

    public EnemyColor Color
    {
        get => _color;
        set
        {
            _color = value;
            GetComponent<SpriteRenderer>().color = _color switch
            {
                EnemyColor.Blue => Statics.Blue,
                EnemyColor.Red => Statics.Red,
                EnemyColor.Yellow => Statics.Yellow,
                EnemyColor.Green => Statics.Green,
                EnemyColor.Purple => Statics.Purple,
                EnemyColor.Orange => Statics.Orange,
                EnemyColor.Black => Statics.Black,
                _ => Statics.DefaultColor
            };
            blowText.GetComponent<TextMeshPro>().color = Statics.GetColorFromEnemyColor(_color);
            strengthText.GetComponent<TextMeshPro>().color = Statics.GetColorFromEnemyColor(_color);
        }
    }

    private void Awake()
    {
        GameObject TMGO = GameObject.FindWithTag("TutorialManager");
        if (TMGO != null)
        {
            _tutorialManager = TMGO.GetComponent<TutorialManager>();
        }
    }

    private void Update()
    {
        InputManager();
    }

    private void InputManager()
    {
        // Only read the inputs if the game is not paused
        if (Statics.IsGamePaused) return;
        
        if (Input.GetButtonDown("DragonFeed") && _nearFood)
        {
            Feed();
            SFXManager.I.DragonFeed();
            if (_tutorialManager != null)
            {
                _tutorialManager.FeedDragon = true;
            }
        }

        if (Input.GetButtonDown("DragonBlow") && _nearCannonball)
        {
            Blow();
            SFXManager.I.DragonBreathe();
            if (_tutorialManager != null)
            {
                _tutorialManager.BlowDragon = true;
            }
        }
    }

    private void Feed()
    {
        // Despawn the food when it's eaten
        _closeFood.Despawn();
        _strength++;
        strengthText.GetComponent<TextMeshPro>().SetText(_strength.ToString());
    }

    private void Blow()
    {
        if (_strength > 0)
        {
            _closeCannonball.Power += _strength;
            _closeCannonball.EnemyColor = DetermineNewColor(_closeCannonball.EnemyColor);
            _strength = 0;
            strengthText.GetComponent<TextMeshPro>().SetText(_strength.ToString());
        }
        else
        {
            Debug.Log("Dragon needs to be fed at least once!");
        }
    }

    private EnemyColor DetermineNewColor(EnemyColor current)
    {

        // In the case it is just a power-up, return the current color
        if (_color == current)
            return _color;
        
        switch (current)
        {
            // If the cannonball has no color yet
            case EnemyColor.Default:
                return _color;
            // Blue variants
            case EnemyColor.Blue:
                if (_color == EnemyColor.Red)
                    return EnemyColor.Purple;
                else if (_color == EnemyColor.Yellow)
                    return EnemyColor.Green;
                break;
            // Yellow variants
            case EnemyColor.Yellow:
                if (_color == EnemyColor.Blue)
                    return EnemyColor.Green;
                else if (_color == EnemyColor.Red)
                    return EnemyColor.Orange;
                break;
            // Red variants
            case EnemyColor.Red:
                if (_color == EnemyColor.Blue)
                    return EnemyColor.Purple;
                else if (_color == EnemyColor.Yellow)
                    return EnemyColor.Orange;
                break;
            // Anything else should become black
            default:
                return EnemyColor.Black;
        }

        // For debug purposes
        return EnemyColor.Default;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Cannonball"))
        {
            _nearCannonball = true;
            blowText.SetActive(true);
            if (_strength > 0)
            {
                blowText.GetComponent<TextMeshPro>().SetText(_blow);
            }
            else
            {
                blowText.GetComponent<TextMeshPro>().SetText(_hungry);
            }
            _closeCannonball = other.gameObject.GetComponent<Cannonball>();
        }
        if (other.gameObject.CompareTag("Food"))
        {
            _nearFood = true;
            eatText.SetActive(true);
            _closeFood = other.gameObject.GetComponent<Food>();
        }
    }

    private void OnTriggerStay2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Cannonball"))
        {
            _nearCannonball = true;
            blowText.SetActive(true);
            if (_strength > 0)
            {
                blowText.GetComponent<TextMeshPro>().SetText(_blow);
            }
            else
            {
                blowText.GetComponent<TextMeshPro>().SetText(_hungry);
            }
            _closeCannonball = other.gameObject.GetComponent<Cannonball>();
        }
        if (other.gameObject.CompareTag("Food"))
        {
            _nearFood = true;
            eatText.SetActive(true);
            _closeFood = other.gameObject.GetComponent<Food>();
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Cannonball"))
        {
            _nearCannonball = false;
            blowText.SetActive(false);
            _closeCannonball = null;
        }
        else if (other.gameObject.CompareTag("Food"))
        {
            _nearFood = false;
            eatText.SetActive(false);
            _closeFood = null;
        }
    }
}
