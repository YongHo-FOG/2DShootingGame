using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    // �̱��� �ν��Ͻ� (���� ���ٿ�)
    public static GameManager Instance { get; private set; }

    [Header("Player Health Setting")]
    public int maxHealth = 2;           // �ִ� ü��
    private int currentHealth;          // ���� ü��

    [Header("Player Bomb Setting")]
    public int maxBombCount = 3;        // �ִ� ��ź ��
    private int currentBombCount;       // ���� ��ź ��

    [Header("References")]
    public PlayerController player;     // �÷��̾� ����
    public GameObject bombPrefab;       // ��ź ������
    public BombUI bombUI;               // ��ź UI ����
    public HealthUI healthUI;           // ü�� UI ����

    void Awake()
    {
        // �̱��� ����
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject); // ���� ��ȯ�Ǿ ����
        }
        else
        {
            Destroy(gameObject);
            return;
        }
    }

    void Start()
    {
        // �ʱ� ü��/��ź ����
        currentHealth = maxHealth;
        currentBombCount = maxBombCount;

        // UI ���� �� �ʱ�ȭ
        healthUI?.CreateHearts(maxHealth);
        bombUI?.CreateBombIcons(maxBombCount);
    }

    // ======================================
    // �� ü�� ���� ó�� ��
    // ======================================

    public void PlayerTakeDamage(int damage)
    {
        if (currentHealth <= 0) return;

        currentHealth -= damage;

        Debug.Log($"[GameManager] ü��: {currentHealth}/{maxHealth}");

        if (currentHealth <= 0)
        {
            currentHealth = 0;
            player?.Die(); // �÷��̾�� ���� ó�� ��û
        }

        // UI ����
        healthUI?.UpdateHearts(currentHealth);
    }

    public void ResetPlayerHealth()
    {
        currentHealth = maxHealth;
        healthUI?.UpdateHearts(currentHealth);
    }

    public int GetPlayerHealth() => currentHealth;

    public void OnPlayerDead()
    {
        Debug.Log("[GameManager] �÷��̾� ��� - ���ӿ��� ó�� ����");
        // TODO: ���ӿ��� UI ó��
    }

    // ======================================
    // �� ��ź ���� ó�� ��
    // ======================================

    public void TryUseBomb(Vector3 position)
    {
        if (currentBombCount <= 0) return;

        // ��ź ����
        Instantiate(bombPrefab, position, Quaternion.identity);

        // ���� ���� �� UI ����
        currentBombCount--;
        bombUI?.UpdateBombIcons(currentBombCount);
    }

    public void ResetBombCount()
    {
        currentBombCount = maxBombCount;
        bombUI?.UpdateBombIcons(currentBombCount);
    }

    public int GetCurrentBombCount() => currentBombCount;
}

