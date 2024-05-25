using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

public class SpawningCoins : MonoBehaviour
{
    public Transform SCtransform;
    public GameObject coin;  
    
    public float minSpawnCoinsTime;
    public float maxSpawnCoinsTime;

    public float elapsedTime;
    public float delayTime;

    void Start()
    {
        SCtransform = GetComponent<Transform>(); 
    }

    void Update()
    {
        if (GameManager.Ins.isGamePlaying && !Player.Ins.isDead)
        {
            elapsedTime += Time.deltaTime; //thời gian này là tốc độ của các frame chứ không phải giây thông thường

            if (elapsedTime >= delayTime)
            {
                elapsedTime = 0f;
                // Gọi hàm để tạo đồng xu
                Invoke("SpawnCoins", Random.Range(minSpawnCoinsTime, maxSpawnCoinsTime)); // thời gian này mới là giây trong thời gian
                //thực thị phương thức với 1 khoảng thời gian delay nhất định
            }
        }
    }

    void SpawnCoins()
    {
        Instantiate(coin, new Vector3 (Random.Range(-1.89f, 1.89f), SCtransform.position.y, SCtransform.position.z), Quaternion.Euler(0, 0, 0));
    }
}
