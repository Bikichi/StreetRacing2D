using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Purchasing;
using UnityEngine.Purchasing.Extension;

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
        }

        if (product.definition.id == coin999_ID)
        {
            CoinManager.Ins.totalCoinValue += 999;
            CoinManager.Ins.SaveCoinValue();
        }
        if (product.definition.id == removeAdsID)
        {
            Debug.Log("Remove Ads!!!");
        }
    }

    public void OnPurchaseFailed(Product product, PurchaseFailureDescription failureReason)
    {
        Debug.Log(product.definition.id + "failed because" + failureReason);
    }
}
