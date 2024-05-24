    using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawningGameObject : MonoBehaviour
{
    public GameObject[] car;
    public GameObject coin;
    public bool isGamePlaying;
    public bool isGameOver;
    public Transform spawnTransform;
    
    public float minSpawnCarsTime; //sau bao lâu thì lại sinh xe (mức nhỏ nhất)
    public float maxSpawnCarsTime; //sau bao lâu thì lại sinh xe (lớn nhất)

    public float minSpawnCoinsTime;
    public float maxSpawnCoinsTime;

    public float delayRepeatSpawningCarsTime; //sau bao lâu thì thực thi lại hàm sinh xe
    public float delayRepeatSpawningCoinsTime; //sau bao lâu thì thực thi lại hàm đồng xu

    void Start()    
    {
        spawnTransform = GetComponent<Transform>();
        //Hàm Start() chỉ được gọi 1 lần duy nhất khi chương trình bắt đầu khởi chạy, Start() chạy trước cả Update(), FixedUpdate(), LastUpdate(),...
        //nên nhiều giá trị khởi tạo lại liên tục ở Update() thì trong Start() không cập nhật được
        //tuy nhiên hàm thực thi bên trong Start hoàn toàn có thể thực hiện logic lặp lại
        InvokeRepeating("RepeatSpawningCars", 0f, delayRepeatSpawningCarsTime);
        //thực thi lại hàm nào, sau bao lâu thì bắt đầu, khoảng thời gian giữa các lần thực thi lại
        //minSpawnCarsTime - delayRepeatSpawningCarsTime = khoảng thời gian giữa 2 xe được sinh ra ngắn nhất (thời gian ngắn nhất để sinh ra 1 xe)
        //tương tự và ngược lại
        //có thể lấy 1 giá trị cụ thể để kiểm tra
        InvokeRepeating("RepeatSpawningCoins", 0f, delayRepeatSpawningCoinsTime);
    }
    private void Update()
        //Hàm Update được gọi mỗi frame
    {
        if (Input.GetKey(KeyCode.Space))
        {
            isGamePlaying = true;   
        }
        else if (Input.GetKey(KeyCode.X))
        {
            isGamePlaying = false;
        }
    }
    public void RepeatSpawningCars()
    {
        Invoke("SpawningCars", Random.Range(minSpawnCarsTime, maxSpawnCarsTime));
    }
    public void RepeatSpawningCoins()
    {
        Invoke("SpawningCoins", Random.Range(minSpawnCoinsTime, maxSpawnCoinsTime));
    }

    public void SpawningCars() //chiếc xe nào được sinh ra, sinh ra ở vị trí nào
    {
        if (isGamePlaying)
        {
            int randomCarsSpawn = Random.Range(0, car.Length); // lấy ngẫu nhiên 1 chiếc xe trong mảng để sinh
            float ranXPosition = Random.Range(-1.89f, 1.89f); // sinh xe với tọa độ x ngẫu nhiên
            Instantiate(car[randomCarsSpawn], new Vector3(ranXPosition, spawnTransform.position.y, spawnTransform.position.z), Quaternion.Euler(0, 0, 90)); //tạo ra bản sao của một đối tượng
            //truyền vào đối tượng muốn tạo bản sao, vị trí của đối tượng khi sinh ra, hướng của đối tượng khi sinh ra
        }
    }

    public void SpawningCoins()
    {
        if (isGamePlaying) 
        {
            float ranXPosition = Random.Range(-1.89f, 1.89f);
            Instantiate(coin, new Vector3(ranXPosition, spawnTransform.position.y, spawnTransform.position.z), Quaternion.identity);
        }
    }
}