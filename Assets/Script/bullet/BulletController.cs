using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletController : MonoBehaviour
{
    [Header("Bullet Settings")]
    public float speed = 10f;          // �Ѿ� �̵� �ӵ�
    public float lifeTime = 5f;        // �Ѿ� ���� �ð�(��)
    public int damage = 1;             // �Ѿ� ���ط�

    private float timer = 0f;

    void Update()
    {
        // �� �������� �̵�
        transform.position += Vector3.up * speed * Time.deltaTime;

        timer += Time.deltaTime;
        if (timer > lifeTime)
        {
            Destroy(gameObject);
        }
    }

    // ���� �浹 �� ȣ��Ǵ� �Լ� (Trigger �浹)
    private void OnTriggerEnter2D(Collider2D other)
    {
        // �� �±� üũ
        if (other.CompareTag("Enemy"))
        {
            // ���� EnemyController ��ũ��Ʈ ��������
            EnemyController enemy = other.GetComponent<EnemyController>();
            if (enemy != null)
            {
                enemy.TakeDamage(damage); // ���� �ֱ�
            }

            // �Ѿ� �ı�
            Destroy(gameObject);
        }
    }
}
