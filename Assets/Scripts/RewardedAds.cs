using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Advertisements;
using UnityEngine.UI;

public class RewardedAds : MonoBehaviour ,IUnityAdsLoadListener ,IUnityAdsShowListener
{
    [SerializeField] private string androidAdUnitId;
    [SerializeField] private string iosAdUnitId;
    [SerializeField] private GameObject rebornButton;
    [SerializeField] private RebornPlayer rebornPlayer;

    private string adUnitId;

    private void Awake()
    {
        #if UNITY_IOS
                        adUnitId = iosAdUnitId;
        #elif UNITY_ANDROID
                adUnitId = androidAdUnitId;
        #endif
    }


    public void LoadRewardedAd()
    {
        Advertisement.Load(adUnitId, this);
    }

    public void ShowRewardedAd()
    {
        Advertisement.Show(adUnitId, this);
        LoadRewardedAd();
        rebornButton.SetActive(false);
    }




    #region LoadCallbacks
    public void OnUnityAdsAdLoaded(string placementId)
    {
        Debug.Log("Interstitial Ad Loaded");
    }

    public void OnUnityAdsFailedToLoad(string placementId, UnityAdsLoadError error, string message) { }
    #endregion

    #region ShowCallbacks
    public void OnUnityAdsShowFailure(string placementId, UnityAdsShowError error, string message) { }

    public void OnUnityAdsShowStart(string placementId) { }

    public void OnUnityAdsShowClick(string placementId) { }

    public void OnUnityAdsShowComplete(string placementId, UnityAdsShowCompletionState showCompletionState)
    {
        if (placementId == adUnitId && showCompletionState.Equals(UnityAdsShowCompletionState.COMPLETED))
        {
            Debug.Log("Ads Fully Watched .....");
            GameManager.Ins.gameoverDiolog.SetActive(false);
            rebornPlayer.Reborn();
            GameManager.Ins.isGamePlaying = true;
        }
    }
    #endregion


}
