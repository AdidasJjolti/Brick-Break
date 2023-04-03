using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class startScene : MonoBehaviour
{
    public void GoToFirstStage()
    {
        SoundManager.Instance.OnClickStart();
        Invoke("LoadFirstStage", 4f);
    }

    public void LoadFirstStage()
    {
        SceneManager.LoadScene(1);
    }
}
