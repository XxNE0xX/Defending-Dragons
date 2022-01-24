using System;
using System.Collections;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine.EventSystems;

public class LevelManager : MonoBehaviour
{

    private List<Button> _levelButtons;
    [SerializeField] private int scenesBeforeMainLevels = 2;
    [SerializeField] private int scenesAfterMainLevels = 1;
    private List<string> _cityNames;

    private void Start()
    {
        // The final -1 is because of the final Victory scene
        int maxLevels = SceneManager.sceneCountInBuildSettings - scenesBeforeMainLevels - scenesAfterMainLevels;
        // Create buttons
        InstantiateButtons(maxLevels);
        
        // Read the city names from file
        _cityNames = new List<string>();
        ReadCityNames();
        // If there are not enough names in the file, name the cities a single value
        if (_cityNames.Count < _levelButtons.Count)
        {
            for (int i = _cityNames.Count; i < _levelButtons.Count; i++)
            {
                _cityNames.Add("Placeholder");
            }
        }
        int currentLevel = PlayerPrefs.GetInt("currentLevel", scenesBeforeMainLevels);
        int completedLevel = PlayerPrefs.GetInt("completedLevel", scenesBeforeMainLevels - 1);

        for (int i = 0; i < _levelButtons.Count; i++)
        {
            // Replace the text on the bottom of the button
            _levelButtons[i].GetComponentInChildren<TextMeshProUGUI>().SetText("Level " + (i + 1) + "\n" + _cityNames[i]);
            // Let the button know which level it would guide us to
            _levelButtons[i].GetComponent<ButtonManager>().Init(i + 1, scenesBeforeMainLevels);
            // If the level is not unlocked yet, don't allow the player to interact with it, and show a lock on the sprite
            if (i + scenesBeforeMainLevels > currentLevel)
            {
                _levelButtons[i].interactable = false;
                _levelButtons[i].GetComponent<Image>().sprite = Resources.Load<Sprite>("Sprites/LevelNotPassed");
                _levelButtons[i].transform.Find("Locked").gameObject.SetActive(true);
            }
            // If the level is unlocked recently, remove the lock, but show the incomplete sprite
            else if (i + scenesBeforeMainLevels == currentLevel && currentLevel != completedLevel)
            {
                _levelButtons[i].interactable = true;
                _levelButtons[i].GetComponent<Image>().sprite = Resources.Load<Sprite>("Sprites/LevelNotPassed");
                _levelButtons[i].transform.Find("Locked").gameObject.SetActive(false);
            }
            // If the level is passed successfully, show the completed sprite
            else
            {
                _levelButtons[i].interactable = true;
                _levelButtons[i].GetComponent<Image>().sprite = Resources.Load<Sprite>("Sprites/LevelPassed");
                _levelButtons[i].transform.Find("Locked").gameObject.SetActive(false);
            }
        }
        
        // Clear the selected object from event system
        EventSystem.current.SetSelectedGameObject(null);
        // Set a new selected object
        EventSystem.current.SetSelectedGameObject(_levelButtons[0].gameObject);
    }

    private void Update()
    {
        InputManager();
    }

    private void InputManager()
    {
        if (Input.GetButtonDown("Cancel"))
        {
            SceneManager.LoadScene("StartMenu");
        }
    }

    /// <summary>
    /// Creates buttons that would guide the player to each level.
    /// </summary>
    /// <param name="maxLevels"> The maximum number of built levels.</param>
    private void InstantiateButtons(int maxLevels)
    {
        _levelButtons = new List<Button>();
        for (int i = 0; i < maxLevels; i++)
        {
            GameObject newButton = Instantiate(GameAssets.I.pfLevelButton, this.transform, true);
            _levelButtons.Add(newButton.GetComponent<Button>());
        }
    }

    /// <summary>
    /// Reads names of the levels from a file in the root of Resources named "CityNames"
    /// </summary>
    private void ReadCityNames()
    {
        TextAsset cityNames = Resources.Load<TextAsset>("CityNames");
        string[] array = cityNames.text.Split('\n');
        if (array.Length == 1)
        {
            if (array[0] == "")
            {
                return;
            }
        }
        _cityNames = new List<string>(array);
    }
}
