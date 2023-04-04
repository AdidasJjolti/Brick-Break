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
        base.Start();       // Brick.cs�� Start �Լ��� ����
        _paddle = FindObjectOfType<Paddle>();
    }

    void OnEnable()
    {
        // �� ItemBrick�� InstanceID�� Ű������ �ϴ� ��ųʸ� ����
        BrickData.GetBrickData().InitDictionary(gameObject.GetInstanceID());

        if (_paddle == null)
        {
            _paddle = FindObjectOfType<Paddle>();
        }

    }

    void OnDisable()
    {
        // ���� ���� �� ù��° ������ �̵� �� ����Ⱑ ���� ��Ȳ������ OnDisable �Լ� �������� ���� : ���� ������ �� ù���� ������ �̵��ϸ鼭 �߻��ϴ� ���̵� ����Ʈ ����
        if (_paddle == null)
        {
            return;
        }

        // ���� ���� ������ �� �߻��ϴ� OnDisable ��Ȳ������ ���� �ı� ������ �������� ���� : ���� ������ �� ù���� ������ �̵��ϸ鼭 �߻��ϴ� ���̵� ����Ʈ ����
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

                BrickData.GetBrickData().NotifyObservers(gameObject.GetInstanceID(), true);                    // �� ItemBrick�� ������ ID���� �ҷ����� �Լ�
                _paddle.SendMessage("GetMissileCount");
                break;

            case eBrickType.LASER_HORIZONTAL:
                Ball[] horiballs = FindObjectsOfType<Ball>();          // ���� ������ ������ ���� �迭
                for(int i = 0; i < horiballs.Length; i++)
                {
                    horiballs[i].SetHorizontalLaser();
                }
                _paddle.SendMessage("GetHorizontalLaserCount");
                break;
            case eBrickType.LASER_VERTICAL:
                Ball[] vertballs = FindObjectsOfType<Ball>();         // ���� ������ ������ ���� �迭
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
