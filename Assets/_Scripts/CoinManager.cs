using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SocialPlatforms.Impl;
using UnityEngine.UI;

public class CoinManager : MonoBehaviour
{
    public static CoinManager Ins;

    public int totalCoinValue = 0;

    private void Awake()
    {
        if (Ins != null && Ins != this)
        {
            Destroy(gameObject);
        }
        else
        {
            Ins = this;
            DontDestroyOnLoad(gameObject);
        }
    }

    public void Start()
    {
        LoadCoinValue();
    }

    public void LoadCoinValue()
    {
        totalCoinValue = PlayerPrefs.GetInt("totalCoinValue", 0);
    }

    public void SaveCoinValue()
    {
        PlayerPrefs.SetInt("totalCoinValue", totalCoinValue);
    }
}
