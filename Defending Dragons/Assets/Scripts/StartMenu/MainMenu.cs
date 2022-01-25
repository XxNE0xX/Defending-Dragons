using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.EventSystems;

public class MainMenu : MonoBehaviour
{
    
    [SerializeField] private GameObject mainMenuFirstButton, optionsFirstButton, optionsClosedButton, aboutFirstButton, aboutClosedButton;
    [SerializeField] private GameObject optionsMenu, mainMenu, aboutPanel;

    private bool _isOptionsOpened;
    
    // Start is called before the first frame update
    void Start()
    {
        _isOptionsOpened = false;
        // Clear the selected object from event system
        EventSystem.current.SetSelectedGameObject(null);
        // Set a new selected object
        EventSystem.current.SetSelectedGameObject(mainMenuFirstButton);
    }

    private void Update()
    {
        InputManager();
    }
    
    private void InputManager()
    {
        if (Input.GetMouseButtonDown(0) || Input.GetMouseButtonDown(1) || Input.GetMouseButtonDown(2))
        {
            CatchMouseClicks();
        }

        if (Input.GetButtonDown("Back") && _isOptionsOpened)
        {
            CloseOptions();
        }
    }
    
    private void CatchMouseClicks()
    {
        // Clear the selected object from event system
        EventSystem.current.SetSelectedGameObject(null);
        // Set a new selected object
        EventSystem.current.SetSelectedGameObject(_isOptionsOpened ? optionsFirstButton : mainMenuFirstButton);
    }

    public void PlayGame()
    {
        SceneManager.LoadScene("LevelSelection");
    }

    public void QuitGame()
    {
        Application.Quit();
    }

    public void OpenOptions()
    {
        optionsMenu.SetActive(true);
        mainMenu.SetActive(false);

        _isOptionsOpened = true;
        // Clear the selected object from event system
        EventSystem.current.SetSelectedGameObject(null);
        // Set a new selected object
        EventSystem.current.SetSelectedGameObject(optionsFirstButton);
    }
    
    public void CloseOptions()
    {
        mainMenu.SetActive(true);
        optionsMenu.SetActive(false);
        
        _isOptionsOpened = false;
        // Clear the selected object from event system
        EventSystem.current.SetSelectedGameObject(null);
        // Set a new selected object
        EventSystem.current.SetSelectedGameObject(optionsClosedButton);
    }
    
    public void OpenAbout()
    {
        aboutPanel.SetActive(true);
        mainMenu.SetActive(false);

        _isOptionsOpened = true;
        // Clear the selected object from event system
        EventSystem.current.SetSelectedGameObject(null);
        // Set a new selected object
        EventSystem.current.SetSelectedGameObject(aboutFirstButton);
    }
    
    public void CloseAbout()
    {
        mainMenu.SetActive(true);
        aboutPanel.SetActive(false);
        
        _isOptionsOpened = false;
        // Clear the selected object from event system
        EventSystem.current.SetSelectedGameObject(null);
        // Set a new selected object
        EventSystem.current.SetSelectedGameObject(aboutClosedButton);
    }
}
