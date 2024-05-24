using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimalMovement : MonoBehaviour
{
 public float speed = 5f;
    private Vector3 direction = Vector3.right; // Con vật sẽ di chuyển sang phải ban đầu

    void Update()
    {
        Move();
    }

    void Move()
    {
        // Di chuyển con vật theo hướng và tốc độ đã xác định
        transform.Translate(direction * speed * Time.deltaTime);

        // Kiểm tra va chạm với biên giới và thay đổi hướng nếu cần
        if (transform.position.x > 10f || transform.position.x < -10f)
        {
            direction *= -1f; // Đảo hướng khi va chạm với biên giới
        }
    }
}
