using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    [SerializeField] int _ballCount;
    [SerializeField] int _brickCount;

    private static GameManager instance;
    [SerializeField] Ball _ball;

    public static GameManager Instance
    {
        get
        {
            if(!instance)
            {
                instance = FindObjectOfType<GameManager>();

                if(instance == null)
                {
                    return null;
                }
            }
            return instance;
        }
    }

    void Awake()
    {
        if(instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }

        DontDestroyOnLoad(gameObject);

    }

    void OnEnable()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
                _ball = FindObjectOfType<Ball>();
        SetBrickCount();          // ù��° �� �ε��� �� ���� ���� üũ
        _ball.SetFirstClick();
    }

    void Start()
    {
        _ballCount = 1;
        //SetBrickCount();          // ù��° �� �ε��� �� ���� ���� üũ
    }

    void Update()
    {
        if(Input.GetKeyDown(KeyCode.R))
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }
    }

    void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Ball"))
        {
            _ballCount--;

            if (_ballCount == 0)
            {
                collision.GetComponent<Rigidbody2D>().velocity = Vector2.zero;
                Debug.Log("Trigger Game Over");
            }
        }
    }

    // �� �ε��� �� ���� ������ ����� �Լ�
    public void SetBrickCount()
    {
        GameObject[] bricks = GameObject.FindGameObjectsWithTag("Brick");
        _brickCount = bricks.Length;
    }

    // Brick.cs���� ���� ��Ȱ��ȭ �� �� �޽����� �޾� ���� ī��Ʈ ����
    public void ChangeBrickCount()
    {
        _brickCount--;

        if(_brickCount <= 0)
        {
            SetVictory();
        }
    }

    public void SetVictory()
    {
        if(_brickCount <=0)
        {
            Debug.Log("Clear!");
        }

        // �������� Ŭ���� �� ���� �� �ҷ����� �ش� ���� �ִ� ���� ���� üũ
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        _ball.SetFirstClick();
        //Invoke("SetBrickCount", 1f);
    }
}
