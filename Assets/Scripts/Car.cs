using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SocialPlatforms.Impl;

public class Car : MonoBehaviour
{
    public Transform transform;
    public float speed;
    //dễ dùng nhưng chưa tối ưu vì tạo cả 1 đối tượng nhưng chỉ dùng để truy cập tới 1 thuộc tính cần
    //chuẩn ra cần dùng delegate, event
    public bool hasPassed = false; // Biến đánh dấu xem đã tăng giá trị carsPassedTotal hay chưa
    public SpawningCars spawningCars;
    [SerializeField] 

    void Start()
    {
        transform = GetComponent<Transform>();
        //scoreManger = GameObject.FindObjectOfType<ScoreManger>(); //Tìm đối tượng ScoreManger trong Scene để tham chiếu
        ////Phương thức GetComponent<>() được sử dụng để lấy thành phần (component) của một đối tượng đang được gắn script hiện tại.
        ////Trong trường hợp của bạn, nếu hai scripts Car và ScoreManger không được gắn vào cùng một đối tượng, việc sử dụng GetComponent<ScoreManger>() sẽ không hoạt động.
        ////Để lấy thành phần từ một đối tượng khác trong Scene, bạn cần sử dụng các phương thức khác như FindObjectOfType<>() hoặc lưu trữ đối tượng đó từ một nguồn khác như đã đề cập trong cách trước đó.
        ////GetComponent<ScoreManger>() trong trường hợp này sẽ không hoạt động vì nó không thể tìm thấy thành phần ScoreManger trên đối tượng chứa script Car.
        spawningCars = GameObject.FindObjectOfType<SpawningCars>();
    }

    void Update()
    {
        UpSpeedCarsAccordingToScore();
        transform.position -= new Vector3(0, speed * Time.deltaTime, 0);
        //Để -= vì nó di chuyển đi xuống, theo hệ trục tọa độ trục y thì hướng xuống dưới là chiều âm
        //Không thể code di chuyển kiểu Background vì Position đã set 90 để quay dọc
        if (transform.position.y <= -6 || Player.Ins.isDead)
        {
            Destroy(gameObject); // vì đến vị trí này đối tượng bị hủy nên scripts cũng bị hủy nên để lưu biến đếm tổng số xe đã vượt qua thì phải tạo biến này ở 1 class khác
        }

        if (Player.Ins.isDead)
        {
            return;
        }
      
        if (!hasPassed && transform.position.y <= Player.Ins.transform.position.y) // Nếu chưa tăng giá trị và vị trí của đối tượng xuống dưới đối tượng Player
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
            Player.Ins.speed = 5.0f;
            Player.Ins.rotationSpeed = 5.0f;
            spawningCars.delayTime = 0.95f;
        }
        else if (ScoreManager.Ins.score >= 30 && ScoreManager.Ins.score < 60)
        {
            speed = 7.0f;
            spawningCars.delayTime = 0.8f;
        }
        else if (ScoreManager.Ins.score >= 60 && ScoreManager.Ins.score < 100)
        {
            speed = 8.5f;
            Player.Ins.speed = 6.0f;
            Player.Ins.rotationSpeed = 6.0f;
            spawningCars.delayTime = 0.7f;
        }
        else
        {
            speed = 10.0f;
            Player.Ins.speed = 7.0f;
            Player.Ins.rotationSpeed = 7.0f;
            spawningCars.delayTime = 0.6f;
        }
    } 
}