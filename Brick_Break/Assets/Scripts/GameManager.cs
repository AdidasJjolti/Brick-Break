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
        SetBrickCount();          // 첫번째 씬 로드할 때 벽돌 갯수 체크
        _ball.SetFirstClick();
    }

    void Start()
    {
        _ballCount = 1;
        //SetBrickCount();          // 첫번째 씬 로드할 때 벽돌 갯수 체크
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

    // 씬 로드할 때 벽돌 갯수를 계산할 함수
    public void SetBrickCount()
    {
        GameObject[] bricks = GameObject.FindGameObjectsWithTag("Brick");
        _brickCount = bricks.Length;
    }

    // Brick.cs에서 벽돌 비활성화 할 때 메시지를 받아 벽돌 카운트 차감
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

        // 스테이지 클리어 시 다음 씬 불러오고 해당 씬에 있는 벽돌 갯수 체크
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        _ball.SetFirstClick();
        //Invoke("SetBrickCount", 1f);
    }
}
