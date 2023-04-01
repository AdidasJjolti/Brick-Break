using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ball : MonoBehaviour
{
    Rigidbody2D rigid;
    [SerializeField] float _speed;
    [SerializeField] float _degree;
    public bool _isFirstClick = false;
    Vector2 _lastVelocity;               // 충돌 직전 볼의 벡터를 저장하는 멤버 변수, OnCollisionEnter2D 또는 OnTriggerEnter2D에서 체크 시 속도가 0이 되기 때문

    public bool _horizontalLaser;
    public bool _verticalLaser;
    //RaycastHit2D[] _rightHits;
    //RaycastHit2D[] _leftHits;
    //RaycastHit2D[] _upHits;
    //RaycastHit2D[] _downHits;


    void Awake()
    {
        rigid = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        // 씬 로드 후 첫 마우스 클릭 감지
        FirstMove();
        // 공의 속도를 일정하게 고정하기 위한 보정 함수
        _lastVelocity = rigid.velocity;
        if (_lastVelocity.magnitude < _speed)
        {
            _lastVelocity = _lastVelocity.normalized * _speed;
        }

        //각도 보정 시도의 흔적
        //Debug.Log(rigid.velocity);

        //if (_degree < 10)
        //{
        //    _degree = 10;
        //    float curSpeed = _lastVelocity.magnitude;
        //    var dir = (Vector2)(Quaternion.Euler(0, 0, _degree) * Vector2.up);
        //    rigid.velocity = dir * Mathf.Max(curSpeed, 0f);        // 그 다음 볼의 속도를 반사각 벡터의 방향으로 스피드만큼 곱함
        //}
        //else if (_degree > 170)
        //{
        //    _degree = 170;
        //    float curSpeed = _lastVelocity.magnitude;
        //    var dir = (Vector2)(Quaternion.Euler(0, 0, _degree) * Vector2.up);
        //    rigid.velocity = dir * Mathf.Max(curSpeed, 0f);        // 그 다음 볼의 속도를 반사각 벡터의 방향으로 스피드만큼 곱함
        //}
    }


    void FirstMove()
    {
        if(Input.GetMouseButtonDown(0) && _isFirstClick == false)
        {
            _isFirstClick = true;
            gameObject.transform.parent = null;
            rigid.velocity = Vector2.up * _speed;

            BrickData.GetBrickData().AddDictionary();
        }
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Wall") || collision.gameObject.CompareTag("Brick"))
        {
            if(collision.gameObject.CompareTag("Brick") && _horizontalLaser == true)
            {
                Transform colTransform = collision.transform;
                Debug.DrawRay(colTransform.position, colTransform.right * 20, Color.green, 1f);
                Debug.DrawRay(colTransform.position, colTransform.right * -20, Color.green, 1f);
                RaycastHit2D[] rightHits = Physics2D.RaycastAll(colTransform.position, colTransform.right * 20);
                RaycastHit2D[] leftHits = Physics2D.RaycastAll(colTransform.position, colTransform.right * -20);

                if(rightHits.Length != 0)
                {
                    for (int i = 0; i < rightHits.Length; i++)
                    {
                        RaycastHit2D hit = rightHits[i];
                        //Debug.Log(hit.transform.name);
                        if(hit.transform.CompareTag("Wall"))
                        {
                            continue;
                        }

                        if(hit.transform.GetComponent<Brick>() != null)
                        {
                            hit.transform.GetComponent<Brick>().BreakBrick();
                        }
                        else
                        {
                            Debug.LogError($"Doesn't have Brick. : {hit.transform.name}");
                            break;
                        }
                    }
                }

                if(leftHits.Length != 0)
                {
                    for (int i = 0; i < leftHits.Length; i++)
                    {
                        RaycastHit2D hit = leftHits[i];
                        //Debug.Log(hit.transform.name);
                        if (hit.transform.CompareTag("Wall"))
                        {
                            continue;
                        }

                        if (hit.transform.GetComponent<Brick>() != null)
                        {
                            hit.transform.GetComponent<Brick>().BreakBrick();
                        }
                        else
                        {
                            Debug.LogError($"Doesn't have Brick. : {hit.transform.name}");
                            break;
                        }
                    }
                }
            }

            if (collision.gameObject.CompareTag("Brick") && _verticalLaser == true)
            {
                Transform colTransform = collision.transform;
                Debug.DrawRay(colTransform.position, colTransform.up * 20, Color.green, 1f);
                Debug.DrawRay(colTransform.position, colTransform.up * -20, Color.green, 1f);
                RaycastHit2D[] upHits = Physics2D.RaycastAll(colTransform.position, colTransform.up * 20);
                RaycastHit2D[] downHits = Physics2D.RaycastAll(colTransform.position, colTransform.up * -20);

                if (upHits.Length != 0)
                {
                    for (int i = 0; i < upHits.Length; i++)
                    {
                        RaycastHit2D hit = upHits[i];
                        //Debug.Log(hit.transform.name);
                        if (hit.transform.CompareTag("Brick"))
                        {
                            if (hit.transform.GetComponent<Brick>() == null)
                            {
                                Debug.LogError("Doesn't have Brick.");
                                break;
                            }

                            hit.transform.GetComponent<Brick>().BreakBrick();
                        }
                        else
                        {
                            continue;
                        }
                    }
                }

                if (downHits.Length != 0)
                {
                    for (int i = 0; i < downHits.Length; i++)
                    {
                        RaycastHit2D hit = downHits[i];
                        //Debug.Log(hit.transform.name);
                        if (hit.transform.CompareTag("Brick"))
                        {
                            if (hit.transform.GetComponent<Brick>() == null)
                            {
                                Debug.LogError("Doesn't have Brick.");
                                break;
                            }

                            hit.transform.GetComponent<Brick>().BreakBrick();
                        }
                        else
                        {
                            continue;
                        }
                    }
                }
            }

            // 벡터의 크기만큼 지역변수로 저장
            float curSpeed = _lastVelocity.magnitude;
            // 충돌 당시 속도의 크기만큼 반사각 결정, normalize하여 벡터의 크기를 1로 설정
            Vector2 dir = Vector2.Reflect(_lastVelocity.normalized, collision.contacts[0].normal);
            // 그 다음 볼의 속도를 반사각 벡터의 방향으로 스피드만큼 곱함
            rigid.velocity = dir * Mathf.Max(curSpeed, 0f);


            // 볼 각도 보정 시도의 흔적
            //Vector2 v2 = Vector2.up - dir;
            //float degree = Mathf.Atan2(v2.y, v2.x) * Mathf.Rad2Deg;
            //_degree = degree;
            //Debug.Log("Degree is " + degree);
        }
    }

    // 씬을 불러올 때 _isFirstClick을 false로 세팅할 함수, GameManager에서 다음씬 로드할 때 메시지를 받아 호출됨
    public void SetFirstClick()
    {
        _isFirstClick = false;
    }

    public float GetSpeed()
    {
        return _speed;
    }

    public void SetHorizontalLaser()
    {
        _horizontalLaser = !_horizontalLaser;
    }

    public void SetVerticalLaser()
    {
        _verticalLaser = !_verticalLaser;
    }
}
