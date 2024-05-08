using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RebornPlayer : MonoBehaviour
{
    public Transform transform;
    public GameObject player;
    void Start()
    {
        transform = GetComponent<Transform>();
    }
    public void Reborn()
    {
        Instantiate(player, transform.position, Quaternion.identity);
    }
}
