using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu]
public class PlayerDatabase : ScriptableObject //không k? th?a MonoBehaviour vì ta s? không g?n scripts này lên Object nào
{
    public PlayerCar[] playerCar;

    public int PlayerCarCount
    {
        get
        {
            return playerCar.Length; //ph??ng th?c kh?i t?o này s? ch? v? s? ph?n t? trong m?ng playerCar
        }
    }

    public PlayerCar GetPlayerCar (int index)
    {
        return playerCar[index];
    }
}
