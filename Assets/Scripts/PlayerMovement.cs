using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using UnityEngine;

public class Player : MonoBehaviour
{
    public Transform transform;
    public float speed; 
    public float rotationSpeed;
    public float limitX = 1.89f; // giá trị điểm position.x giới hạn để Player không đi ra khỏi màn hình game
    void Start()
    {
        transform = GetComponent<Transform>(); //dòng code này thay cho thao tác kéo tham chiếu transform của đối tượng
    }

    void Update()
    {
        //float moveDistance = speed * Time.deltaTime;
        if (Input.GetKey(KeyCode.RightArrow))
        {
            //transform.Translate(Vector2.right * moveDistance); 
            //code theo cách này xảy ra lỗi mỗi khi quay hướng đối tượng sẽ bị lùi dần xuống theo góc chéo vì Translate di chuyển cả đối tượng, chứ không di chuyển đối tượng theo hệ tọa độ
            transform.position += new Vector3(speed * Time.deltaTime, 0, 0);// += vì đi sang phải theo trục x
            transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.Euler(0, 0, -25), rotationSpeed * Time.deltaTime);
            //Quaternion.Euler() trong Unity được sử dụng để tạo ra một đối tượng Quaternion từ các giá trị Euler.
            //Euler là cách biểu diễn góc quay trong không gian ba chiều bằng cách sử dụng ba giá trị số thực, mỗi giá trị đại diện cho góc quay quanh một trong ba trục(x, y, z).
            //Quaternion.Lerp() quay đối tượng từ hướng này, sang hướng khác, với tốc độ nhất định.
        }

        if (Input.GetKey(KeyCode.LeftArrow))
        {
            transform.position -= new Vector3(speed * Time.deltaTime, 0, 0);// -= vì đi sang trái theo trục x
            transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.Euler(0, 0, 25), rotationSpeed * Time.deltaTime);
        }

        if (!Input.GetKey(KeyCode.RightArrow) && !Input.GetKey(KeyCode.LeftArrow))
        {
            // Quay trở lại góc 0 độ
            transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.Euler(0, 0, 0), rotationSpeed * Time.deltaTime);

        }

        //cách làm để đối tượng không di chuyển ra khỏi màn hình chính
        if (transform.position.x < -limitX) 
            //nếu đối tượng di chuyển qua điểm giới hạn thì khởi tạo lại đối tượng ở ngay điểm giới hạn đấy
            //phải set tọa độ Vector3 nếu không tọa độ z mặc định sẽ bằng 0, và sẽ bị nằm dưới background
        {
            transform.position = new Vector3(
                -limitX,
                transform.position.y,
                transform.position.z
                );
            // hướng của đối tượng cũng xoay dần về góc 0
            transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.Euler(0, 0, 0), rotationSpeed * Time.deltaTime);
            /*nếu ta đè lúc di chuyển
            hướng của đối tượng vẫn hơi nghiêng 1 góc bằng khoảng 1/2 với góc như bên trên lúc di chuyển đã set
            vì đối tượng gần như đồng thời quay về góc 0 và gần như đồng thời quay về góc như trên lúc di chuyển set*/
        }

        else if (transform.position.x > limitX)
        {
            transform.position = new Vector3(
                limitX,
                transform.position.y,
                transform.position.z
                );
            transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.Euler(0, 0, 0), rotationSpeed * Time.deltaTime);
        }
    }
}
