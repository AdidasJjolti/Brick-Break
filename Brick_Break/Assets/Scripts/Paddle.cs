using UnityEngine;

public class Paddle : MonoBehaviour
{
    Rigidbody2D rigidbody;
    Camera cam;                // ���� ī�޶�
    float _minX = -2.5f;
    float _maxX = 2.5f;
    float _maxBounceAngle = 70f;

    float _scaleX;
    [SerializeField] uint _lengthenCount;        // ����� �ø��� ȿ�� ���� Ƚ��
    [SerializeField] uint _shortenCount;         // ����� ���̱� ȿ�� ���� Ƚ��
    [SerializeField] uint _missileCount;         // �̻��� ȿ�� ���� Ƚ��
    [SerializeField] uint _horLaserCount;         // ���� ������ ȿ�� ���� Ƚ��
    [SerializeField] uint _verLaserCount;         // ���� ������ ȿ�� ���� Ƚ��

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
        Vector2 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);         // ���� ���콺 ��ġ�� ���� ��ǥ�� ��ȯ
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
            float width = collision.otherCollider.bounds.size.x / 2;             // otherCollider = �浹�� ������ ������Ʈ�� ����� �浹ü

            float bounceAngle = (offset / width) * _maxBounceAngle;              // �е� �ʺ� ��� �浹 ������ ���� �ݻ簢�� ����, ����(������)���� ������ ����(������)���� ���� ����

            Quaternion rotation = Quaternion.AngleAxis(bounceAngle, Vector3.forward);
            ball.GetComponent<Rigidbody2D>().velocity = rotation * Vector2.up * ball.GetSpeed();


            // Ƚ�� ������ �ִ� ������ ȿ�� ī��Ʈ�� üũ�ϴ� �Լ�, ȿ�� ���� �� ���� ���·� �����ϴ� ���
            CheckCount(ref _lengthenCount);
            CheckCount(ref _shortenCount);
            CheckMissileCount(ref _missileCount);
            CheckHorizontalLaserCount(ref _horLaserCount);
            CheckVerticalLaserCount(ref _verLaserCount);
        }
    }

    void LengthenPaddle()
    {
        _lengthenCount = 2;
        this.transform.localScale = new Vector3(_scaleX * 1.5f, this.transform.localScale.y, this.transform.localScale.z);
    }

    void ShortenPaddle()
    {
        _shortenCount = 2;
        this.transform.localScale = new Vector3(_scaleX * 0.5f, this.transform.localScale.y, this.transform.localScale.z);
    }

    void GetMissileCount()
    {
        _missileCount = 2;
    }

    void GetHorizontalLaserCount()
    {
        _horLaserCount = 2;
    }

    void GetVerticalLaserCount()
    {
        _verLaserCount = 20;
    }

    // lengthenCount, shortenCount�� üũ�� ���� �Լ�
    // ref�� Ȱ���Ͽ� ��� ���� �� ī��Ʈ�� ���� ����
    void CheckCount(ref uint count)
    {
        if (count > 0)
        {
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
                BrickData.GetBrickData().NotifyObservers(-1, false);       // Ű���� -1�� �� isTrigger�� false�� ����� ��
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

    // lengthenCount �Ǵ� shortenCount�� 0�� �Ǿ��� �� ����� ����� ������ų �Լ�
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

    public void ResetItemCounts()
    {
        _lengthenCount = 0;        // ����� �ø��� ȿ�� ���� Ƚ��
        _shortenCount = 0;         // ����� ���̱� ȿ�� ���� Ƚ��
        _missileCount = 0;         // �̻��� ȿ�� ���� Ƚ��
        _horLaserCount = 0;         // ���� ������ ȿ�� ���� Ƚ��
        _verLaserCount = 0;         // ���� ������ ȿ�� ���� Ƚ��
    }
}
