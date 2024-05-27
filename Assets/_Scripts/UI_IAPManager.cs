using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class UI_IAPManager : MonoBehaviour
{
    public Text coinText;
    public Button buyNoAdsButton;
    public void Update()
    {
        UpdateCoinValue();
        CheckBuyNoAdsButton();
    }

    public void UpdateCoinValue()
    {
        coinText.text = "Coin: " + PlayerPrefs.GetInt("totalCoinValue", 0);
    }

    public void ChangeScene(int sceneID)
    {
        SceneManager.LoadScene(sceneID);
    }
    public void CheckBuyNoAdsButton()
    {
        if (PlayerPrefs.GetInt("isRemoveAds", 0) == 0)
        {
            buyNoAdsButton.gameObject.SetActive(true);
        }
        else
        {
            buyNoAdsButton.gameObject.SetActive(false);
        }
    }
}
