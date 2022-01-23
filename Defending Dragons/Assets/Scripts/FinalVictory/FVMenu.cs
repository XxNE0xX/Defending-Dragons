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
    
    public void Back()
    {
        SceneManager.LoadScene("LevelSelection");
    }
}
