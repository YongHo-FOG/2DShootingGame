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

    [Header("Shooting")]
    public GameObject bulletPrefab;     // 총알 프리팹
    public float fireInterval = 2f;     // 발사 간격
    private float fireTimer;

    private Transform player;           // 플레이어 위치 추적용

    void Start()
    {
        currentHealth = maxHealth;
        fireTimer = 0f;
        player = GameObject.FindGameObjectWithTag("Player")?.transform;
    }

    void Update()
    {
        Move();

        fireTimer += Time.deltaTime;
        if (fireTimer >= fireInterval)
        {
            FireAtPlayer();
            fireTimer = 0f;
        }
    }

    void Move()
    {
        transform.Translate(Vector3.down * speed * Time.deltaTime);
    }

    void FireAtPlayer()
    {
        if (player == null || bulletPrefab == null) return;

        GameObject bullet = Instantiate(bulletPrefab, transform.position, Quaternion.identity);

        Vector3 direction = (player.position - transform.position).normalized;

        EnemyBulletController bulletCtrl = bullet.GetComponent<EnemyBulletController>();
        if (bulletCtrl != null)
        {
            bulletCtrl.SetDirection(direction);
        }
    }

    public void TakeDamage(int amount)
    {
        currentHealth -= amount;
        if (currentHealth <= 0)
            Destroy(gameObject);
    }
}
