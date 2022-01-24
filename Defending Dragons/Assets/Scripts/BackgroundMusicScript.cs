using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackgroundMusicScript : MonoBehaviour
{
    public static BackgroundMusicScript BGMusicInstance;

    private void Awake()
    {
        if (BGMusicInstance != null && BGMusicInstance != this)
        {
            Destroy(this.gameObject);
            return;
        }

        BGMusicInstance = this;
        DontDestroyOnLoad(this);
    }
}
