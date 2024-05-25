using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Purchasing;
using UnityEngine.Purchasing.Extension;
using UnityEngine.UI;

public class IAPManager : MonoBehaviour
{
    [SerializeField] private string coin100_ID = "com.phong.streetracing2d.coin100";
    [SerializeField] private string coin999_ID = "com.phong.streetracing2d.coin999";
    [SerializeField] private string removeAdsID = "com.phong.streetracing2d.noads";
    public void OnPurchaseComplete(Product product)
    {
        if (product.definition.id == coin100_ID)
        {
            CoinManager.Ins.totalCoinValue += 100;
            CoinManager.Ins.SaveCoinValue();
            CoinManager.Ins.LoadCoinValue();
        }

        if (product.definition.id == coin999_ID)
        {
            CoinManager.Ins.totalCoinValue += 999;
            CoinManager.Ins.SaveCoinValue();
            CoinManager.Ins.LoadCoinValue();
        }
        if (product.definition.id == removeAdsID)
        {
            Debug.Log("Remove Ads!!!");
            PlayerPrefs.SetInt("isRemoveAds", 1);
        }
    }

    public void OnPurchaseFailed(Product product, PurchaseFailureDescription failureReason)
    {
        Debug.Log(product.definition.id + "failed because" + failureReason);
    }
}
