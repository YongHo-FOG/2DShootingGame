using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// �� �Ѿ� - ������ �������� ���� �̵� + �浹 ó�� + ȭ�� �� ����
/// </summary>
public class EnemyBulletController : MonoBehaviour
{
    public float speed = 5f;
    public float lifetime = 5f;

    private Vector2 direction;
    private Rigidbody2D rb;

    public void SetDirection(Vector2 dir)
    {
        direction = dir.normalized;
    }

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        rb.velocity = direction * speed;

        Destroy(gameObject, lifetime); // �ڵ� ����
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            GameManager.Instance.PlayerTakeDamage(1);
            Destroy(gameObject);
        }
    }
}
