using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    [SerializeField]
    GameObject gameplayPanel;

    [SerializeField]
    GameObject gameOverPanel;


    Quiz quiz;
    public float finalScore { get; private set; }

    void Start()
    {
        gameplayPanel.SetActive(true);
        gameOverPanel.SetActive(false);
    }

    public void EndGame(float score)
    {
        finalScore = score;
        gameOverPanel.SetActive(true);
        gameplayPanel.SetActive(false);
        gameOverPanel.GetComponent<GameOver>().ShowFinialScore();

    }

    public void StartGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

}
