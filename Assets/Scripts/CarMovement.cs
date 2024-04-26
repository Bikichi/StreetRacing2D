using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarMovement : MonoBehaviour
{
    public Transform transform;
    public float speed = 7.5f;
    void Start()
    {
        transform = GetComponent<Transform>();
    }

    void Update()
    {
        transform.position -= new Vector3(0, speed * Time.deltaTime, 0);
        //Để -= vì nó di chuyển đi xuống
        //Không thể code di chuyển kiểu Background vì Position đã set 90 để quay dọc
        if (transform.position.y <= -6)
        {
            Destroy(gameObject);
        }
    }
}
