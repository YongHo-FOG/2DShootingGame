using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TitleFlowManager : MonoBehaviour
{
    public GameObject pressAnyButtonText;
    public GameObject menuPanel;
    private bool inputReceived = false;

    void Start()
    {
        pressAnyButtonText.SetActive(true);
        menuPanel.SetActive(false);
    }

    void Update()
    {
        if (!inputReceived && Input.anyKeyDown)
        {
            inputReceived = true;
            ShowMainMenu();
        }
    }

    void ShowMainMenu()
    {
        pressAnyButtonText.SetActive(false);
        menuPanel.SetActive(true);
    }
}
