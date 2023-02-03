using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerManager : MonoBehaviour
{
    public static bool gameOver;
    public static bool isGameStarted;
    public GameObject gameOverPanel;
    public Text coinsText;
    public static int numberOfCoins; 
    // Start is called before the first frame update
    void Start()
    { 
        gameOver = false;
        gameOverPanel.SetActive(false);
        numberOfCoins = 0;
    }

    // Update is called once per frame
    void Update()
    {
        if (gameOver)
        {
            Time.timeScale = 0;
            gameOverPanel.SetActive(true);
        }
        coinsText.text = "Coins: "+numberOfCoins;
    }

    

    // if(SwipeManager.tap)
    // {
    //     isGameStarted=true;
    // }
}
