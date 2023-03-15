using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Paddle : MonoBehaviour
{
    Rigidbody2D rigidbody;
    Camera cam;                // 메인 카메라
    float _minX = -2.5f;
    float _maxX = 2.5f;

    void Awake()
    {
        rigidbody = GetComponent<Rigidbody2D>();
        cam = Camera.main;
    }

    void Update()
    {
        MovePaddle();
    }

    void MovePaddle()
    {
        Vector2 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);         // 현재 마우스 위치를 월드 좌표로 변환
        this.transform.position = new Vector2(mousePos.x, this.transform.position.y);

        if(this.transform.position.x <= _minX)
        {
            this.transform.position = new Vector2(_minX, this.transform.position.y);
        }
        else if(this.transform.position.x >= _maxX)
        {
            this.transform.position = new Vector2(_maxX, this.transform.position.y);
        }
    }
}
