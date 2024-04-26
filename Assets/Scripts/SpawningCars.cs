using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawningCars : MonoBehaviour
{
    public GameObject[] car; //mảng các gameobject
    void Start()
    {
        StartCoroutine(SpawnCars());//???
    }

    public void CarsSpawn()
    {
        int randomCarsSpawn = Random.Range(0, car.Length);
        float ranXPosition = Random.Range(-1.89f, 1.89f);
        var carsSpawn =  Instantiate(car[randomCarsSpawn], new Vector3(ranXPosition, transform.position.y, transform.position.z), Quaternion.Euler(0, 0, 90));
        //position không set trong code thì lúc chạy sẽ lấy position của gameobject lúc kéo thả trong Unity
        //??? tại sao Prefab set hướng rồi mà lúc sinh bản sao vẫn phải set lại hướng nhỉ?
    }

    IEnumerator SpawnCars() //???
    {
        while (true)
        {
            yield return new WaitForSeconds(1f);//???
            CarsSpawn();
        }
    } 
}
