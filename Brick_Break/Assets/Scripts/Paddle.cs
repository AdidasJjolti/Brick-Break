using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Paddle : MonoBehaviour
{
    Rigidbody2D rigidbody;
    Camera cam;                // ���� ī�޶�
    float _minX = -2.5f;
    float _maxX = 2.5f;
    float _maxBounceAngle = 80f;

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
        Vector2 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);         // ���� ���콺 ��ġ�� ���� ��ǥ�� ��ȯ
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

    void OnCollisionEnter2D(Collision2D collision)
    {
        Ball ball = collision.gameObject.GetComponent<Ball>();

        if(ball != null)
        {
            Vector3 paddlePosition = transform.position;
            Vector2 contactPosition = collision.GetContact(0).point;

            float offset = paddlePosition.x - contactPosition.x;
            float width = collision.otherCollider.bounds.size.x / 2;             // otherCollider = �浹�� ������ ������Ʈ�� ����� �浹ü

            float bounceAngle = (offset / width) * _maxBounceAngle;              // �е� �ʺ� ��� �浹 ������ ���� �ݻ簢�� ����, ����(������)���� ������ ����(������)���� ���� ����

            Quaternion rotation = Quaternion.AngleAxis(bounceAngle, Vector3.forward);
            ball.GetComponent<Rigidbody2D>().velocity = rotation * Vector2.up * ball.GetSpeed();
        }
    }
}
