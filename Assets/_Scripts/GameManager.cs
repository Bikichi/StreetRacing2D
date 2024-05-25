using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public static GameManager Ins;//Singletons

    public bool isGamePlaying;
    public GameObject homeGui;
    public GameObject gameGui;
    public GameObject pauseDialog;
    public GameObject gameoverDiolog; 
    public GameObject tutorialDiolog;
    public Text timeCountingText;
    public Text totalCoinValueInGameText;
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

    private void Update()
    {
        UpdateCoinValueInGame();
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

    public void ShowTutorialPanel()
    {
        tutorialDiolog.SetActive(true);
    }

    public void PauseGame ()
    {
        Time.timeScale = 0f;
        if (pauseDialog)
        {
            pauseDialog.SetActive(true);
        }
    }

    public void ChangeScene(int sceneID)
    {
        SceneManager.LoadScene(sceneID);
    }

    public void GameOver()
    {
        isGamePlaying = false;
        ScoreManager.Ins.lastScore = ScoreManager.Ins.score;
        CoinManager.Ins.SaveCoinValue();
        ScoreManager.Ins.SaveBestScoreValue();
        gameoverDiolog.SetActive(true);
        AudioController.Ins.StopPlayMusic();
        AudioController.Ins.PlaySound(AudioController.Ins.explosion);
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
            AudioController.Ins.PlaySound(AudioController.Ins.timeBeep);
        }

        isGamePlaying = true;
        ShowGameGUI(true);
        AudioController.Ins.PlayBackgroundMusic();
    }

    void UpdateCoinValueInGame()
    {
        totalCoinValueInGameText.text = "Coin: " + CoinManager.Ins.totalCoinValue.ToString(); //cú pháp này thay cho việc sửa tay Component Text trong Game Object  
    }
}
