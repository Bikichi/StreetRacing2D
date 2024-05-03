using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using UnityEditor;
using UnityEngine;
using UnityEngine.UIElements;

public class Player : MonoBehaviour
{
    public Transform transform;
    public float speed; 
    public float rotationSpeed;
    public float limitX = 1.89f; // giá trị điểm position.x giới hạn để Player không đi ra khỏi màn hình game
    public float limitY = 3.5f; //tương tự
    public GameObject bigBang;
    public bool isDead;
    public RoadScolling roadScolling;
    public ScoreManger scoreManger;
    void Start()
    {
        transform = GetComponent<Transform>(); //dòng code này thay cho thao tác kéo tham chiếu transform của đối tượng
        scoreManger = GameObject.FindObjectOfType<ScoreManger>();
        roadScolling = GameObject.FindObjectOfType<RoadScolling>();
    }

    void Update()
    {
        if (Input.GetKey(KeyCode.UpArrow))
        {
            roadScolling.speed = 12;
            transform.position += new Vector3(0, speed * Time.deltaTime, 0);
        }

        if (Input.GetKey(KeyCode.DownArrow))
        {
            roadScolling.speed = 4;
            transform.position -= new Vector3(0, speed * Time.deltaTime, 0);
        }

        if (!Input.GetKey(KeyCode.UpArrow) && !Input.GetKey(KeyCode.DownArrow))
        {
            roadScolling.speed = 8;
        }

        if (Input.GetKey(KeyCode.RightArrow))
        {
            //float moveDistance = speed * Time.deltaTime;
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
        if (transform.position.x <= -limitX) 
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

        else if (transform.position.x >= limitX)
        {
            transform.position = new Vector3(
                limitX,
                transform.position.y,
                transform.position.z
                );
            transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.Euler(0, 0, 0), rotationSpeed * Time.deltaTime);
        }

        if (transform.position.y >= limitY)
        {
            transform.position = new Vector3(
                transform.position.x,
                limitY,
                transform.position.z
                );
            roadScolling.speed = 8;
        }

        else if (transform.position.y <= -limitY)
        {
            transform.position = new Vector3(
                transform.position.x,
                -limitY,
                transform.position.z
                );
            roadScolling.speed = 8;
        }
    }

    private void OnTriggerEnter2D(Collider2D col) //Đây là khai báo của phương thức
                                                  //Nó sẽ được gọi mỗi khi một Collider2D khác va chạm với Collider2D của đối tượng này
                                                  //Tham số "col" là Collider2D của đối tượng khác mà va chạm với Collider2D của đối tượng này.
    {
        if (col.CompareTag(Const.CARS_TAG)) //nếu đối tượng này va chạm với đối tượng có tag là CARS_TAG thì thực thi
        {
            /*gameObject.SetActive(false);*/ //tắt đối tượng và phương thức gắn vào
            Destroy(col.gameObject); //hủy đối tượng va chạm phải

            isDead = true;
            if (bigBang) //nếu đối tượng khác null
            {
                var bigBangCopy = Instantiate(bigBang, transform.position, Quaternion.identity); //đối tượng được tạo ra từ Instantiate là 1 bản sao của bigBang
                //Instantiate() là một phương thức được sử dụng để tạo ra một BẢN SAO mới của một prefab hoặc một đối tượng có sẵn trong trò chơi
                //truyền vào GameOject, vị trí, góc quay 
                //Một Quaternion là một cách biểu diễn các phép quay trong không gian ba chiều. 
                //khi bạn sử dụng Quaternion.identity, bạn đang chỉ định rằng không có phép quay nào được áp dụng, nghĩa là đối tượng sẽ không bị xoay khi được tạo ra hoặc di chuyển 
                Destroy(bigBangCopy, 0.2f);
                //phải set 1 khoảng thời gian chờ để chạy Animation trước khi Destroy GameObject
                //StartCoroutine(DelayAndDestroy(bigBangCopy, 0.15f));
                //đây là 1 cách khác hoặc có cách khác là ta có thêm script AnimationEvent vào Animation
            }
        }

        if (col.CompareTag(Const.COIN_TAG))
        {
            Destroy(col.gameObject);
            scoreManger.score += 10;
        }
    }

    //private IEnumerator DelayAndDestroy(GameObject obj, float delay)
    //{
    //    yield return new WaitForSeconds(delay);
    //    Destroy(obj);
    //}
}
