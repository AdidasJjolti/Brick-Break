using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

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

public abstract class Brick : MonoBehaviour
{
    [SerializeField] eBrickType type;
    [SerializeField] float _brickHP;
    [SerializeField] float _curHP;
    public Slider _hpBar;

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

    void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.CompareTag("Ball"))
        {
            _curHP--;
            HPBarChanged();
            _hpBar.gameObject.SetActive(true);

            if (_curHP <= 0)
            {
                gameObject.SetActive(false);
                GameManager.Instance.ChangeBrickCount();
            }
        }
    }

    public virtual void SetHPBar(GameObject hpBar)
    {
        _hpBar = hpBar.GetComponent<Slider>();
    }


    // 벽돌이 피격당할 때마다 HP바 영역을 반영하기 위한 함수
    public void HPBarChanged()
    {
        _hpBar.GetComponent<Slider>().value = _curHP / _brickHP;
    }

    // 벽돌이 비활성화될 때 HP바도 비활성화 처리
    void OnDisable()
    {
        if(_hpBar != null)
        {
            _hpBar.gameObject.SetActive(false);
        }
    }
}
