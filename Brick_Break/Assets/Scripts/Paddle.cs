using UnityEngine;

public class Paddle : MonoBehaviour
{
    Rigidbody2D rigidbody;
    Camera cam;                // 메인 카메라
    float _minX = -2.5f;
    float _maxX = 2.5f;
    float _maxBounceAngle = 70f;

    float _scaleX;
    [SerializeField] uint _lengthenCount;        // 막대기 늘리기 효과 지속 횟수
    [SerializeField] uint _shortenCount;         // 막대기 줄이기 효과 지속 횟수
    [SerializeField] uint _missileCount;         // 미사일 효과 지속 횟수
    [SerializeField] uint _horLaserCount;         // 가로 레이저 효과 지속 횟수
    [SerializeField] uint _verLaserCount;         // 세로 레이저 효과 지속 횟수

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

    // lengthenCount, shortenCount를 체크할 공통 함수
    // ref를 활용하여 멤버 변수 각 카운트에 직접 접근
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

    public void ResetItemCounts()
    {
        _lengthenCount = 0;        // 막대기 늘리기 효과 지속 횟수
        _shortenCount = 0;         // 막대기 줄이기 효과 지속 횟수
        _missileCount = 0;         // 미사일 효과 지속 횟수
        _horLaserCount = 0;         // 가로 레이저 효과 지속 횟수
        _verLaserCount = 0;         // 세로 레이저 효과 지속 횟수
    }
}
