using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TitleManager : MonoBehaviour
{
    public void OnStartButtonClick()
    {
        SceneManager.LoadScene("Stage1"); // 이후 메인 스테이지 씬 이름
    }

    public void OnOptionsButtonClick()
    {
        Debug.Log("옵션 버튼 클릭됨 (옵션 패널은 추후 구현)");
        // 옵션 패널 UI 활성화 예정
    }

    public void OnQuitButtonClick()
    {
        Application.Quit();
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#endif
    }
}
