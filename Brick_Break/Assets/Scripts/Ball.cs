using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ball : MonoBehaviour
{
    Rigidbody2D rigid;
    [SerializeField] float _speed;
    [SerializeField] float _degree;
    public bool _isFirstClick = false;
    Vector2 _lastVelocity;               // �浹 ���� ���� ���͸� �����ϴ� ��� ����, OnCollisionEnter2D �Ǵ� OnTriggerEnter2D���� üũ �� �ӵ��� 0�� �Ǳ� ����

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
        // �� �ε� �� ù ���콺 Ŭ�� ����
        FirstMove();
        // ���� �ӵ��� �����ϰ� �����ϱ� ���� ���� �Լ�
        _lastVelocity = rigid.velocity;
        if (_lastVelocity.magnitude < _speed)
        {
            _lastVelocity = _lastVelocity.normalized * _speed;
        }

        //���� ���� �õ��� ����
        //Debug.Log(rigid.velocity);

        //if (_degree < 10)
        //{
        //    _degree = 10;
        //    float curSpeed = _lastVelocity.magnitude;
        //    var dir = (Vector2)(Quaternion.Euler(0, 0, _degree) * Vector2.up);
        //    rigid.velocity = dir * Mathf.Max(curSpeed, 0f);        // �� ���� ���� �ӵ��� �ݻ簢 ������ �������� ���ǵ常ŭ ����
        //}
        //else if (_degree > 170)
        //{
        //    _degree = 170;
        //    float curSpeed = _lastVelocity.magnitude;
        //    var dir = (Vector2)(Quaternion.Euler(0, 0, _degree) * Vector2.up);
        //    rigid.velocity = dir * Mathf.Max(curSpeed, 0f);        // �� ���� ���� �ӵ��� �ݻ簢 ������ �������� ���ǵ常ŭ ����
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

            // ������ ũ�⸸ŭ ���������� ����
            float curSpeed = _lastVelocity.magnitude;
            // �浹 ��� �ӵ��� ũ�⸸ŭ �ݻ簢 ����, normalize�Ͽ� ������ ũ�⸦ 1�� ����
            Vector2 dir = Vector2.Reflect(_lastVelocity.normalized, collision.contacts[0].normal);
            // �� ���� ���� �ӵ��� �ݻ簢 ������ �������� ���ǵ常ŭ ����
            rigid.velocity = dir * Mathf.Max(curSpeed, 0f);


            // �� ���� ���� �õ��� ����
            //Vector2 v2 = Vector2.up - dir;
            //float degree = Mathf.Atan2(v2.y, v2.x) * Mathf.Rad2Deg;
            //_degree = degree;
            //Debug.Log("Degree is " + degree);
        }
    }

    // ���� �ҷ��� �� _isFirstClick�� false�� ������ �Լ�, GameManager���� ������ �ε��� �� �޽����� �޾� ȣ���
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
