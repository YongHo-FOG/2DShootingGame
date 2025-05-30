using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    // 싱글톤 패턴 (어디서든 쉽게 접근 가능)
    public static GameManager Instance { get; private set; }

    [Header("Player Health Setting")]
    public int maxHealth = 2;      // 최대 체력
    private int currentHealth;     // 현재 체력

    public PlayerController player; // 플레이어 참조 (인스펙터에 연결)

    void Awake()
    {
        // 싱글톤 초기화
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject); // 씬이 바뀌어도 파괴 안됨
        }
        else
        {
            Destroy(gameObject);
            return;
        }
    }

    void Start()
    {
        // 초기 체력 세팅
        currentHealth = maxHealth;
    }

    // ========================
    // ■ 플레이어가 데미지를 입었을 때 호출하는 함수
    // ========================
    public void PlayerTakeDamage(int damage)
    {
        if (currentHealth <= 0) return;

        currentHealth -= damage;
        Debug.Log($"Player Health: {currentHealth}/{maxHealth}");

        if (currentHealth <= 0)
        {
            currentHealth = 0;
            // 죽음 판단 후 플레이어에게 죽음 처리 요청
            if (player != null)
                player.Die();
        }
        else
        {
            // 체력 남아있으면 데미지 효과 등 추가 처리 가능
        }
    }

    // ========================
    // ■ 플레이어 사망 처리
    // ========================
    // ========================
    // ■ 플레이어 사망 후 게임오버 처리
    // ========================
    public void OnPlayerDead()
    {
        Debug.Log("GameManager: Player is Dead - Game Over");

        // TODO: 게임오버 UI 호출, 씬 전환 등 게임 전체 처리 여기서 진행
    }

    // ========================
    // ■ 플레이어 체력 초기화 (예: 스테이지 시작 시)
    // ========================
    public void ResetPlayerHealth()
    {
        currentHealth = maxHealth;
    }

    // ========================
    // ■ 현재 체력 반환 (필요 시)
    // ========================
    public int GetPlayerHealth()
    {
        return currentHealth;
    }
}
