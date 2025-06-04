using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// EnemyA: 가장 기본적인 적 유닛
/// - 아래로 직선 이동
/// - 일정 시간 간격으로 플레이어를 향해 총알 발사
/// </summary>
public class EnemyA : MonoBehaviour
{
    [Header("Movement")]
    public float speed = 3f;

    [Header("Health")]
    public int maxHealth = 3;
    private int currentHealth;

    void Start()
    {
        currentHealth = maxHealth;
    }

    void Update()
    {
        Move();
    }

    void Move()
    {
        transform.Translate(Vector3.down * speed * Time.deltaTime);
    }

    public void TakeDamage(int amount)
    {
        currentHealth -= amount;
        if (currentHealth <= 0)
            Destroy(gameObject);
    }
}
