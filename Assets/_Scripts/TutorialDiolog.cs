using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialDiolog : MonoBehaviour
{
    public GameObject tutorialMenu;
    
    public void BackToMenu()
    { 
        tutorialMenu.SetActive(false);
    }
}
