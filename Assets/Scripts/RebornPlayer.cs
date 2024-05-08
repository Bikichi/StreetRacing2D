using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RebornPlayer : MonoBehaviour
{
    public Transform transform;
    public GameObject player;
    public GameObject effect;
    void Start()
    {
        transform = GetComponent<Transform>();
    }
    public void Reborn()
    {
        if (effect) 
        {
            var effectI = Instantiate(effect, transform.position, Quaternion.identity);
            Destroy(effectI, 0.5f);
        }

        if (player)
        {
            Instantiate(player, transform.position, Quaternion.identity);
        }
    }
}
