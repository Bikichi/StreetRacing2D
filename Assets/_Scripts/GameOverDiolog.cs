using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;


public class GameOverDiolog : MonoBehaviour
{
    public Text totalCoinValueInGameoverDiologText;
    public void ReplayGame()
    {   
        AdsManager.Instance.ReloadRewardedAd();
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void ExitGame()
    {
        Application.Quit();
    }

    private void Update()
    {
        UpdateCoinValueInGameoverDiolog();
    }

    void UpdateCoinValueInGameoverDiolog()
    {
        totalCoinValueInGameoverDiologText.text = CoinManager.Ins.totalCoinValue.ToString();
    }
}
