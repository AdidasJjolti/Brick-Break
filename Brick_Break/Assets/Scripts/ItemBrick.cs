using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using ObserverPattern;


public class ItemBrick : Brick
{
    Paddle _paddle;
    Brick[] _bricks;
    [SerializeField] GameObject _newBall;

    List<IObserver> _observers = new List<IObserver>();

    void Start()
    {
        _paddle = FindObjectOfType<Paddle>();
        _bricks = FindObjectsOfType<Brick>();
    }

    void OnEnable()
    {
        // 각 ItemBrick의 InstanceID를 키값으로 하는 딕셔너리 생성
        BrickData.GetBrickData().InitDictionary(gameObject.GetInstanceID());
    }

    void OnDisable()
    {
        switch(Type)
        {
            case eBrickType.PADDLE_LENGTHEN:
                _paddle.SendMessage("LengthenPaddle");
                break;
            case eBrickType.PADDLE_SHORTEN:
                _paddle.SendMessage("ShortenPaddle");
                break;
            case eBrickType.BONUS_BALL:
                GameObject newBall = Instantiate(_newBall, transform.position, Quaternion.identity);

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
                //for (int i = 0; i < _bricks.Length; i++)
                //{
                //    _bricks[i].SendMessage("SwitchOnTrigger");
                //}

                BrickData.GetBrickData().NotifyObservers(gameObject.GetInstanceID(), true);                    // 각 ItemBrick의 고유한 ID값을 불러오는 함수
                _paddle.SendMessage("GetMissileCount");
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
