using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.EventSystems;

public class LostPanel : MonoBehaviour
{
    [SerializeField] private GameObject lostMenuFirstButton;
    
    // Start is called before the first frame update
    void Start()
    {
        // Clear the selected object from event system
        EventSystem.current.SetSelectedGameObject(null);
        // Set a new selected object
        EventSystem.current.SetSelectedGameObject(lostMenuFirstButton);
    }
    
    public void ReloadLevel()
    {
        Time.timeScale = 1;
        Statics.IsGamePaused = false;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        SFXManager.I.PlayEntrance();
    }

    public void BackToLevelSelection()
    {
        Time.timeScale = 1;
        Statics.IsGamePaused = false;
        SceneManager.LoadScene("LevelSelection");
    }
}
