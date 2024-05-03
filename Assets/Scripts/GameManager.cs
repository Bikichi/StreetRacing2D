using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Ins;
    public bool isGamePlaying;
    public GameObject homeGui;
    public GameObject gameGui;
    public GameObject gameOverMenu;
    public GameObject pauseMenu;

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

    void Update()
    {
        PlayGame();
    }

    void PlayGame()
    {
        if (Input.GetKey(KeyCode.Space))
        {
            isGamePlaying = true;
        }
    }
}
