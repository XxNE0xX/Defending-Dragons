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

        if (Input.GetButtonDown("Back"))
        {
            BackToLevelSelection();
        }
    }
    
    private void CatchMouseClicks()
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
