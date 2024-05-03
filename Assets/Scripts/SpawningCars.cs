using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawningCars : MonoBehaviour
{
    public Transform carsTransform;
    public GameObject[] cars;

    public float minSpawnCarsTime;
    public float maxSpawnCarsTime;

    public float elapsedTime;
    public float delayTime;
    void Start()
    {
        carsTransform = GetComponent<Transform>();
        //StartCoroutine(SpawningCoins());
        // sử dụng hàm StartCoroutine() để khởi chạy các hàm bất đồng bộ
        //hàm này cũng chỉ khởi tạo 1 lần duy nhất khi chương trình bắt đầu chạy
    }
    void Update()
    {
        if (GameManager.Ins.isGamePlaying)
        {
            elapsedTime += Time.deltaTime;

            if (elapsedTime >= delayTime)
            {
                elapsedTime = 0f;
                Invoke("CarsSpawn", Random.Range(minSpawnCarsTime, maxSpawnCarsTime));
            }
        }
    }

    void CarsSpawn()
    {
        Instantiate(cars[Random.Range(0, cars.Length)], new Vector3(Random.Range(-1.89f, 1.89f), transform.position.y, transform.position.z), Quaternion.Euler(0, 0, 90));
    }

    //IEnumerator SpawnCars()
    //IEnumerator là một giao diện(interface) trong ngôn ngữ lập trình C#
    //IEnumerator thường được sử dụng để triển khai các coroutine
    //Coroutine đại khái là các hàm chạy bất đồng bộ mà có thể tạm dừng và tiếp tục sau một khoảng thời gian hoặc khi một điều kiện nào đó được đáp ứng.
    //{
    //    //float spawmTime = Random.Range(0.5f, 2f);
    //    //yield return new WaitForSeconds(spawmTime);
    //    //    //Bằng cách sử dụng các lệnh yield kết hợp với WaitForSeconds(),
    //    //    //có thể tạm dừng thực thi của coroutine và sau đó tiếp tục nó sau một khoảng thời gian nhất định hoặc khi một đSiều kiện nào đó được đáp ứng.
    //    //CarsSpawn();
    //        //spawningStarted = false;
    //}
}
