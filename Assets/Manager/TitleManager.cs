using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TitleManager : MonoBehaviour
{
    public void OnStartButtonClick()
    {
        SceneManager.LoadScene("Stage1"); // ���� ���� �������� �� �̸�
    }

    public void OnOptionsButtonClick()
    {
        Debug.Log("�ɼ� ��ư Ŭ���� (�ɼ� �г��� ���� ����)");
        // �ɼ� �г� UI Ȱ��ȭ ����
    }

    public void OnQuitButtonClick()
    {
        Application.Quit();
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#endif
    }
}
