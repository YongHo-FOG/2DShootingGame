using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StageClearUI : MonoBehaviour
{
    public void OnNextStagePressed()
    {
        Debug.Log("�������� 2 �Ѿ (�̱���)");
        //SceneManager.LoadScene("Stage2"); // �������� �̸��� �°� ����
    }

    public void OnTitlePressed()
    {
        SceneManager.LoadScene("TitleScenes"); // Ÿ��Ʋ �� �̸�
    }
}
