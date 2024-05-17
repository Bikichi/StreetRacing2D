using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PlayerCarManager : MonoBehaviour
{
    public PlayerDatabase playerDatabase;

    public SpriteRenderer spriteRenderer;

    private int selectedOption = 0; //biến để theo dõi PlayerCar nào đã được chọn
    void Start()
    {
        if (!PlayerPrefs.HasKey("selectedOption"))
        {
            selectedOption = 0;
        } 
        else 
        {
            Load();
        }
        
        UpdatePLayerCar(selectedOption);
    }

    public void NextOption()
    {
        selectedOption++;

        if (selectedOption >= playerDatabase.PlayerCarCount)
        {
            selectedOption = 0;
        }

        UpdatePLayerCar(selectedOption);
        Save();
    }

    public void BackOption()
    {
        selectedOption--;

        if (selectedOption <0)
        {
            selectedOption = playerDatabase.PlayerCarCount - 1;
        }

        UpdatePLayerCar(selectedOption);
        Save();
    }


    private void UpdatePLayerCar(int selectedOption)
    {
        PlayerCar playerCar = playerDatabase.GetPlayerCar(selectedOption);
        spriteRenderer.sprite = playerCar.playerCarSprite;
    }

    private void Load()
    {
        selectedOption = PlayerPrefs.GetInt("selectedOption");
    }

    private void Save()
    {
        PlayerPrefs.SetInt("selectedOption", selectedOption);
    }

    public void ChangeScene(int sceneID)
    {
        SceneManager.LoadScene(sceneID);
    }
}
