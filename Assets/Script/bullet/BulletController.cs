using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletController : MonoBehaviour
{
    [Header("Bullet Settings")]
    public float speed = 10f;          // 총알 이동 속도
    public float lifeTime = 5f;        // 총알 생존 시간(초)
    public int damage = 1;             // 총알 피해량

    private float timer = 0f;

    void Update()
    {
        // 위 방향으로 이동
        transform.position += Vector3.up * speed * Time.deltaTime;

        timer += Time.deltaTime;
        if (timer > lifeTime)
        {
            Destroy(gameObject);
        }
    }

    // 적과 충돌 시 호출되는 함수 (Trigger 충돌)
    private void OnTriggerEnter2D(Collider2D other)
    {
        // 적 태그 체크
        if (other.CompareTag("Enemy"))
        {
            // 적의 EnemyController 스크립트 가져오기
            EnemyController enemy = other.GetComponent<EnemyController>();
            if (enemy != null)
            {
                enemy.TakeDamage(damage); // 피해 주기
            }

            // 총알 파괴
            Destroy(gameObject);
        }
    }
}
