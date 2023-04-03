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

    [SerializeField] AudioClip[] _audioClips;     // ȿ���� ����� �ҽ� �迭
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

    // Ÿ��Ʋ ������ �Ѿ �� ���
    public void OnClickStart()
    {
        _audioSource.PlayOneShot(_audioClips[0]);
    }


    // ������ ��Ȱ��ȭ �� �� ���, �ߺ� ��� ����
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
