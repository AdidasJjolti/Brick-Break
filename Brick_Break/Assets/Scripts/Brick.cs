using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum eBrickType
{
    NONE = -1,
    NORMAL = 0,
    HARD = 1,
    ITEM
}

public class Brick : MonoBehaviour
{
    [SerializeField] eBrickType type;
    public int _brickHP;

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
    }

    void Update()
    {

    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.CompareTag("Ball"))
        {
            _brickHP--;

            if (_brickHP <= 0)
            {
                gameObject.SetActive(false);
            }
        }
    }

    //public void ChangeHP()
    //{
    //    _brickHP--;

    //    if(_brickHP <=0)
    //    {
    //        gameObject.SetActive(false);
    //    }
    //}
}
