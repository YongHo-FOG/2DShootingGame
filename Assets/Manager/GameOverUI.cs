using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOverUI : MonoBehaviour
{
    public void OnRestartPressed()
    {
        SceneManager.LoadScene("Stage1"); // �������� �̸��� �°� ����
    }

    public void OnTitlePressed()
    {
        SceneManager.LoadScene("TitleScenes"); // Ÿ��Ʋ �� �̸�
    }
}
