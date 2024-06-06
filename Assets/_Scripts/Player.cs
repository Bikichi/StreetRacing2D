using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;


public class Player : MonoBehaviour
{
    public static Player Ins; //khởi tạo Design Pattern - Singletons, thể hiện tính toàn cục và duy nhất
    //các Class khác có thể truy cập tới thuộc tính của class này
    //game chỉ có 1 player nên mới có thể dùng Singletons
    public GameObject bigBang;

    public PlayerDatabase playerDatabase;

    public SpriteRenderer spriteRenderer;

    public bool isDead;

    private int selectedOption = 0;

    private void Awake()
    {
        if (Ins != null && Ins != this) //nếu như đã có thằng khác khởi tạo Singletons này
        {
            Destroy(Ins);
        }
        else
        {
            Ins = this;
        }
    }

    void Start()
    {
        if (!PlayerPrefs.HasKey("selectedOption"))
        {
            selectedOption = 0;
        }
        else
        {
            LoadOption();
        }

        UpdatePLayerCar(selectedOption);
    }

    private void OnTriggerEnter2D(Collider2D col) //Đây là khai báo của phương thức
                                                  //Nó sẽ được gọi mỗi khi một Collider2D khác va chạm với Collider2D của đối tượng được gắn scripts này
                                                  //Tham số "col" là Collider2D của đối tượng khác mà va chạm với Collider2D của đối tượng được gắn scripts này.
    {
        if (col.CompareTag(Const.CARS_TAG)) //nếu đối tượng này va chạm với đối tượng có tag là CARS_TAG thì thực thi
        {
            gameObject.SetActive(false); //tạm tắt đối tượng và phương thức gắn vào
            Destroy(col.gameObject); //hủy đối tượng va chạm phải

            isDead = true;
            if (bigBang) //nếu đối tượng khác null
            {
                var newBigBang = Instantiate(bigBang, transform.position, Quaternion.identity); //đối tượng được tạo ra từ Instantiate là 1 bản sao của bigBang
                //Instantiate() là một phương thức được sử dụng để tạo ra một BẢN SAO mới của một prefab hoặc một đối tượng có sẵn trong trò chơi
                //truyền vào GameOject, vị trí, góc quay 
                //Một Quaternion là một cách biểu diễn các phép quay trong không gian ba chiều. 
                //khi bạn sử dụng Quaternion.identity, bạn đang chỉ định rằng không có phép quay nào được áp dụng, nghĩa là đối tượng sẽ không bị xoay khi được tạo ra hoặc di chuyển 
                Destroy(newBigBang, 0.35f);
                //phải set 1 khoảng thời gian chờ để chạy Animation trước khi Destroy GameObject
            }
            GameManager.Ins.GameOver();
        }

        if (col.CompareTag(Const.COIN_TAG))
        {
            Destroy(col.gameObject);
            ScoreManager.Ins.score += 10;
            CoinManager.Ins.totalCoinValue += 1;
            AudioController.Ins.PlaySound(AudioController.Ins.collect);
        }
    }

    private void UpdatePLayerCar(int selectedOption)
    {
        PlayerCar playerCar = playerDatabase.GetPlayerCar(selectedOption);
        spriteRenderer.sprite = playerCar.playerCarSprite;
    }

    private void LoadOption()
    {
        selectedOption = PlayerPrefs.GetInt("selectedOption");
    }
}
