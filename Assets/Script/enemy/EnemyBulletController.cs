using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 적 총알 - 지정된 방향으로 직선 이동 + 충돌 처리 + 화면 밖 제거
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

        Destroy(gameObject, lifetime); // 자동 제거
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
