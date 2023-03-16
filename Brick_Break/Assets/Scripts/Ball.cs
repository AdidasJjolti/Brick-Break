using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ball : MonoBehaviour
{
    Rigidbody2D rigid;
    [SerializeField] float _speed;
    public bool _isDown = true;
    private bool _isFirstClick = false;

    void Awake()
    {
        //transform.position = new Vector2(0, 0);
        rigid = GetComponent<Rigidbody2D>();
        //rigid.velocity = Vector2.down * _speed;
    }

    void Update()
    {
        FirstMove();
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
        if (collision.gameObject.CompareTag("Wall"))
        {
            //_isDown = !_isDown;

            //if (_isDown)
            //{
            //    rigid.velocity = Vector2.up * _speed;
            //}
            //else
            //{
            //    rigid.velocity = Vector2.down * _speed;
            //}

            rigid.velocity = new Vector2(0, -1 * _speed);
        }
    }

    public float GetSpeed()
    {
        return _speed;
    }
}
