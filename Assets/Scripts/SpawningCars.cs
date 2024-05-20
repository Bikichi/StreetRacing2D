using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawningCarsManager : MonoBehaviour
{
    public Transform carsTransform;
    public GameObject[] cars;

    public float minSpawnCarsTime;
    public float maxSpawnCarsTime;

    public float elapsedTime;
    public float delayTime;

    public float spawnXSpacing = 1.0f; // Khoảng cách giữa các đối tượng theo trục x
    void Start()
    {
        carsTransform = GetComponent<Transform>();
    }
    void Update()
    {
        if (GameManager.Ins.isGamePlaying && !Player.Ins.isDead)
        {
            elapsedTime += Time.deltaTime;

            if (elapsedTime >= delayTime) //delayTime set phải > minSpawnCarsTime 1 khoảng nhất định không sẽ xảy ra tình trạng 2 xe sinh ra gần như cùng lúc
                                          //và sẽ đè lên nhau
            {
                elapsedTime = 0f;
                Invoke("SpawningCars", GetSpawnCarsTime());
            }
        }
    }

    float GetSpawnCarsTime()
    {
        float spawnCarsTime = Random.Range(minSpawnCarsTime, maxSpawnCarsTime);
        return spawnCarsTime;
    }

    float GetSpawnXPosition()
    {
        float spawnXPosition = Random.Range(-1.89f, 1.89f);   
        return spawnXPosition;
    }

    void SpawningCars()
    {
        Instantiate(cars[Random.Range(0, cars.Length)], new Vector3(GetSpawnXPosition(), transform.position.y, transform.position.z), Quaternion.Euler(0, 0, 90));
    }

    float CalculateNextSpawnX()
    {
        float lastSpawnXPosition;
        float SpawnX = GetSpawnXPosition() + spawnXSpacing; // Tính toán vị trí x mới
        lastSpawnXPosition = Mathf.Clamp(SpawnX, -1.89f, 1.89f);
        // Cập nhật lại giá trị của lastSpawnXPosition
        // Giới hạn vị trí x mới trong khoảng (-1.89f, 1.89f)
        return GetSpawnXPosition();
    }

    void SpawnCarsDontOverlapEachOther ()
    {
        float newSpawnX = CalculateNextSpawnX(); // Tính toán vị trí x mới
        Instantiate(cars[Random.Range(0, cars.Length)], new Vector3(newSpawnX, transform.position.y, transform.position.z), Quaternion.Euler(0, 0, 90));
    }
}
