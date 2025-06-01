using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthUI : MonoBehaviour
{
    public GameObject heartPrefab; // 하트 이미지 프리팹
    public Transform heartParent;  // 부모 오브젝트 (HorizontalLayoutGroup 사용 권장)

    private Image[] hearts;

    // 하트 이미지 생성
    public void CreateHearts(int count)
    {
        hearts = new Image[count];

        for (int i = 0; i < count; i++)
        {
            GameObject heart = Instantiate(heartPrefab, heartParent);
            hearts[i] = heart.GetComponent<Image>();
        }
    }

    // 현재 체력에 맞춰 하트 표시 업데이트
    public void UpdateHearts(int currentHealth)
    {
        for (int i = 0; i < hearts.Length; i++)
        {
            hearts[i].enabled = i < currentHealth;
        }
    }
}

