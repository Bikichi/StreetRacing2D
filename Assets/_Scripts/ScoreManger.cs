using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class ScoreManager : MonoBehaviour
{
    public static ScoreManager Ins; //Singletons

    public int score = 0;

    public int bestScore = 0;
    public int lastScore;

    public Text scoreText;
    public Text lastScoreText;
    public Text bestScoreText;

    private void Awake()
    {
        if (Ins != null && Ins != this)
        {
            Destroy(Ins);
        }
        else
        {
            Ins = this;
        }
    }

    void Start ()
    {
        LoadBestScoreValue();
    }

    void Update()
    {
        UpdateScoreValue();
    }

    public void LoadBestScoreValue ()
    {
        bestScore = PlayerPrefs.GetInt("bestScore", 0);
    }

    public void SaveBestScoreValue ()
    {
        //ép kiểu dữ liệu
        if (score > bestScore)
        {
            bestScore = score;
            PlayerPrefs.SetInt("bestScore", bestScore);
        }
    }
    void UpdateScoreValue ()
    {
        lastScoreText.text = lastScore.ToString();
        bestScoreText.text = bestScore.ToString();
        scoreText.text = "Score: " + score.ToString();
    }
}
