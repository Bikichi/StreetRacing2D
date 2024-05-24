using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public Text coinText;
    public Button playButton;

    public void Update()
    {
        UpdateCoinValue();
    }

    public void UpdateCoinValue()
    {
        coinText.text = "Coin: " + PlayerPrefs.GetInt("totalCoinValue", 0);
    }

    public void ChangeScene(int sceneID)
    {
        SceneManager.LoadScene(sceneID);
    }
}
