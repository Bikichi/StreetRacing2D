using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu]
public class PlayerDatabase : ScriptableObject //kh�ng k? th?a MonoBehaviour v� ta s? kh�ng g?n scripts n�y l�n Object n�o
{
    public PlayerCar[] playerCar;

    public int PlayerCarCount
    {
        get
        {
            return playerCar.Length; //ph??ng th?c kh?i t?o n�y s? ch? v? s? ph?n t? trong m?ng playerCar
        }
    }

    public PlayerCar GetPlayerCar (int index)
    {
        return playerCar[index];
    }
}
