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

    //public Car car; //không thể dùng Singletons ở đẩy vì có nhiều đối tượng Car khác nhau

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
        bestScore = PlayerPrefs.GetInt("bestScore", 0);
    }

    void Update()
    {
        if (Player.Ins.isDead && !GameManager.Ins.isGamePlaying) return;
        ScoreValue();
    }
   

    void ScoreValue ()
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
    }
}
