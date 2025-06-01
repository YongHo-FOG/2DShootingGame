using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    // 싱글톤 인스턴스 (전역 접근용)
    public static GameManager Instance { get; private set; }

    [Header("Player Health Setting")]
    public int maxHealth = 2;           // 최대 체력
    private int currentHealth;          // 현재 체력

    [Header("Player Bomb Setting")]
    public int maxBombCount = 3;        // 최대 폭탄 수
    private int currentBombCount;       // 현재 폭탄 수

    [Header("References")]
    public PlayerController player;     // 플레이어 참조
    public GameObject bombPrefab;       // 폭탄 프리팹
    public BombUI bombUI;               // 폭탄 UI 연결
    public HealthUI healthUI;           // 체력 UI 연결

    void Awake()
    {
        // 싱글톤 설정
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject); // 씬이 전환되어도 유지
        }
        else
        {
            Destroy(gameObject);
            return;
        }
    }

    void Start()
    {
        // 초기 체력/폭탄 설정
        currentHealth = maxHealth;
        currentBombCount = maxBombCount;

        // UI 생성 및 초기화
        healthUI?.CreateHearts(maxHealth);
        bombUI?.CreateBombIcons(maxBombCount);
    }

    // ======================================
    // ▼ 체력 관련 처리 ▼
    // ======================================

    public void PlayerTakeDamage(int damage)
    {
        if (currentHealth <= 0) return;

        currentHealth -= damage;

        Debug.Log($"[GameManager] 체력: {currentHealth}/{maxHealth}");

        if (currentHealth <= 0)
        {
            currentHealth = 0;
            player?.Die(); // 플레이어에게 죽음 처리 요청
        }

        // UI 갱신
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
        Debug.Log("[GameManager] 플레이어 사망 - 게임오버 처리 예정");
        // TODO: 게임오버 UI 처리
    }

    // ======================================
    // ▼ 폭탄 관련 처리 ▼
    // ======================================

    public void TryUseBomb(Vector3 position)
    {
        if (currentBombCount <= 0) return;

        // 폭탄 생성
        Instantiate(bombPrefab, position, Quaternion.identity);

        // 수량 감소 및 UI 갱신
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

