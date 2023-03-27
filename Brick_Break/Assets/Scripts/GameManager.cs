using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    [SerializeField] int _ballCount;
    [SerializeField] int _brickCount;

    private static GameManager instance;
    [SerializeField] GameObject obj;
    private static GameObject gameManager;
    [SerializeField] Ball _ball;

    public static GameManager Instance
    {
        get
        {
            if(!instance)
            {
                GameObject obj = new GameObject("GameManager");
                obj.AddComponent<GameManager>();
                instance = obj.GetComponent<GameManager>();
                instance.Init();
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
        Init();
    }

    public void Init()
    {
        _ball = FindObjectOfType<Ball>();
        SetBrickCount();          // ù��° �� �ε��� �� ���� ���� üũ
        _ball.SetFirstClick();
    }

    void Start()
    {
        _ballCount = 1;
    }

    void Update()
    {
        if(Input.GetKeyDown(KeyCode.R))
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }
        else if(Input.GetKeyDown(KeyCode.Alpha1))
        {
            SceneManager.LoadScene(0);
        }
        else if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            SceneManager.LoadScene(1);
        }
        else if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            SceneManager.LoadScene(2);
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
        //GameObject[] bricks = GameObject.FindGameObjectsWithTag("Brick");      
        _brickCount = Transform.FindObjectsOfType<Brick>(true).Length;
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
