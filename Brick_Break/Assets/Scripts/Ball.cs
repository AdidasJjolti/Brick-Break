using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ball : MonoBehaviour
{
    Rigidbody2D rigid;
    [SerializeField] float _speed;
    [SerializeField] float _degree;
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

        //if (_degree < 10)
        //{
        //    _degree = 10;
        //    float curSpeed = _lastVelocity.magnitude;
        //    var dir = (Vector2)(Quaternion.Euler(0, 0, _degree) * Vector2.up);
        //    rigid.velocity = dir * Mathf.Max(curSpeed, 0f);        // �� ���� ���� �ӵ��� �ݻ簢 ������ �������� ���ǵ常ŭ ����
        //}
        //else if (_degree > 170)
        //{
        //    _degree = 170;
        //    float curSpeed = _lastVelocity.magnitude;
        //    var dir = (Vector2)(Quaternion.Euler(0, 0, _degree) * Vector2.up);
        //    rigid.velocity = dir * Mathf.Max(curSpeed, 0f);        // �� ���� ���� �ӵ��� �ݻ簢 ������ �������� ���ǵ常ŭ ����
        //}

        //if(_lastVelocity.magnitude < _speed)
        //{
        //    _lastVelocity = _lastVelocity.normalized * _speed;
        //}
    }

    void FixedUpdate()
    {

    }

    void FirstMove()
    {
        if(Input.GetMouseButtonDown(0) && _isFirstClick == false)
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

            //Vector2 v2 = Vector2.up - dir;
            //float degree = Mathf.Atan2(v2.y, v2.x) * Mathf.Rad2Deg;
            //_degree = degree;
            //Debug.Log("Degree is " + degree);

            rigid.velocity = dir * Mathf.Max(curSpeed, 0f);        // �� ���� ���� �ӵ��� �ݻ簢 ������ �������� ���ǵ常ŭ ����
        }
    }

    public float GetSpeed()
    {
        return _speed;
    }
}
