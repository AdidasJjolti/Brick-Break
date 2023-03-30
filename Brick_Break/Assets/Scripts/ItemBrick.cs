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
        // �� ItemBrick�� InstanceID�� Ű������ �ϴ� ��ųʸ� ����
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

                // ������� ���� üũ ����
                if(newBall == null)
                {
                    Debug.LogError("Ball didn't instantiate.");
                    break;
                }

                newBall.transform.parent = null;

                // ������� ���� üũ ����
                Rigidbody2D ballRigid = newBall.GetComponent<Rigidbody2D>();
                if (ballRigid == null)
                {
                    Debug.LogError("Ball doesn't have Rigidbody2D.");
                    break;
                }

                // ������� ���� üũ ����
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

                BrickData.GetBrickData().NotifyObservers(gameObject.GetInstanceID(), true);                    // �� ItemBrick�� ������ ID���� �ҷ����� �Լ�
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
