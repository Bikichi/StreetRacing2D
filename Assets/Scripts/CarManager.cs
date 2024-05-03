using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarManager : MonoBehaviour
{
    public Transform transform;
    public float speed = 7.5f;
    public ScoreManger scoreManger; //tạo đối tượng để truy cập đến thuộc tính 
    //dễ dùng nhưng chưa tối ưu vì tạo cả 1 đối tượng nhưng chỉ dùng để truy cập tới 1 thuộc tính cần
    //chuẩn ra cần dùng delegate, event
    private bool hasPassed = false; // Biến đánh dấu xem đã tăng giá trị carsPassedTotal hay chưa
    public Player player; //tạo đối tượng để truy cập đến thuộc tính 
    void Start()
    {
        transform = GetComponent<Transform>();
        scoreManger = GameObject.FindObjectOfType<ScoreManger>(); //Tìm đối tượng ScoreManger trong Scene để tham chiếu
        //Phương thức GetComponent<>() được sử dụng để lấy thành phần (component) được gắn vào cùng một đối tượng với script hiện tại.
        //Trong trường hợp của bạn, nếu hai scripts CarManager và ScoreManger không được gắn vào cùng một đối tượng, việc sử dụng GetComponent<ScoreManger>() sẽ không hoạt động.
        //Để lấy thành phần từ một đối tượng khác trong Scene, bạn cần sử dụng các phương thức khác như FindObjectOfType<>() hoặc lưu trữ đối tượng đó từ một nguồn khác như đã đề cập trong cách trước đó.
        //GetComponent<ScoreManger>() trong trường hợp này sẽ không hoạt động vì nó không thể tìm thấy thành phần ScoreManger trên cùng một đối tượng chứa script CarManager.
        player = GameObject.FindObjectOfType<Player>();
    }

    void Update()
    {
        transform.position -= new Vector3(0, speed * Time.deltaTime, 0);
        //Để -= vì nó di chuyển đi xuống
        //Không thể code di chuyển kiểu Background vì Position đã set 90 để quay dọc
        
        if (!hasPassed && transform.position.y <= player.transform.position.y) // // Nếu chưa tăng giá trị và vị trí của đối tượng xuống dưới đối tượng Player
        {
            scoreManger.score += 1; //truy cập tới thuộc tính của class thông qua đối tượng
            hasPassed = true; // Đánh dấu đã tăng giá trị
        }

        if (transform.position.y <= -6)
        {
            Destroy(gameObject); // vì đến vị trí này đối tượng bị hủy nên scripts cũng bị hủy nên để lưu biến đếm tổng số xe đã vượt qua thì phải tạo biến này ở 1 class khác
        }
    }
}