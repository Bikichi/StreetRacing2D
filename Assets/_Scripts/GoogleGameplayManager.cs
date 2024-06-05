using GooglePlayGames.BasicApi;
using GooglePlayGames;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoogleGameplayManager : MonoBehaviour
{
    public static GoogleGameplayManager Ins;
    public bool connectedToGooglePlay;

    void Awake()
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

        PlayGamesPlatform.DebugLogEnabled = true;
        PlayGamesPlatform.Activate();
    }
    void Start()
    {
        LoginGooglePlay();
    }
    void LoginGooglePlay()
    {
        PlayGamesPlatform.Instance.Authenticate(ProcessAuthentication);
    }
    void ProcessAuthentication(SignInStatus status)
    {
        if (status == SignInStatus.Success)
        {
            connectedToGooglePlay = true;
        }
        else connectedToGooglePlay = false;
    }
}