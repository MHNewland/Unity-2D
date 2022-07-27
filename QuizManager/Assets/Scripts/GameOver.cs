using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class GameOver : MonoBehaviour
{

    [SerializeField]
    TextMeshProUGUI finalScoreText;
    GameManager gm;

    void Awake()
    {
        gm = FindObjectOfType<GameManager>();
    }

    
    public void ShowFinialScore()
    {
        finalScoreText.text = "Congratulations!\nYou Scored " + (int)(gm.finalScore * 100) + "%";

    }

}
