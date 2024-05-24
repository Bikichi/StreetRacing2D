using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu]
public class PlayerDatabase : ScriptableObject //không kế thừa MonoBehaviour vì ta sẽ không gắn scripts này lên GameObject nào
                                               //chỉ class kế thừa MonoBehaviour thì mới có thể gắn scripts này lên GameObject 
{
    public PlayerCar[] playerCar;

    public int PlayerCarCount
    {
        get
        {
            return playerCar.Length; //phương thức khởi tạo này sẽ trả về số lượng phần tử trong mảng playerCar
        }
    }

    public PlayerCar GetPlayerCar (int index)
    {
        return playerCar[index];
    }
}
