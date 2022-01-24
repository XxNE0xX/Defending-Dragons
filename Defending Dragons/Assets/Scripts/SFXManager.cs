using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SFXManager : MonoBehaviour
{

    private Dictionary<string, AudioClip> soundEffects = new Dictionary<string, AudioClip>();
    private AudioSource _mainAudioSource;
    private AudioSource _enemiesAudioSource;
    private AudioSource[] _conveyorsAudio;

    private static SFXManager _i;

    public static SFXManager I
    {
        get
        {
            if (_i == null)
            {
                _i = Instantiate(Resources.Load<SFXManager>("Prefabs/SFXManager"));
                _i._mainAudioSource = _i.GetComponent<AudioSource>();
                
                // Loading the sound effects from the files in Resources
                LoadSFX();
                
                // Adding two different components for the conveyor sound, since it's continuous and looping
                // we can't use the main audio source, since upon stopping, all the sounds will be stopped
                AudioSource cv1 = _i.gameObject.AddComponent<AudioSource>();
                cv1.loop = true;
                cv1.clip = _i.soundEffects["conveyor"];
                AudioSource cv2 = _i.gameObject.AddComponent<AudioSource>();
                cv2.loop = true;
                cv2.clip = _i.soundEffects["conveyor"];
                _i._conveyorsAudio = new[] {cv1, cv2};
                
                // Adding a separate audioSource for the enemies as well, so the change in volume doesn't affect
                // other objects
                _i._enemiesAudioSource = _i.gameObject.AddComponent<AudioSource>();

                // We want to keep this instance available in all the scenes
                DontDestroyOnLoad(_i);
            }

            return _i;
        }
    }

    private static void LoadSFX()
    {
        _i.soundEffects.Add("click", Resources.Load<AudioClip>("Sounds/axe_slash"));
        _i.soundEffects.Add("entrance", Resources.Load<AudioClip>("Sounds/entrance"));
        _i.soundEffects.Add("dragon_roar", Resources.Load<AudioClip>("Sounds/dragon_roar"));
        _i.soundEffects.Add("food_between_ice", Resources.Load<AudioClip>("Sounds/food_between_ice"));
        _i.soundEffects.Add("meat_bite", Resources.Load<AudioClip>("Sounds/meat_bite"));
        _i.soundEffects.Add("meat_eat", Resources.Load<AudioClip>("Sounds/meat_eat"));
        _i.soundEffects.Add("pickup_food", Resources.Load<AudioClip>("Sounds/pickup_food"));
        _i.soundEffects.Add("meat_fall", Resources.Load<AudioClip>("Sounds/meat_fall"));
        _i.soundEffects.Add("conveyor", Resources.Load<AudioClip>("Sounds/conveyor"));
        _i.soundEffects.Add("forge", Resources.Load<AudioClip>("Sounds/forge"));
        _i.soundEffects.Add("dragon_breath", Resources.Load<AudioClip>("Sounds/dragon_breath"));
        _i.soundEffects.Add("load_cannon", Resources.Load<AudioClip>("Sounds/load_cannon"));
        _i.soundEffects.Add("shot_cannon", Resources.Load<AudioClip>("Sounds/shot_cannon"));
        _i.soundEffects.Add("cannonball_drop", Resources.Load<AudioClip>("Sounds/cannonball_drop"));
        _i.soundEffects.Add("cannonball_explode", Resources.Load<AudioClip>("Sounds/cannonball_explode"));
        _i.soundEffects.Add("battering_ram", Resources.Load<AudioClip>("Sounds/battering_ram"));
        _i.soundEffects.Add("metal_on_wood", Resources.Load<AudioClip>("Sounds/metal_on_wood"));
    }

    public void PlayEntrance()
    {
        _mainAudioSource.volume = 0.4f;
        _mainAudioSource.PlayOneShot(soundEffects["entrance"]);
    }

    public void Click()
    {
        _mainAudioSource.volume = 0.6f;
        _mainAudioSource.PlayOneShot(soundEffects["click"]);
    }

    public void DragonRoar()
    {
        _mainAudioSource.volume = 0.3f;
        _mainAudioSource.PlayOneShot(soundEffects["dragon_roar"]);
    }

    public void FoodBetweenIce()
    {
        _mainAudioSource.volume = 0.3f;
        _mainAudioSource.PlayOneShot(soundEffects["food_between_ice"]);
    }

    public void PickupFood()
    {
        _mainAudioSource.volume = 0.3f;
        _mainAudioSource.PlayOneShot(soundEffects["pickup_food"]);
    }

    public void MeatFall()
    {
        _mainAudioSource.volume = 0.3f;
        _mainAudioSource.PlayOneShot(soundEffects["meat_fall"]);
    }

    public void DragonFeed()
    {
        _mainAudioSource.volume = 0.4f;
        _mainAudioSource.PlayOneShot(soundEffects["meat_bite"]);
        _mainAudioSource.PlayDelayed(soundEffects["meat_bite"].length);
        _mainAudioSource.PlayOneShot(soundEffects["meat_eat"]);
    }

    public void ConveyorMoving(bool isDragon)
    {
        if (isDragon)
        {
            _conveyorsAudio[0].volume = 0.2f;
            _conveyorsAudio[0].Play();
        }
        else
        {
            _conveyorsAudio[1].volume = 0.2f;
            _conveyorsAudio[1].Play();
        }
    }

    public void ConveyorStop(bool isDragon)
    {
        if (isDragon)
        {
            _conveyorsAudio[0].Stop();
        }
        else
        {
            _conveyorsAudio[1].Stop();
        }
    }

    public void CreateCannonBall()
    {
        _mainAudioSource.volume = 0.3f;
        _mainAudioSource.PlayOneShot(soundEffects["forge"]);
    }

    public void DragonBreathe()
    {
        _mainAudioSource.volume = 0.6f;
        _mainAudioSource.PlayOneShot(soundEffects["dragon_breath"]);
    }

    public void LoadCannon()
    {
        _mainAudioSource.volume = 0.5f;
        _mainAudioSource.PlayOneShot(soundEffects["load_cannon"]);
    }
    
    public void ShotCannon()
    {
        _mainAudioSource.volume = 0.5f;
        _mainAudioSource.PlayOneShot(soundEffects["shot_cannon"]);
    }

    public void DropCannonball()
    {
        _mainAudioSource.volume = 0.5f;
        _mainAudioSource.PlayOneShot(soundEffects["cannonball_drop"]);
    }

    public void CannonballExplode()
    {
        _mainAudioSource.volume = 0.5f;
        _mainAudioSource.PlayOneShot(soundEffects["cannonball_explode"]);
    }

    public void BatteringRam()
    {
        _enemiesAudioSource.volume = 0.05f;
        _enemiesAudioSource.PlayOneShot(soundEffects["battering_ram"]);
    }

    public void SwordOnDoor()
    {
        _enemiesAudioSource.volume = 0.05f;
        _enemiesAudioSource.PlayOneShot(soundEffects["metal_on_wood"]);
    }
}
