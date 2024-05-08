using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public static GameManager Ins;//Singletons

    public bool isGamePlaying;
    public GameObject homeGui;
    public GameObject gameGui;
    public GameObject pauseDialog;
    public GameObject gameoverDiolog;
    public Text timeCountingText;

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

    public void ShowGameGUI(bool isShow)
    {
        if (gameGui)
        {
            gameGui.SetActive(isShow);
        }

        if (homeGui)    
        {
            homeGui.SetActive(!isShow);
        }
    }

    public void UpdateTimeCountingText(float time) 
    {
        if (timeCountingText) 
        {
            timeCountingText.gameObject.SetActive(true);
            timeCountingText.text = time.ToString();
            if (time <= 0)
            {
                timeCountingText.gameObject.SetActive(false);
            }
        }
    }

    public void PlayGame()
    {
        homeGui.SetActive(false);
        StartCoroutine(CountingDown());
    }


    void Start()
    {
        ShowGameGUI(false);
    }

    public void PauseGame ()
    {
        Time.timeScale = 0f;
        if (pauseDialog)
        {
            pauseDialog.SetActive(true);
        }
    }
    
    public void GameOver()
    {
        isGamePlaying = false;
        ScoreManager.Ins.lastScore = ScoreManager.Ins.score;
        gameoverDiolog.SetActive(true);
    }

    IEnumerator CountingDown()
    {
        float time = 3f;

        UpdateTimeCountingText(time);

        while (time > 0f)
        {
            yield return new WaitForSeconds(1f);
            time--;
            UpdateTimeCountingText(time);
        }

        isGamePlaying = true;
        ShowGameGUI(true);
    }
}
