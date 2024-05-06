using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseDiolog : MonoBehaviour
{
    public GameObject pauseMenu;
    public void UnpauseGame()
    {
        Time.timeScale = 1.0f;
        pauseMenu.SetActive(false);
    }

    // Update is called once per frame
    public void ExitGame()
    {
        Application.Quit();
    }
}
