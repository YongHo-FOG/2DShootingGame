using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthUI : MonoBehaviour
{
    public GameObject heartPrefab; // ��Ʈ �̹��� ������
    public Transform heartParent;  // �θ� ������Ʈ (HorizontalLayoutGroup ��� ����)

    private Image[] hearts;

    // ��Ʈ �̹��� ����
    public void CreateHearts(int count)
    {
        hearts = new Image[count];

        for (int i = 0; i < count; i++)
        {
            GameObject heart = Instantiate(heartPrefab, heartParent);
            hearts[i] = heart.GetComponent<Image>();
        }
    }

    // ���� ü�¿� ���� ��Ʈ ǥ�� ������Ʈ
    public void UpdateHearts(int currentHealth)
    {
        for (int i = 0; i < hearts.Length; i++)
        {
            hearts[i].enabled = i < currentHealth;
        }
    }
}

