using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SocialPlatforms.Impl;

public class Car : MonoBehaviour
{
    public Transform carTransform;
    public float speed;
    //dễ dùng nhưng chưa tối ưu vì tạo cả 1 đối tượng nhưng chỉ dùng để truy cập tới 1 thuộc tính cần
    //chuẩn ra cần dùng delegate, event
    public bool hasPassed = false; // Biến đánh dấu xem đã tăng giá trị carsPassedTotal hay chưa
    //public Car car; //không thể dùng Singletons ở đẩy vì có nhiều đối tượng Car khác nhau
    public PlayerMovement playerMovement;

    void Start()
    {
        carTransform = GetComponent<Transform>();
        playerMovement = GameObject.FindAnyObjectByType<PlayerMovement>();
    }

    void Update()
    {
        UpSpeedCarsAccordingToScore();
        carTransform.position -= new Vector3(0, speed * Time.deltaTime, 0);
        //Để -= vì nó di chuyển đi xuống, theo hệ trục tọa độ trục y thì hướng xuống dưới là chiều âm
        //Không thể code di chuyển kiểu Background vì Position đã set 90 để quay dọc
        if (carTransform.position.y <= -6 || Player.Ins.isDead)
        {
            Destroy(gameObject); // vì đến vị trí này đối tượng bị hủy nên scripts cũng bị hủy nên để lưu biến đếm tổng số xe đã vượt qua thì phải tạo biến này ở 1 class khác
        }

        if (Player.Ins.isDead)
        {
            return;
        }
      
        if (!hasPassed && carTransform.position.y <= playerMovement.PMtransform.position.y) // Nếu chưa tăng giá trị và vị trí của đối tượng xuống dưới đối tượng Player
        {
            ScoreManager.Ins.score += 1;
            hasPassed = true; // Đánh dấu đã tăng giá trị
        }
    }

    void UpSpeedCarsAccordingToScore() //tăng tốc độ theo điểm số
    {
        if (ScoreManager.Ins.score >= 0 && ScoreManager.Ins.score < 30)
        {
            speed = 6.0f;
        }
        else if (ScoreManager.Ins.score >= 30 && ScoreManager.Ins.score < 60)
        {
            speed = 7.0f;
        }
        else if (ScoreManager.Ins.score >= 60 && ScoreManager.Ins.score < 100)
        {
            speed = 8.5f;
        }
        else if (ScoreManager.Ins.score >= 100 && ScoreManager.Ins.score < 150)
        {
            speed = 10.0f;
        }
        else
        {
            speed = 10.5f;
        }
    } 
}