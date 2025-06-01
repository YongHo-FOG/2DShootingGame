using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BombUI : MonoBehaviour
{
    public GameObject bombIconPrefab; // ��ź ������ ������
    public Transform bombParent;      // �θ� ������Ʈ (HorizontalLayoutGroup ��� ����)

    private Image[] bombIcons;

    // ��ź ������ ����
    public void CreateBombIcons(int count)
    {
        bombIcons = new Image[count];

        for (int i = 0; i < count; i++)
        {
            GameObject icon = Instantiate(bombIconPrefab, bombParent);
            bombIcons[i] = icon.GetComponent<Image>();
        }
    }

    // ���� ��ź ���� ���� ������ ǥ�� ������Ʈ
    public void UpdateBombIcons(int currentCount)
    {
        for (int i = 0; i < bombIcons.Length; i++)
        {
            bombIcons[i].enabled = i < currentCount;
        }
    }
}

