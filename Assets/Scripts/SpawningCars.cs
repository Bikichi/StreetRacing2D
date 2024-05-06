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
                Invoke("CarsSpawn", Random.Range(minSpawnCarsTime, maxSpawnCarsTime));
            }
        }
    }

    void CarsSpawn()
    {
        Instantiate(cars[Random.Range(0, cars.Length)], new Vector3(Random.Range(-1.89f, 1.89f), transform.position.y, transform.position.z), Quaternion.Euler(0, 0, 90));
    }
}
