using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using ObserverPattern;


public class ItemBrick : Brick
{
    Paddle _paddle;
    [SerializeField] GameObject _newBall;

    List<IObserver> _observers = new List<IObserver>();

    protected override void Start()
    {
        base.Start();       // Brick.cs의 Start 함수를 실행
        _paddle = FindObjectOfType<Paddle>();
    }

    void OnEnable()
    {
        // 각 ItemBrick의 InstanceID를 키값으로 하는 딕셔너리 생성
        BrickData.GetBrickData().InitDictionary(gameObject.GetInstanceID());

        if (_paddle == null)
        {
            _paddle = FindObjectOfType<Paddle>();
        }

    }

    void OnDisable()
    {
        // 게임 오버 후 첫번째 씬으로 이동 시 막대기가 없는 상황에서는 OnDisable 함수 실행하지 않음 : 게임 오버일 때 첫번재 씬으로 이동하면서 발생하는 사이드 이펙트 수정
        if (_paddle == null)
        {
            return;
        }

        // 게임 오버 상태일 때 발생하는 OnDisable 상황에서는 벽돌 파괴 로직을 실행하지 않음 : 게임 오버일 때 첫번재 씬으로 이동하면서 발생하는 사이드 이펙트 수정
        if(GameManager.Instance.GetGameOver() == true)
        {
            return;
        }

        switch (Type)
        {
            case eBrickType.PADDLE_LENGTHEN:
                _paddle.SendMessage("LengthenPaddle");
                break;
            case eBrickType.PADDLE_SHORTEN:
                _paddle.SendMessage("ShortenPaddle");
                break;
            case eBrickType.BONUS_BALL:
                GameObject newBall = Instantiate(_newBall, transform.position, Quaternion.identity);
                newBall.transform.name = "Second Ball";

                // 디버깅을 위한 체크 로직
                if(newBall == null)
                {
                    Debug.LogError("Ball didn't instantiate.");
                    break;
                }

                newBall.transform.parent = null;

                // 디버깅을 위한 체크 로직
                Rigidbody2D ballRigid = newBall.GetComponent<Rigidbody2D>();
                if (ballRigid == null)
                {
                    Debug.LogError("Ball doesn't have Rigidbody2D.");
                    break;
                }

                // 디버깅을 위한 체크 로직
                Ball ballBall = newBall.GetComponent<Ball>();
                if (ballBall == null)
                {
                    Debug.LogError("Ball doesn't have Ball.");
                    break;
                }

                ballRigid.velocity = Vector2.down * ballBall.GetSpeed();
                GameManager.Instance.AddBallCount();
                break;

            case eBrickType.MISSILE:

                BrickData.GetBrickData().NotifyObservers(gameObject.GetInstanceID(), true);                    // 각 ItemBrick의 고유한 ID값을 불러오는 함수
                _paddle.SendMessage("GetMissileCount");
                break;

            case eBrickType.LASER_HORIZONTAL:
                Ball[] horiballs = FindObjectsOfType<Ball>();          // 가로 레이저 적용할 볼의 배열
                for(int i = 0; i < horiballs.Length; i++)
                {
                    horiballs[i].SetHorizontalLaser();
                }
                _paddle.SendMessage("GetHorizontalLaserCount");
                break;
            case eBrickType.LASER_VERTICAL:
                Ball[] vertballs = FindObjectsOfType<Ball>();         // 세로 레이저 적용할 볼의 배열
                for (int i = 0; i < vertballs.Length; i++)
                {
                    vertballs[i].SetVerticalLaser();
                }
                _paddle.SendMessage("GetVerticalLaserCount");
                break;
        }
        DisableHPBar();
    }

    //public void RegisterObserver(IObserver observer)
    //{
    //    _observers.Add(observer);
    //}

    //public void RemoveObserver(IObserver observer)
    //{
    //    _observers.Remove(observer);
    //}

    //public void NotifyObservers()
    //{
    //    foreach(IObserver observer in _observers)
    //    {
    //        observer.SwitchOnTrigger();
    //    }
    //}
}
