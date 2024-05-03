using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class ScoreManger : MonoBehaviour
{
    public int score = 0;
    public Text scoreText;
    public int bestScore = 0;
    public int lastScore;
    public Player player;
    public Text lastScoreText;
    public Text bestScoreText;

    void Start ()
    {
        bestScore = PlayerPrefs.GetInt("bestScore", 0);
    }

    private void Update()
    {
        scoreText.text = "Score: " + score.ToString();
        lastScoreText.text = lastScore.ToString();
        bestScoreText.text = bestScore.ToString();
        //ép kiểu dữ liệu
        if (score > bestScore)
        {
            bestScore = score;
            PlayerPrefs.SetInt("bestScore", bestScore);
        }
        if (player.isDead)
        {
            lastScore = score;
        }
    }
}
