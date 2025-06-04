using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOverUI : MonoBehaviour
{
    public void OnRestartPressed()
    {
        SceneManager.LoadScene("Stage1"); // 스테이지 이름에 맞게 수정
    }

    public void OnTitlePressed()
    {
        SceneManager.LoadScene("TitleScenes"); // 타이틀 씬 이름
    }
}
