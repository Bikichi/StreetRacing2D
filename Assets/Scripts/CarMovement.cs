using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarMovement : MonoBehaviour
{
    public Transform transform;
    public float speed;
    // Start is called before the first frame update
    void Start()
    {
        transform = GetComponent<Transform>();
    }

    // Update is called once per frame
    void Update()
    {
        transform.position -= new Vector3(0, speed * Time.deltaTime, 0);
        //Để -= vì nó di chuyển đi xuống
        //Không thể code di chuyển kiểu Background vì Position đã set 90 để quay dọc
    }
}
