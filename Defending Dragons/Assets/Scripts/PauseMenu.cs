using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.EventSystems;

public class PauseMenu : MonoBehaviour
{
    [SerializeField] private GameObject pauseMenuFirstButton, optionsFirstButton, optionsClosedButton;
    [SerializeField] private GameObject optionsMenu, mainPauseMenu;

    [SerializeField] private GameManager gameManager;

    private bool _isOptionsOpened;
    
    void Start()
    {
        _isOptionsOpened = false;
        // Clear the selected object from event system
        EventSystem.current.SetSelectedGameObject(null);
        // Set a new selected object
        EventSystem.current.SetSelectedGameObject(pauseMenuFirstButton);
    }

    private void Update()
    {
        InputManager();
    }

    private void InputManager()
    {
        // Getting the cancel button twice, closes the menu
        if (Input.GetButtonDown("Cancel") && !_isOptionsOpened)
        {
            ResumeGame();
        }
        // Getting the cancel button when the options are opened, closes it
        else if (Input.GetButtonDown("Cancel") && _isOptionsOpened)
        {
            CloseOptions();
        }
        if (Input.GetMouseButtonDown(0) || Input.GetMouseButtonDown(1) || Input.GetMouseButtonDown(2))
        {
            CatchMouseClicks();
        }
    }
    
    private void CatchMouseClicks()
    {
        // Clear the selected object from event system
        EventSystem.current.SetSelectedGameObject(null);
        // Set a new selected object
        EventSystem.current.SetSelectedGameObject(_isOptionsOpened ? optionsFirstButton : pauseMenuFirstButton);
    }

    public void ResumeGame()
    {
        Time.timeScale = 1;
        Statics.IsGamePaused = false;
        this.gameObject.SetActive(false);
    }

    public void ReloadGame()
    {
        Time.timeScale = 1;
        Statics.IsGamePaused = false;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        SFXManager.I.PlayEntrance();
    }

    public void QuitGame()
    {
        Time.timeScale = 1;
        Statics.IsGamePaused = false;
        SceneManager.LoadScene("StartMenu");
    }

    public void OpenOptions()
    {
        mainPauseMenu.SetActive(false);
        optionsMenu.SetActive(true);
        
        _isOptionsOpened = true;
        // Clear the selected object from event system
        EventSystem.current.SetSelectedGameObject(null);
        // Set a new selected object
        EventSystem.current.SetSelectedGameObject(optionsFirstButton);
    }
    
    public void CloseOptions()
    {
        mainPauseMenu.SetActive(true);
        optionsMenu.SetActive(false);
        
        _isOptionsOpened = false;
        // Clear the selected object from event system
        EventSystem.current.SetSelectedGameObject(null);
        // Set a new selected object
        EventSystem.current.SetSelectedGameObject(optionsClosedButton);
    }
}
