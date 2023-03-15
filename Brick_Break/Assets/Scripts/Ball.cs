using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ball : MonoBehaviour
{
    Rigidbody2D rigid;
    [SerializeField] float _speed;
    public bool _isDown = true;

    void Awake()
    {
        transform.position = new Vector2(0, 0);
        rigid = GetComponent<Rigidbody2D>();
        rigid.velocity = Vector2.down * _speed;
    }

    void Update()
    {
        Debug.Log(rigid.velocity);
    }

    void FixedUpdate()
    {
        //Move();
    }

    void Move()
    {
        rigid.AddForce(Vector2.up * _speed * Time.deltaTime);
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject != null)
        {
            _isDown = !_isDown;

            if(_isDown)
            {
                rigid.velocity = Vector2.up * _speed;
            }
            else
            {
                rigid.velocity = Vector2.down * _speed;
            }

            //rigid.velocity *= -1;
        }
    }
}
