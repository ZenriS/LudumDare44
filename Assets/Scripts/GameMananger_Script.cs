using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameMananger_Script : MonoBehaviour
{
    public GameObject GameOverScreen;
    private TextMeshProUGUI[] _gameOverText;
    public int TotalBlood;

    void Start()
    {
        GameOverScreen.SetActive(false);
        _gameOverText = GameOverScreen.GetComponentsInChildren<TextMeshProUGUI>();
    }

    public void RestartGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void QuitGame()
    {
        Application.Quit();
    }

    public void GameOver(string a, string b)
    {
        _gameOverText[0].text = a;
        _gameOverText[1].text = b;
        _gameOverText[2].text = "Blood Collected: " + TotalBlood;
        GameOverScreen.SetActive(true);
    }


}
