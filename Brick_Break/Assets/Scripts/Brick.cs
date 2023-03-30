using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using ObserverPattern;

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

public class Brick : MonoBehaviour, IObserver
{
    [SerializeField] eBrickType type;
    public eBrickType Type
    {
        get
        {
            return type;
        }
    }

    [SerializeField] float _brickHP;
    [SerializeField] float _curHP;
    BoxCollider2D _collider;
    public Slider _hpBar;

    void Awake()
    {
        _collider = GetComponent<BoxCollider2D>();
    }

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
            BreakBrick();
        }
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Ball"))
        {
            BreakBrick();
        }
    }

    public void SetHPBar(GameObject hpBar)
    {
        _hpBar = hpBar.GetComponent<Slider>();
    }


    // ������ �ǰݴ��� ������ HP�� ������ �ݿ��ϱ� ���� �Լ�
    public void HPBarChanged()
    {
        _hpBar.GetComponent<Slider>().value = _curHP / _brickHP;
    }

    // ������ ��Ȱ��ȭ�� �� HP�ٵ� ��Ȱ��ȭ ó��
    void OnDisable()
    {
        DisableHPBar();
    }

    // ���� ���⿡ �������� ����� �ڵ带 �Լ��� ����� : OnCollisionEnter2D, OnTriggerEnter2D�� ���
    public void BreakBrick()
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

    public void DisableHPBar()
    {
        if (_hpBar != null)
        {
            _hpBar.gameObject.SetActive(false);
        }
    }

    public void SwitchOnTrigger(bool isTrigger)
    {
        _collider.isTrigger = isTrigger;
    }
}
