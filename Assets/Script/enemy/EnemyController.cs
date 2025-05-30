using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    [Header("Enemy Settings")]
    [SerializeField] private int maxHealth = 5;
    [SerializeField] private float moveSpeed = 2f;
    [SerializeField] private float attackInterval = 2f;
    [SerializeField] private float detectionRadius = 5f;
    [SerializeField] private GameObject enemyBulletPrefab;
    [SerializeField] private Transform shotPoint;

    [SerializeField, Tooltip("최대 공격 횟수")]
    private int maxAttackCount = 2;   // 인스펙터에서 조절 가능

    private int currentHealth;
    private Transform playerTransform;
    private float attackTimer = 0f;
    private int currentAttackCount = 0;

    [SerializeField]
    private Transform[] waypoints;
    private int currentWaypointIndex = 0;

    void Start()
    {
        currentHealth = maxHealth;

        GameObject playerObj = GameObject.FindGameObjectWithTag("Player");
        if (playerObj != null)
            playerTransform = playerObj.transform;
    }

    void Update()
    {
        MovePattern();
        AttackWithDetection();
    }

    void MovePattern()
    {
        if (waypoints == null || waypoints.Length == 0) return;

        Transform target = waypoints[currentWaypointIndex];
        Vector3 dir = (target.position - transform.position);
        float distanceThisFrame = moveSpeed * Time.deltaTime;

        if (dir.magnitude <= distanceThisFrame)
        {
            transform.position = target.position;
            currentWaypointIndex = (currentWaypointIndex + 1) % waypoints.Length;
        }
        else
        {
            transform.position += dir.normalized * distanceThisFrame;
        }
    }

    void AttackWithDetection()
    {
        if (playerTransform == null) return;

        float distanceToPlayer = Vector3.Distance(transform.position, playerTransform.position);

        if (distanceToPlayer <= detectionRadius && currentAttackCount < maxAttackCount)
        {
            attackTimer += Time.deltaTime;
            if (attackTimer >= attackInterval)
            {
                ShootAtPlayer();
                attackTimer = 0f;
                currentAttackCount++;
            }
        }
        else
        {
            attackTimer = 0f;
        }
    }

    void ShootAtPlayer()
    {
        Vector3 direction = (playerTransform.position - shotPoint.position).normalized;

        GameObject bullet = Instantiate(enemyBulletPrefab, shotPoint.position, Quaternion.identity);

        EnemyBulletController bulletController = bullet.GetComponent<EnemyBulletController>();
        if (bulletController != null)
        {
            bulletController.SetDirection(direction);
        }

        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        bullet.transform.rotation = Quaternion.Euler(0, 0, angle - 90);
    }

    public void TakeDamage(int damage)
    {
        currentHealth -= damage;

        if (currentHealth <= 0)
            Die();
    }

    void Die()
    {
        // 적 죽음 처리 (이펙트, 점수 증가, 오브젝트 파괴 등)
        Destroy(gameObject);
    }
}
