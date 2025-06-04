using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StageClearUI : MonoBehaviour
{
    public void OnNextStagePressed()
    {
        Debug.Log("스테이지 2 넘어감 (미구현)");
        //SceneManager.LoadScene("Stage2"); // 스테이지 이름에 맞게 수정
    }

    public void OnTitlePressed()
    {
        SceneManager.LoadScene("TitleScenes"); // 타이틀 씬 이름
    }
}
