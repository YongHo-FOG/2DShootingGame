using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BombUI : MonoBehaviour
{
    public GameObject bombIconPrefab; // 폭탄 아이콘 프리팹
    public Transform bombParent;      // 부모 오브젝트 (HorizontalLayoutGroup 사용 권장)

    private Image[] bombIcons;

    // 폭탄 아이콘 생성
    public void CreateBombIcons(int count)
    {
        bombIcons = new Image[count];

        for (int i = 0; i < count; i++)
        {
            GameObject icon = Instantiate(bombIconPrefab, bombParent);
            bombIcons[i] = icon.GetComponent<Image>();
        }
    }

    // 현재 폭탄 수에 맞춰 아이콘 표시 업데이트
    public void UpdateBombIcons(int currentCount)
    {
        for (int i = 0; i < bombIcons.Length; i++)
        {
            bombIcons[i].enabled = i < currentCount;
        }
    }
}

