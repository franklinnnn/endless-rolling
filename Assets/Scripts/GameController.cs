using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameController : MonoBehaviour
{
    public static bool gameOver = false;
    public GameObject gameOverUI;
    public static bool gameStart;
    public GameObject startingText;
    public static int numberOfCoins;
    public Text coinsText;
    public GameObject controlsUI;
    // public static bool paused = false;
    // public GameObject pauseUI;

    // Start is called before the first frame update
    void Start()
    {
        gameOver = false;
        Time.timeScale = 1;
        gameStart = false;
        numberOfCoins = 0;
        // paused = false;
    }

    // Update is called once per frame
    void Update()
    {
        if(gameOver)
        {
            Time.timeScale = 0f;
            gameOverUI.SetActive(true);
        }

        coinsText.text = "COINS: " + numberOfCoins;

        if(Input.anyKeyDown)
        {
            gameStart = true;
            startingText.SetActive(false);
            controlsUI.SetActive(false);
        }

        // if(paused)
        // {
        //     Time.timeScale = 0f;
        //     pauseUI.SetActive(true);
        // }
        // if(!paused)
        // {
        //     Time.timeScale = 1f;
        //     pauseUI.SetActive(false);
        // }

    }
}
