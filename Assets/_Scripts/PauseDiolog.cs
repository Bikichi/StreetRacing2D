using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseDiolog : MonoBehaviour
{
    public GameObject pauseMenu;
    public void UnpauseGame()
    {
        Time.timeScale = 1.0f;
        pauseMenu.SetActive(false);
    }

    public void ReplayGame()
    {
        Time.timeScale = 1.0f;
        AdsManager.Instance.ReloadRewardedAd();
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        CoinManager.Ins.LoadCoinValue();
    }

    public void ExitGame()
    {
        Application.Quit();
    }
}
