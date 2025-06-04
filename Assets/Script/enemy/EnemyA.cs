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
