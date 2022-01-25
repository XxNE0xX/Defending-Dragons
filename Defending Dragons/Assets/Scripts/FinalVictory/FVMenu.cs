using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.EventSystems;

public class FVMenu : MonoBehaviour
{
    [SerializeField] private GameObject fvMenuFirstButton;
    
    // Start is called before the first frame update
    void Start()
    {
        // Clear the selected object from event system
        EventSystem.current.SetSelectedGameObject(null);
        // Set a new selected object
        EventSystem.current.SetSelectedGameObject(fvMenuFirstButton);
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
            Back();
        }
    }
    
    private void CatchMouseClicks()
    {
        // Clear the selected object from event system
        EventSystem.current.SetSelectedGameObject(null);
        // Set a new selected object
        EventSystem.current.SetSelectedGameObject(fvMenuFirstButton);
    }
    
    public void Back()
    {
        SceneManager.LoadScene("LevelSelection");
    }
}
