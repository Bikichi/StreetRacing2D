using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Coin : MonoBehaviour
{
    public Transform coinTransform;
    public float coinSpeed;

    void Start()
    {
        coinTransform = GetComponent<Transform>();
    }

    void Update()
    {
        coinTransform.position -= new Vector3(
            0f,
            coinSpeed * Time.deltaTime,
            0f
            );
        if (coinTransform.position.y <= -6 || Player.Ins.isDead)
        {
            Destroy(gameObject);
        }
    }
}
