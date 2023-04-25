using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Events : MonoBehaviour
{
        
    public void ReplayGame()
    {
        Debug.Log("reload scene");
        SceneManager.LoadScene("Level");
    }

    public void GoToMenu()
    {
        Debug.Log("go to menu scene");
        SceneManager.LoadScene(0);
    }
    
    // public void ResumeGame()
    // {
    //     GameController.paused = false;
    // }
}
