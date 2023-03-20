using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ball : MonoBehaviour
{
    Rigidbody2D rigid;
    [SerializeField] float _speed;
    public bool _isDown = true;
    private bool _isFirstClick = false;
    Vector2 _lastVelocity;

    void Awake()
    {
        //transform.position = new Vector2(0, 0);
        rigid = GetComponent<Rigidbody2D>();
        //rigid.velocity = Vector2.down * _speed;
    }

    void Update()
    {
        FirstMove();
        _lastVelocity = rigid.velocity;
        Debug.Log(rigid.velocity);
    }

    void FixedUpdate()
    {

    }

    void FirstMove()
    {
        if(Input.GetMouseButtonDown(0))
        {
            _isFirstClick = true;
            gameObject.transform.parent = null;
            rigid.velocity = Vector2.up * _speed;
        }
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Wall") || collision.gameObject.CompareTag("Brick"))
        {
            float curSpeed = _lastVelocity.magnitude;              // ������ ũ�⸸ŭ ���������� ����
            Vector2 dir = Vector2.Reflect(_lastVelocity.normalized, collision.contacts[0].normal);       // �浹 ��� �ӵ��� ũ�⸸ŭ �ݻ簢 ����, normalize�Ͽ� ������ ũ�⸦ 1�� ����
            rigid.velocity = dir * Mathf.Max(curSpeed, 0f);        // �� ���� ���� �ӵ��� �ݻ簢 ������ �������� ���ǵ常ŭ ����
        }
    }

    public float GetSpeed()
    {
        return _speed;
    }
}
