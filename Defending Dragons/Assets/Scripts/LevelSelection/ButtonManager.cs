using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ButtonManager : MonoBehaviour
{
    private Button _button;
    private int _level;
    private int _scenesBeforeMainLevels;

    private void Start()
    {
        _button = GetComponent<Button>();
        _button.onClick.AddListener(GoToLevel);
    }

    public void Init(int level, int scenesBeforeMainLevels)
    {
        _level = level;
        _scenesBeforeMainLevels = scenesBeforeMainLevels;
    }

    private void GoToLevel()
    {
        SceneManager.LoadScene(_scenesBeforeMainLevels + _level - 1);
        SFXManager.I.PlayEntrance();
    }
}
