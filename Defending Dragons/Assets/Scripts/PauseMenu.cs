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

    private bool isOptionsOpened;
    
    void Start()
    {
        // Clear the selected object from event system
        EventSystem.current.SetSelectedGameObject(null);
        // Set a new selected object
        EventSystem.current.SetSelectedGameObject(pauseMenuFirstButton);

        isOptionsOpened = false;
    }

    private void Update()
    {
        InputManager();
    }

    private void InputManager()
    {
        // Getting the cancel button twice, closes the menu
        if (Input.GetButtonDown("Cancel") && !isOptionsOpened)
        {
            ResumeGame();
        }
        // Getting the cancel button when the options are opened, closes it
        else if (Input.GetButtonDown("Cancel") && isOptionsOpened)
        {
            CloseOptions();
        }
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
    }

    public void QuitGame()
    {
        SceneManager.LoadScene("StartMenu");
    }

    public void OpenOptions()
    {
        mainPauseMenu.SetActive(false);
        optionsMenu.SetActive(true);
        
        // Clear the selected object from event system
        EventSystem.current.SetSelectedGameObject(null);
        // Set a new selected object
        EventSystem.current.SetSelectedGameObject(optionsFirstButton);

        isOptionsOpened = true;
    }
    
    public void CloseOptions()
    {
        mainPauseMenu.SetActive(true);
        optionsMenu.SetActive(false);
        
        // Clear the selected object from event system
        EventSystem.current.SetSelectedGameObject(null);
        // Set a new selected object
        EventSystem.current.SetSelectedGameObject(optionsClosedButton);

        isOptionsOpened = false;
    }
}
