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
    void Start()
    {
        carsTransform = GetComponent<Transform>();
    }
    void Update()
    {
        if (GameManager.Ins.isGamePlaying && !Player.Ins.isDead)
        {
            UpDelaytimeAccordingToScore();
            elapsedTime += Time.deltaTime;

            if (elapsedTime >= delayTime) //delayTime set phải > minSpawnCarsTime 1 khoảng nhất định không sẽ xảy ra tình trạng 2 xe sinh ra gần như cùng lúc
                                          //và sẽ đè lên nhau
            {
                elapsedTime = 0f;
                Invoke("SpawningCars", GetSpawnCarsTime());
            }
        }
    }

    public float GetSpawnCarsTime()
    {
        float spawnCarsTime = Random.Range(minSpawnCarsTime, maxSpawnCarsTime);
        return spawnCarsTime;
    }

    public float GetSpawnXPosition()
    {
        float spawnXPosition = Random.Range(-1.89f, 1.89f);   
        return spawnXPosition;
    }

    public void SpawningCars()
    {
        Instantiate(cars[Random.Range(0, cars.Length)], new Vector3(GetSpawnXPosition(), transform.position.y, transform.position.z), Quaternion.Euler(0, 0, 90));
    }

    void UpDelaytimeAccordingToScore() //tăng tốc độ theo điểm số
    {
        if (ScoreManager.Ins.score >= 0 && ScoreManager.Ins.score < 30)
        {
            delayTime = 1.0f;
        }
        else if (ScoreManager.Ins.score >= 30 && ScoreManager.Ins.score < 60)
        {
            delayTime = 0.8f;
        }
        else if (ScoreManager.Ins.score >= 60 && ScoreManager.Ins.score < 100)
        {
            delayTime = 0.6f;
        }
        else if (ScoreManager.Ins.score >= 100 && ScoreManager.Ins.score < 150)
        {
            delayTime = 0.5f;
        }
        else
        {
            delayTime = 0.45f;
        }
    }
}
