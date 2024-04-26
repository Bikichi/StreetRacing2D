using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnCar : MonoBehaviour
{
    public GameObject[] car; //mảng các gameobject
    public float speedCars = 5f;
    void Start()
    {
        StartCoroutine(SpawnCars());
    }

    public void CarsSpawn()
    {
        int randomCarsSpawn = Random.Range(0, car.Length);
        float ranXPosition = Random.Range(-1.89f, 1.89f);
        Instantiate(car[randomCarsSpawn], new Vector3(ranXPosition, transform.position.y, transform.position.z), Quaternion.Euler(0, 0, 90));
        //??? tại sao Prefab set hướng rồi mà lúc sinh vẫn phải set lại hướng nhỉ?
    }

    void Update()
    {
        transform.position = new Vector3(0, 10, 0);

        if (transform.position.y <= -8)
        {
            Destroy(gameObject);
        }
    }

    IEnumerator SpawnCars()
    {
        while (true)
        {
            yield return new WaitForSeconds(1f);
            CarsSpawn();
        }
    } 
}
