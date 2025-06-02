using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// EnemyA: ���� �⺻���� �� ����
/// - �Ʒ��� ���� �̵�
/// - ���� �ð� �������� �÷��̾ ���� �Ѿ� �߻�
/// </summary>
public class EnemyA : MonoBehaviour
{
    [Header("Movement")]
    public float speed = 3f;

    [Header("Health")]
    public int maxHealth = 3;
    private int currentHealth;

    [Header("Shooting")]
    public GameObject bulletPrefab;     // �Ѿ� ������
    public float fireInterval = 2f;     // �߻� ����
    private float fireTimer;

    private Transform player;           // �÷��̾� ��ġ ������

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
