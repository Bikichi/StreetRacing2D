using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public Transform PMtransform;
    public RoadScolling roadScolling;

    public float speed;
    public float rotationSpeed;

    public bool isMoveLeft;
    public bool isMoveRight;
    public bool isMoveUp;
    public bool isMoveDown;

    public float limitX = 1.89f; // giá trị điểm position.x giới hạn để Player không đi ra khỏi màn hình game
    public float limitY = 3.5f; //tương tự
    private void Start()
    {
        PMtransform = GetComponent<Transform>(); //dòng code này thay cho thao tác kéo tham chiếu transform của đối tượng
        roadScolling = GameObject.FindObjectOfType<RoadScolling>(); //Tìm đối tượng RoadScolling trong Scene để tham chiếu
        ////Phương thức GetComponent<>() được sử dụng để lấy thành phần (component) được gắn vào cùng một đối tượng với script hiện tại.
        ////Trong trường hợp của bạn, nếu hai scripts PlayerManager và RoadScolling không được gắn vào cùng một đối tượng, việc sử dụng GetComponent<RoadScolling>() sẽ không hoạt động.
        ////Để lấy thành phần từ một đối tượng khác trong Scene, bạn cần sử dụng các phương thức khác như FindObjectOfType<>() hoặc lưu trữ đối tượng đó từ một nguồn khác như đã đề cập trong cách trước đó.
    }

    void Update()
    {
        if (Player.Ins.isDead || !GameManager.Ins.isGamePlaying) return;
        Movement();
        CheckMoveUpDown();
        CheckMoveLeftRight();
        CheckLimitPositionX();
        CheckLimitPositionY();
        UpSpeedPlayerAccordingToScore();
    }

    public void Movement()
    {
        if (isMoveUp)
        {
            MoveUp();
        }
        else if (isMoveDown)
        {
            MoveDown();
        }
        else if (isMoveLeft)
        {
            MoveLeft();
        }
        else if (isMoveRight)
        {
            MoveRight();
        }
    }

    public void PointerDownMoveUp()
    {
        isMoveUp = true;
    }
    public void PointerUpMoveUp()
    {
        isMoveUp = false;
    }

    public void PointerDownMoveDown()
    {
        isMoveDown = true;
    }
    public void PointerUpMoveDown()
    {
        isMoveDown = false;
    }

    public void PointerDownMoveLeft()
    {
        isMoveLeft = true;
    }
    public void PointerUpMoveLeft()
    {
        isMoveLeft = false;
    }

    public void PointerDownMoveRight()
    {
        isMoveRight = true;
    }
    public void PointerUpMoveRight()
    {
        isMoveRight = false;
    }

    public void MoveUp()
    {
        roadScolling.speed = 13.5f;
        PMtransform.position += new Vector3(0, speed * Time.deltaTime, 0);
    }

    public void MoveDown()
    {
        roadScolling.speed = 7.5f;
        PMtransform.position -= new Vector3(0, speed * Time.deltaTime, 0);

    }

    public void MoveRight()
    {
        isMoveRight = true;
        PMtransform.position += new Vector3(speed * Time.deltaTime, 0, 0);// += vì đi sang phải theo trục x
        PMtransform.rotation = Quaternion.Lerp(PMtransform.rotation, Quaternion.Euler(0, 0, -25), rotationSpeed * Time.deltaTime);
        //Quaternion.Euler() trong Unity được sử dụng để tạo ra một đối tượng Quaternion từ các giá trị Euler.
        //Euler là cách biểu diễn góc quay trong không gian ba chiều bằng cách sử dụng ba giá trị số thực, mỗi giá trị đại diện cho góc quay quanh một trong ba trục(x, y, z).
        //Quaternion.Lerp() quay đối tượng từ hướng này, sang hướng khác, với tốc độ nhất định.
    }

    public void MoveLeft()
    {
        isMoveLeft = true;
        PMtransform.position -= new Vector3(speed * Time.deltaTime, 0, 0);// -= vì đi sang trái theo trục x
        PMtransform.rotation = Quaternion.Lerp(PMtransform.rotation, Quaternion.Euler(0, 0, 25), rotationSpeed * Time.deltaTime);
    }

    void CheckMoveUpDown()
    {
        if (!isMoveDown && !isMoveUp)
        {
            roadScolling.speed = 10f;
        }
    }

    void CheckMoveLeftRight()
    {
        if (!isMoveRight && !isMoveLeft)
        {
            // Quay trở lại góc 0 độ
            PMtransform.rotation = Quaternion.Lerp(PMtransform.rotation, Quaternion.Euler(0, 0, 0), rotationSpeed * Time.deltaTime);

        }
    }

    void CheckLimitPositionX()
    {
        //cách làm để đối tượng không di chuyển ra khỏi màn hình chính
        if (PMtransform.position.x <= -limitX)
        //nếu đối tượng di chuyển qua điểm giới hạn thì khởi tạo lại đối tượng ở ngay điểm giới hạn đấy
        //phải set tọa độ Vector3 nếu không tọa độ z mặc định sẽ bằng 0, và sẽ bị nằm dưới background
        {
            PMtransform.position = new Vector3(
                -limitX,
                PMtransform.position.y,
                PMtransform.position.z
                );
            // hướng của đối tượng cũng xoay dần về góc 0
            PMtransform.rotation = Quaternion.Lerp(PMtransform.rotation, Quaternion.Euler(0, 0, 0), rotationSpeed * Time.deltaTime);
            /*nếu ta đè lúc di chuyển
            hướng của đối tượng vẫn hơi nghiêng 1 góc bằng khoảng 1/2 với góc như bên trên lúc di chuyển đã set
            vì đối tượng gần như đồng thời quay về góc 0 và gần như đồng thời quay về góc như trên lúc di chuyển set*/
        }

        else if (PMtransform.position.x >= limitX)
        {
            PMtransform.position = new Vector3(
                limitX,
                PMtransform.position.y,
                PMtransform.position.z
                );
            PMtransform.rotation = Quaternion.Lerp(PMtransform.rotation, Quaternion.Euler(0, 0, 0), rotationSpeed * Time.deltaTime);
        }
    }

    void CheckLimitPositionY()
    {
        if (PMtransform.position.y >= limitY)
        {
            PMtransform.position = new Vector3(
                PMtransform.position.x,
                limitY,
                PMtransform.position.z
                );
            roadScolling.speed = 12f;
        }

        else if (PMtransform.position.y <= -limitY)
        {
            PMtransform.position = new Vector3(
                PMtransform.position.x,
                -limitY,
                PMtransform.position.z
                );
            roadScolling.speed = 12f;
        }
    }
    void UpSpeedPlayerAccordingToScore() //tăng tốc độ theo điểm số
    {
        if (ScoreManager.Ins.score >= 0 && ScoreManager.Ins.score < 30)
        {
            speed = 5.0f;
            rotationSpeed = 5.0f;
        }
        else if (ScoreManager.Ins.score >= 30 && ScoreManager.Ins.score < 60)
        {
            speed = 5.5f;
            rotationSpeed = 5.5f;
        }
        else if (ScoreManager.Ins.score >= 60 && ScoreManager.Ins.score < 100)
        {
            speed = 6.0f;
            rotationSpeed = 6.0f;
        }
        else if (ScoreManager.Ins.score >= 100 && ScoreManager.Ins.score < 150)
        {
            speed = 7.0f;
            rotationSpeed = 7.0f;
        }
        else
        {
            speed = 7.5f;
            rotationSpeed = 7.5f;
        }
    }
}
