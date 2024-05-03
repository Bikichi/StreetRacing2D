using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public SpawningCars spawningCars;
    public SpawningCoins spawningCoins;
    public RoadScolling roadScolling;

    void Update()
    {
        if (Input.GetKey(KeyCode.Space))
        {
            roadScolling.isStart = true;
            spawningCars.isGamePlaying = true;
            spawningCoins.isGamePlaying = true;
        }
    }
}
