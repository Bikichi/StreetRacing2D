using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class RoadScolling : MonoBehaviour
{
    //ý tưởng là sẽ có 2 background y hệt nhau khởi tạo liên tiếp nhau khi ô tô của người chơi chạy hết background2 thì sẽ đảo background1 kia vào kế tiếp
    //chạy hết background1 thì ngược lại, cứ liên tục như vậy
    public float speed; //tốc độ trôi của màn hình
    public Transform backGround1; //biến kiểu Transform (vị trí, hướng, độ lớn)
    public Transform backGround2;
    float ySize; //chiều cao của background
    //public bool isStartScolling;

    public void Start() //chương trình sẽ chạy hàm này đầu tiên sau đó bắt đầu trò chơi
                        //hàm này sẽ được chạy trước các hàm như Start(), Update(), LateUpdate(), FixedUpdate(),...
    {
        ySize = backGround1.GetComponent<BoxCollider2D>().size.y * backGround1.transform.localScale.y;
        //kích thước thực sự của một đối tượng trong không gian game là kết quả của phép nhân kích thước của collider và tỷ lệ scale của nó 
        //(vì đôi khi tỷ lệ scale chúng ta set khác 1)

        backGround1.transform.position = Vector2.zero; //đại diện cho điểm có tọa độ (0, 0)
        //điều này có nghĩa là nó là một Vector2 không di chuyển, không thay đổi vị trí so với gốc tọa độ của không gian hai chiều
        //mục đích ở đây là để khởi tạo vị trí làm mốc
        backGround2.transform.position = new Vector2(
            backGround1.transform.position.x, //2 background cùng chung tọa độ x
            backGround1.transform.position.y + ySize // vị trí để chồng BG2 lên BG1 
            );
    }

    public void Update() // hàm này được gọi mỗi frame (khung hình)
    {
        if (!GameManager.Ins.isGamePlaying || Player.Ins.isDead)
            return;

        //Time.deltaTime là một giá trị thời gian (đơn vị là giây)
        //Time.deltaTime tính khoảng thời gian tồn tại giữa 2 frame liên tiếp,  - hay nói cách khác là thời gian tồn tại của 1 frame
        //Khoảng thời gian tính bằng giây từ khung hình gần nhất đến khung hình hiện tại (Chỉ đọc).
        float moveDistance = speed * Time.deltaTime; //khoảng cách mà đối tượng sẽ di chuyển trong mỗi khung hình
        transform.Translate(Vector2.down * moveDistance);
        //Translate là phương thức thành phần trong Transform được sử dụng để di chuyển đối tượng trong không gian
        //Bên trên là cú pháp, tham số truyền vào của Translate

        if (backGround1.transform.position.y <= -ySize) // về bản chất thì 1 cái là vị trí, 1 cái là kích thước nhưng cả hai đều có giá trị float nên so sánh được
        /*thỏa mãn điều kiện trên thì đối tượng đang làm mốc (background1) đã di chuyển xuống ngoài màn hình hoàn toàn, dựa trên vị trí của nó trên trục y.*/
        //ySize chính là chiều cao của background, lấy giá trị âm vì nó di chuyển xuống dưới
        //Mặc định, gốc tọa độ của một đối tượng được đặt tại điểm góc dưới cùng bên trái của đối tượng đó.

        {
            backGround1.transform.position = new Vector2(
                backGround2.transform.position.x,
                backGround2.transform.position.y + ySize // vị trí để chồng BG2 lên BG1 
            );

            Transform temp = backGround1; //đặt biến tạm để hoán đổi đối tượng
            /*trao đổi vị trí giữa backGround1 và backGround2 
            để backGround2 trở thành backGround1 cho lần di chuyển tiếp theo*/
            backGround1 = backGround2;
            backGround2 = temp;

        }
    }


}
