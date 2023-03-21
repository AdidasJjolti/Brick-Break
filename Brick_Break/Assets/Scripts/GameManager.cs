using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    [SerializeField] int _ballCount;
    [SerializeField] int _brickCount;

    private static GameManager instance;

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
        SetBrickCount();
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

    public void ChangeBrickCount()
    {
        _brickCount--;
    }

    public void SetVictory()
    {
        if(_brickCount <=0)
        {
            Debug.Log("Clear!");
        }

        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }
}
