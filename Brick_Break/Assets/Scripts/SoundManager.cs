using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    private static SoundManager instance;

    public static SoundManager Instance
    {
        get
        {
            if (!instance)
            {
                GameObject obj = new GameObject("SoundManager");
                obj.AddComponent<SoundManager>();
                instance = obj.GetComponent<SoundManager>();
            }
            return instance;
        }
    }

    [SerializeField] AudioClip[] _audioClips;     // 효과음 오디오 소스 배열
    AudioSource _audioSource;

    void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }

        DontDestroyOnLoad(gameObject);
        _audioSource = GetComponent<AudioSource>();
    }

    // 타이틀 씬에서 넘어갈 때 재생
    public void OnClickStart()
    {
        _audioSource.PlayOneShot(_audioClips[0]);
    }


    // 벽돌이 비활성화 될 때 재생, 중복 재생 가능
    public void PlayBrickBreak()
    {
        _audioSource.PlayOneShot(_audioClips[1]);
    }

    public void PlayVictory()
    {
        _audioSource.PlayOneShot(_audioClips[2]);
    }

    public void PlayGameOver()
    {
        _audioSource.PlayOneShot(_audioClips[3]);
    }

    public void PlayEnding()
    {
        _audioSource.PlayOneShot(_audioClips[4]);
    }
}
