using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialManager : MonoBehaviour
{
    [SerializeField] private GameObject[] popUps;

    private int _popUpIndex;

    private void Update()
    {

        for (int i = 0; i < popUps.Length; i++)
        {
            if (i == _popUpIndex)
            {
                popUps[_popUpIndex].SetActive(true);
            }
            else
            {
                popUps[i].SetActive(false);
            }
        }
        if (_popUpIndex == 0) // Tutorial start
        {
            
        }
    }
}
