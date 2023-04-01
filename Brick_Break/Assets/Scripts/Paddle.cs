using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;
using System;

public class Paddle : MonoBehaviour
{
    Rigidbody2D rigidbody;
    Camera cam;                // 메인 카메라
    float _minX = -2.5f;
    float _maxX = 2.5f;
    float _maxBounceAngle = 70f;

    float _scaleX;
    [SerializeField] uint _lengthenCount;         // 막대기 늘리기 효과 지속 횟수
    [SerializeField] uint _shortenCount;          // 막대기 줄이기 효과 지속 횟수
    [SerializeField] uint _missileCount;          // 미사일 효과 지속 횟수
    [SerializeField] uint _horLaserCount;         // 가로 레이저 효과 지속 횟수
    [SerializeField] uint _verLaserCount;         // 세로 레이저 효과 지속 횟수
    [SerializeField] Sprite[] _itemSprites;       // 0 : 막대기 확장, 1 : 막대기 축소, 2 : 미사일, 3: 가로 레이저, 4 : 세로 레이저
    [SerializeField] GameObject _objitemEffect;
    [SerializeField] GameObject _skillEffectUI;
    [SerializeField] List<(eBrickType type, GameObject obj)> _objList = new List<(eBrickType, GameObject)>();

    void Awake()
    {
        rigidbody = GetComponent<Rigidbody2D>();
        cam = Camera.main;
        _scaleX = this.transform.localScale.x;
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

    void OnCollisionEnter2D(Collision2D collision)
    {
        Ball ball = collision.gameObject.GetComponent<Ball>();

        if (ball != null)
        {
            Vector3 paddlePosition = transform.position;
            Vector2 contactPosition = collision.GetContact(0).point;

            float offset = paddlePosition.x - contactPosition.x;
            float width = collision.otherCollider.bounds.size.x / 2;             // otherCollider = 충돌해 들어오는 오브젝트의 상대편 충돌체

            float bounceAngle = (offset / width) * _maxBounceAngle;              // 패들 너비 대비 충돌 지점에 따라 반사각을 결정, 왼쪽(오른쪽)에서 맞으면 왼쪽(오른쪽)으로 각도 결정

            Quaternion rotation = Quaternion.AngleAxis(bounceAngle, Vector3.forward);
            ball.GetComponent<Rigidbody2D>().velocity = rotation * Vector2.up * ball.GetSpeed();


            // 횟수 제한이 있는 아이템 효과 카운트를 체크하는 함수, 효과 만료 시 원래 상태로 복구하는 기능
            CheckCount(ref _lengthenCount);
            CheckCount(ref _shortenCount);
            CheckMissileCount(ref _missileCount);
            CheckHorizontalLaserCount(ref _horLaserCount);
            CheckVerticalLaserCount(ref _verLaserCount);
            ChangeItemCount();         // 감소한 아이템 효과 횟수를 매개 변수로 전달

        }
    }

    void LengthenPaddle()
    {
        _lengthenCount = 50;
        this.transform.localScale = new Vector3(_scaleX * 1.5f, this.transform.localScale.y, this.transform.localScale.z);

        // 아이템 효과 UI 게임 오브젝트를 생성
        GameObject obj = Instantiate(_objitemEffect, _skillEffectUI.transform.position, Quaternion.identity, _skillEffectUI.transform);
        obj.transform.localScale = new Vector3(1, 1, 1);
        obj.transform.parent = _skillEffectUI.transform;
        _objList.Add((eBrickType.PADDLE_LENGTHEN, obj));
        obj.GetComponent<UIItem>().SetItemImage(_itemSprites[0]);
        obj.GetComponent<UIItem>().SetItemCount(_lengthenCount);
    }

    void ShortenPaddle()
    {
        _shortenCount = 10;
        this.transform.localScale = new Vector3(_scaleX * 0.5f, this.transform.localScale.y, this.transform.localScale.z);

        // 아이템 효과 UI 게임 오브젝트를 생성
        GameObject obj = Instantiate(_objitemEffect, _skillEffectUI.transform.position, Quaternion.identity, _skillEffectUI.transform);
        obj.transform.localScale = new Vector3(1, 1, 1);
        obj.transform.parent = _skillEffectUI.transform;
        _objList.Add((eBrickType.PADDLE_SHORTEN, obj));
        obj.GetComponent<UIItem>().SetItemImage(_itemSprites[1]);
        obj.GetComponent<UIItem>().SetItemCount(_shortenCount);
    }

    void GetMissileCount()
    {
        _missileCount = 15;

        // 아이템 효과 UI 게임 오브젝트를 생성
        GameObject obj = Instantiate(_objitemEffect, _skillEffectUI.transform.position, Quaternion.identity, _skillEffectUI.transform);
        obj.transform.localScale = new Vector3(1, 1, 1);
        obj.transform.parent = _skillEffectUI.transform;
        _objList.Add((eBrickType.MISSILE, obj));
        obj.GetComponent<UIItem>().SetItemImage(_itemSprites[2]);
        obj.GetComponent<UIItem>().SetItemCount(_missileCount);
    }

    void GetHorizontalLaserCount()
    {
        _horLaserCount = 13;

        // 아이템 효과 UI 게임 오브젝트를 생성
        GameObject obj = Instantiate(_objitemEffect, _skillEffectUI.transform.position, Quaternion.identity, _skillEffectUI.transform);
        obj.transform.localScale = new Vector3(1, 1, 1);
        obj.transform.parent = _skillEffectUI.transform;
        _objList.Add((eBrickType.LASER_HORIZONTAL, obj));
        obj.GetComponent<UIItem>().SetItemImage(_itemSprites[3]);
        obj.GetComponent<UIItem>().SetItemCount(_horLaserCount);
    }

    void GetVerticalLaserCount()
    {
        _verLaserCount = 17;

        // 아이템 효과 UI 게임 오브젝트를 생성
        GameObject obj = Instantiate(_objitemEffect, _skillEffectUI.transform.position, Quaternion.identity, _skillEffectUI.transform);
        obj.transform.localScale = new Vector3(1, 1, 1);
        obj.transform.parent = _skillEffectUI.transform;
        _objList.Add((eBrickType.LASER_VERTICAL, obj));
        obj.GetComponent<UIItem>().SetItemImage(_itemSprites[4]);
        obj.GetComponent<UIItem>().SetItemCount(_verLaserCount);
    }

    // lengthenCount, shortenCount를 체크할 공통 함수
    // ref를 활용하여 멤버 변수 각 카운트에 직접 접근
    void CheckCount(ref uint count)
    {
        if (count > 0)
        {
            // count : 볼이 패들에 충돌하여 감소한 아이템 효과 횟수
            count--;

            if (count == 0)
            {
                ResetSizeX();
            }
        }
    }

    void CheckMissileCount(ref uint count)
    {
        if (count > 0)
        {
            count--;


            if (count == 0)
            {
                BrickData.GetBrickData().NotifyObservers(-1, false);       // 키값이 -1일 때 isTrigger를 false로 만들어 줌
            }
        }
    }

    void CheckHorizontalLaserCount(ref uint count)
    {
        if (count > 0)
        {
            count--;


            if (count == 0)
            {
                ResetHorizontalLaser();
            }
        }
    }

    void CheckVerticalLaserCount(ref uint count)
    {
        if (count > 0)
        {
            count--;


            if (count == 0)
            {
                ResetVerticalalLaser();
            }
        }
    }

    // lengthenCount 또는 shortenCount가 0이 되었을 때 막대기 사이즈를 원복시킬 함수
    void ResetSizeX()
    {
        this.transform.localScale = new Vector3(_scaleX, this.transform.localScale.y, this.transform.localScale.z);
    }

    void ResetHorizontalLaser()
    {
        Ball[] balls = FindObjectsOfType<Ball>();
        for (int i = 0; i < balls.Length; i++)
        {
            balls[i].SetHorizontalLaser();
        }
    }

    void ResetVerticalalLaser()
    {
        Ball[] balls = FindObjectsOfType<Ball>();
        for (int i = 0; i < balls.Length; i++)
        {
            balls[i].SetVerticalLaser();
        }
    }

    // 아이템UI의 카운트를 변경할 공통 함수
    public void ChangeItemCount()
    {
        for(int i = 0; i < _objList.Count; i++)
        {
            if(_objList[i].obj == null)
            {
                continue;
            }

            uint tempCount = 0;

            switch(_objList[i].type)
            {
                case eBrickType.PADDLE_LENGTHEN:
                    tempCount = _lengthenCount;
                    _objList[i].obj.GetComponent<UIItem>().CheckItemCount(_lengthenCount);           // 전달 받은 남은 아이템 효과 횟수를 아이템 효과 UI에 다시 전달하여 UI에 표시
                    break;
                case eBrickType.PADDLE_SHORTEN:
                    tempCount = _shortenCount;
                    _objList[i].obj.GetComponent<UIItem>().CheckItemCount(_shortenCount);           // 전달 받은 남은 아이템 효과 횟수를 아이템 효과 UI에 다시 전달하여 UI에 표시
                    break;
                case eBrickType.MISSILE:
                    tempCount = _missileCount;
                    _objList[i].obj.GetComponent<UIItem>().CheckItemCount(_missileCount);           // 전달 받은 남은 아이템 효과 횟수를 아이템 효과 UI에 다시 전달하여 UI에 표시
                    break;
                case eBrickType.LASER_HORIZONTAL:
                    tempCount = _horLaserCount;
                    _objList[i].obj.GetComponent<UIItem>().CheckItemCount(_horLaserCount);           // 전달 받은 남은 아이템 효과 횟수를 아이템 효과 UI에 다시 전달하여 UI에 표시
                    break;
                case eBrickType.LASER_VERTICAL:
                    tempCount = _verLaserCount;
                    _objList[i].obj.GetComponent<UIItem>().CheckItemCount(_verLaserCount);           // 전달 받은 남은 아이템 효과 횟수를 아이템 효과 UI에 다시 전달하여 UI에 표시
                    break;
            }

            if (tempCount == 0)
            {
                //_objList.Remove(_objList[i]);
                //Destroy(_objList[i]);
                _objList[i].obj.SetActive(false);
            }
        }
    }

    public void ResetItemCounts()
    {
        _lengthenCount = 0;        // 막대기 늘리기 효과 지속 횟수
        _shortenCount = 0;         // 막대기 줄이기 효과 지속 횟수
        _missileCount = 0;         // 미사일 효과 지속 횟수
        _horLaserCount = 0;         // 가로 레이저 효과 지속 횟수
        _verLaserCount = 0;         // 세로 레이저 효과 지속 횟수
    }
}
