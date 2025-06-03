using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI; // Text 사용 시
using TMPro; // TextMeshPro 사용 시

public class PressAnyButtonBlink : MonoBehaviour
{
    public float speed = 2f;

    private Text uiText;
    private TextMeshProUGUI tmpText;
    private bool useTMP = false;

    void Start()
    {
        uiText = GetComponent<Text>();
        tmpText = GetComponent<TextMeshProUGUI>();
        useTMP = tmpText != null;
    }

    void Update()
    {
        float alpha = (Mathf.Sin(Time.time * speed) + 1f) / 2f; // 0~1 사이 반복

        if (useTMP)
        {
            Color color = tmpText.color;
            color.a = alpha;
            tmpText.color = color;
        }
        else if (uiText != null)
        {
            Color color = uiText.color;
            color.a = alpha;
            uiText.color = color;
        }
    }
}
