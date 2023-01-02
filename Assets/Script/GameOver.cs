using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;


public class GameOver : MonoBehaviour
{
    public Text scoreText;

    public void Setup(int score)
    {
        gameObject.SetActive(true);
        scoreText.text = score.ToString() + " Gift";
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
