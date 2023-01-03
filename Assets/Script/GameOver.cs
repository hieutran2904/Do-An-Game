using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System;

public class GameOver : MonoBehaviour
{
    public Text scoreText;
    public Text gameOverText;

    public void Setup(int score, String statusGame)
    {
        gameObject.SetActive(true);
        scoreText.text = score.ToString() + " Gift";
        gameOverText.text = statusGame;
    }
    public void RestartButton()
    {
        SceneManager.LoadScene("Game");
    }

    public void ExitButton()
    {
        SceneManager.LoadScene("MenuGame");
    }

}
