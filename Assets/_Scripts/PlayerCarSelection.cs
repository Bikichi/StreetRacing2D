using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PlayerCarSelection: MonoBehaviour
{
    public PlayerDatabase playerDatabase;

    public SpriteRenderer spriteRenderer;

    public PlayerCar[] playerCars;

    public Button unlockButton;

    public Button resetButton;   

    public Text coinText;

    private int selectedOption = 0; //biến để theo dõi PlayerCar nào đã được chọn

    private void Awake()
    {
        foreach (PlayerCar car in playerCars)
        {
            if(car.price == 0)
                car.isUnlocked = true;
            else
            {
                if (PlayerPrefs.GetInt(car.name, 0) == 0) // = 0 là chiếc xe chưa được mở khoá
                {
                    car.isUnlocked = false;
                }
                else
                {
                    car.isUnlocked = true;
                }
            }
        }
        UpdateUI();
    }
    void Start()
    {
        if (!PlayerPrefs.HasKey("selectedOption"))
        {
            selectedOption = 0;
        } 
        else 
        {
            LoadOptions();
        }
        
        UpdatePLayerCar(selectedOption);
    }

    void Update()
    {
        CheckForEnoughCoins();
    }

    public void NextOption()
    {
        selectedOption++;

        if (selectedOption >= playerDatabase.PlayerCarCount)
        {
            selectedOption = 0;
        }

        UpdatePLayerCar(selectedOption);
        if (playerCars[selectedOption].isUnlocked)
        {
            SaveOptions();
        }
        UpdateUI();
    }

    public void BackOption()
    {
        selectedOption--;

        if (selectedOption <0)
        {
            selectedOption = playerDatabase.PlayerCarCount - 1;
        }

        UpdatePLayerCar(selectedOption);
        if (playerCars[selectedOption].isUnlocked)
        {
            SaveOptions();
        }
        UpdateUI();
    }


    private void UpdatePLayerCar(int selectedOption)
    {
        PlayerCar playerCar = playerDatabase.GetPlayerCar(selectedOption);
        spriteRenderer.sprite = playerCar.playerCarSprite;
    }

    private void LoadOptions()
    {
        selectedOption = PlayerPrefs.GetInt("selectedOption");
    }

    private void SaveOptions()
    {
        PlayerPrefs.SetInt("selectedOption", selectedOption);
    }

    public void ChangeScene(int sceneID)
    {
        SceneManager.LoadScene(sceneID);
    }

    public void UpdateUI()
    {
        coinText.text = "Coin: " + PlayerPrefs.GetInt("totalCoinValue", 0);
        if (playerCars[selectedOption].isUnlocked == true)
        {
            unlockButton.gameObject.SetActive(false);
        }
        else
        {
            unlockButton.GetComponentInChildren<Text>().text = "Price: " + playerCars[selectedOption].price;
            if (PlayerPrefs.GetInt("totalCoinValue", 0) < playerCars[selectedOption].price)
            {
                unlockButton.gameObject.SetActive(true);
                unlockButton.interactable = false; //không ấn được vào nút đấy nữa
            }
            else
            {
                unlockButton.gameObject.SetActive(true);
                unlockButton.interactable = true;
            }
        }
    }

    public void Unlock()
    {
        int coins = PlayerPrefs.GetInt("totalCoinValue", 0);
        int price = playerCars[selectedOption].price;
        PlayerPrefs.SetInt("totalCoinValue", coins - price);
        CoinManager.Ins.LoadCoinValue();
        PlayerPrefs.SetInt(playerCars[selectedOption].name, 1); // = 1 tức là đã mở khoá chiếc xe
        SaveOptions();
        playerCars[selectedOption].isUnlocked = true;
        UpdateUI();
    }
    public void ClearPlayerPrefs()
    {
        PlayerPrefs.DeleteAll();
        CoinManager.Ins.LoadCoinValue();
        ChangeScene(0);
        UpdateUI();
    }

    public void CheckForEnoughCoins()
    {
        if (unlockButton.gameObject.activeInHierarchy)
        {
            unlockButton.interactable = (PlayerPrefs.GetInt("totalCoinValue", 0) >= playerCars[selectedOption].price);
        }
    }
}
