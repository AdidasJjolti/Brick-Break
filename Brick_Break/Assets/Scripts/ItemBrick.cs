using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ItemBrick : Brick
{
    //public override void SetHPBar(GameObject hpBar)
    //{
    //    _hpBar = hpBar.GetComponent<Slider>();
    //}

    Paddle _paddle;
    [SerializeField] GameObject _newBall;

    void Start()
    {
        _paddle = FindObjectOfType<Paddle>();
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
        }
        DisableHPBar();
    }
}
