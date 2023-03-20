using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum eBrickType
{
    NONE = -1,
    NORMAL = 0,
    HARD = 1,
    BONUS_BALL = 2,
    MISSILE = 3,
    LASER_HORIZONTAL = 4,
    LASER_VERTICAL = 5,
    PADDLE_SHORTEN = 6,
    PADDLE_LENGTHEN
}

public class Brick : MonoBehaviour
{
    [SerializeField] eBrickType type;
    [SerializeField] int _brickHP;
    [SerializeField] int _curHP;

    void Start()
    {
        switch((int)type)
        {
            case 1:
                _brickHP = 10;
                break;
            default:
                _brickHP = 1;
                break;
        }

        _curHP = _brickHP;
    }

    void Update()
    {

    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.CompareTag("Ball"))
        {
            _curHP--;

            if (_curHP <= 0)
            {
                gameObject.SetActive(false);
            }
        }
    }
}
