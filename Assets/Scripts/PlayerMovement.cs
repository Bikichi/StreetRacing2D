using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public Transform transform;
    public float speed;
    void Start()
    {
        
    }

    void Update()
    {
        float moveDistance = speed * Time.deltaTime;
        if (Input.GetKey(KeyCode.RightArrow))
        {
            transform.Translate(Vector2.right * moveDistance);
        }

        if (Input.GetKey(KeyCode.LeftArrow))
        {
            transform.Translate(Vector2.left * moveDistance);
        }
    }
}
