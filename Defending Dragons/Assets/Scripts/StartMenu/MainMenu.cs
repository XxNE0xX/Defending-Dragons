using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.EventSystems;

public class MainMenu : MonoBehaviour
{
    
    [SerializeField] private GameObject mainMenuFirstButton, optionsFirstButton, optionsClosedButton;
    
    // Start is called before the first frame update
    void Start()
    {
        // Clear the selected object from event system
        EventSystem.current.SetSelectedGameObject(null);
        // Set a new selected object
        EventSystem.current.SetSelectedGameObject(mainMenuFirstButton);
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
        // Clear the selected object from event system
        EventSystem.current.SetSelectedGameObject(null);
        // Set a new selected object
        EventSystem.current.SetSelectedGameObject(optionsFirstButton);
    }
    
    public void CloseOptions()
    {
        // Clear the selected object from event system
        EventSystem.current.SetSelectedGameObject(null);
        // Set a new selected object
        EventSystem.current.SetSelectedGameObject(optionsClosedButton);
    }
}
