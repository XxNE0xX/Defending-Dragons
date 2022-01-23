using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.EventSystems;

public class VictoryMenu : MonoBehaviour
{
    [SerializeField] private GameObject victoryMenuFirstButton;
    
    // Start is called before the first frame update
    void Start()
    {
        // Clear the selected object from event system
        EventSystem.current.SetSelectedGameObject(null);
        // Set a new selected object
        EventSystem.current.SetSelectedGameObject(victoryMenuFirstButton);
    }
    
    public void NextLevel()
    {
        Time.timeScale = 1;
        Statics.IsGamePaused = false;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    public void BackToLevelSelection()
    {
        Time.timeScale = 1;
        Statics.IsGamePaused = false;
        SceneManager.LoadScene("LevelSelection");
    }
}
