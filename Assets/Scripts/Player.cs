using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using UnityEditor;
using UnityEngine;

public class Player : MonoBehaviour
{
    public static Player Ins; //khởi tạo Design Pattern - Singletons, thể hiện tính toàn cục và duy nhất
    //các Class khác có thể truy cập tới thuộc tính của class này
    //game chỉ có 1 player nên mới có thể dùng Singletons
    public Transform transform;
    public float speed; 
    public float rotationSpeed;
    public float limitX = 1.89f; // giá trị điểm position.x giới hạn để Player không đi ra khỏi màn hình game
    public float limitY = 3.5f; //tương tự
    public GameObject bigBang;
    public bool isDead;
    public RoadScolling roadScolling;

    private void Awake()
    {
        if (Ins != null && Ins != this) //nếu như đã có thằng khác khởi tạo Singletons này
        {
            Destroy(Ins);
        }
        else 
        {
            Ins = this;
        }
    }

    void Start()
    {
        transform = GetComponent<Transform>(); //dòng code này thay cho thao tác kéo tham chiếu transform của đối tượng
        roadScolling = GameObject.FindObjectOfType<RoadScolling>(); //Tìm đối tượng RoadScolling trong Scene để tham chiếu
        ////Phương thức GetComponent<>() được sử dụng để lấy thành phần (component) được gắn vào cùng một đối tượng với script hiện tại.
        ////Trong trường hợp của bạn, nếu hai scripts PlayerManager và RoadScolling không được gắn vào cùng một đối tượng, việc sử dụng GetComponent<RoadScolling>() sẽ không hoạt động.
        ////Để lấy thành phần từ một đối tượng khác trong Scene, bạn cần sử dụng các phương thức khác như FindObjectOfType<>() hoặc lưu trữ đối tượng đó từ một nguồn khác như đã đề cập trong cách trước đó.
        ////GetComponent<RoadScolling>() trong trường hợp này sẽ không hoạt động vì nó không thể tìm thấy thành phần RoadScolling trên cùng một đối tượng chứa script PlayerManager.
    }
    void Update()
    {
        if (isDead || !GameManager.Ins.isGamePlaying) return;
        MoveDown();
        MoveUp();
        CheckMoveUpDown();
        MoveLeft();
        MoveRight();
        CheckMoveLeftRight();
        CheckLimitPositionX();
        CheckLimitPositionY();
    }

    void MoveUp ()
    {
        if (Input.GetKey(KeyCode.UpArrow))
        {
            roadScolling.speed = 14f;
            transform.position += new Vector3(0, speed * Time.deltaTime, 0);
        }
    }

    void MoveDown()
    {
        if (Input.GetKey(KeyCode.DownArrow))
        {
            roadScolling.speed = 8f;
            transform.position -= new Vector3(0, speed * Time.deltaTime, 0);
        }
    } 

    void CheckMoveUpDown () 
    {
        if (!Input.GetKey(KeyCode.UpArrow) && !Input.GetKey(KeyCode.DownArrow))
        {
            roadScolling.speed = 12f;
        }
    }

    void MoveRight()
    {
        if (Input.GetKey(KeyCode.RightArrow))
        {
            transform.position += new Vector3(speed * Time.deltaTime, 0, 0);// += vì đi sang phải theo trục x
            transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.Euler(0, 0, -25), rotationSpeed * Time.deltaTime);
            //Quaternion.Euler() trong Unity được sử dụng để tạo ra một đối tượng Quaternion từ các giá trị Euler.
            //Euler là cách biểu diễn góc quay trong không gian ba chiều bằng cách sử dụng ba giá trị số thực, mỗi giá trị đại diện cho góc quay quanh một trong ba trục(x, y, z).
            //Quaternion.Lerp() quay đối tượng từ hướng này, sang hướng khác, với tốc độ nhất định.
        }
    }

    void MoveLeft()
    {
        if (Input.GetKey(KeyCode.LeftArrow))
        {
            transform.position -= new Vector3(speed * Time.deltaTime, 0, 0);// -= vì đi sang trái theo trục x
            transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.Euler(0, 0, 25), rotationSpeed * Time.deltaTime);
        }
    }

    void CheckMoveLeftRight() 
    {
        if (!Input.GetKey(KeyCode.RightArrow) && !Input.GetKey(KeyCode.LeftArrow))
        {
            // Quay trở lại góc 0 độ
            transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.Euler(0, 0, 0), rotationSpeed * Time.deltaTime);

        }
    }

    void CheckLimitPositionX()
    {
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
    }

    void CheckLimitPositionY()
    {
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
            Destroy(gameObject); //hủy đối tượng và phương thức gắn vào
            Destroy(col.gameObject); //hủy đối tượng va chạm phải

            isDead = true;
            if (bigBang) //nếu đối tượng khác null
            {
                var bigBangCopy = Instantiate(bigBang, transform.position, Quaternion.identity); //đối tượng được tạo ra từ Instantiate là 1 bản sao của bigBang
                //Instantiate() là một phương thức được sử dụng để tạo ra một BẢN SAO mới của một prefab hoặc một đối tượng có sẵn trong trò chơi
                //truyền vào GameOject, vị trí, góc quay 
                //Một Quaternion là một cách biểu diễn các phép quay trong không gian ba chiều. 
                //khi bạn sử dụng Quaternion.identity, bạn đang chỉ định rằng không có phép quay nào được áp dụng, nghĩa là đối tượng sẽ không bị xoay khi được tạo ra hoặc di chuyển 
                Destroy(bigBangCopy, 0.35f);
                //phải set 1 khoảng thời gian chờ để chạy Animation trước khi Destroy GameObject
            }
            GameManager.Ins.GameOver();
        }

        if (col.CompareTag(Const.COIN_TAG))
        {
            Destroy(col.gameObject);
            ScoreManager.Ins.score += 10;
        }
    }
}
