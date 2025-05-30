using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    // �̱��� ���� (��𼭵� ���� ���� ����)
    public static GameManager Instance { get; private set; }

    [Header("Player Health Setting")]
    public int maxHealth = 2;      // �ִ� ü��
    private int currentHealth;     // ���� ü��

    public PlayerController player; // �÷��̾� ���� (�ν����Ϳ� ����)

    void Awake()
    {
        // �̱��� �ʱ�ȭ
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject); // ���� �ٲ� �ı� �ȵ�
        }
        else
        {
            Destroy(gameObject);
            return;
        }
    }

    void Start()
    {
        // �ʱ� ü�� ����
        currentHealth = maxHealth;
    }

    // ========================
    // �� �÷��̾ �������� �Ծ��� �� ȣ���ϴ� �Լ�
    // ========================
    public void PlayerTakeDamage(int damage)
    {
        if (currentHealth <= 0) return;

        currentHealth -= damage;
        Debug.Log($"Player Health: {currentHealth}/{maxHealth}");

        if (currentHealth <= 0)
        {
            currentHealth = 0;
            // ���� �Ǵ� �� �÷��̾�� ���� ó�� ��û
            if (player != null)
                player.Die();
        }
        else
        {
            // ü�� ���������� ������ ȿ�� �� �߰� ó�� ����
        }
    }

    // ========================
    // �� �÷��̾� ��� ó��
    // ========================
    // ========================
    // �� �÷��̾� ��� �� ���ӿ��� ó��
    // ========================
    public void OnPlayerDead()
    {
        Debug.Log("GameManager: Player is Dead - Game Over");

        // TODO: ���ӿ��� UI ȣ��, �� ��ȯ �� ���� ��ü ó�� ���⼭ ����
    }

    // ========================
    // �� �÷��̾� ü�� �ʱ�ȭ (��: �������� ���� ��)
    // ========================
    public void ResetPlayerHealth()
    {
        currentHealth = maxHealth;
    }

    // ========================
    // �� ���� ü�� ��ȯ (�ʿ� ��)
    // ========================
    public int GetPlayerHealth()
    {
        return currentHealth;
    }
}
