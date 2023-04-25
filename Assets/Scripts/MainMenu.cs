using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    private void Start() 
    {
        Time.timeScale = 1;
    }

    IEnumerator DelaySceneLoad()
    {
        yield return new WaitForSeconds(1.0f);
        SceneManager.LoadScene("Level");
    }

    public void PlayGame()
    {
        FindObjectOfType<AudioManager>().PlaySound("Play");
        StartCoroutine(DelaySceneLoad());
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}
