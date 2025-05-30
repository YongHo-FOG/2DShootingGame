using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBulletController : MonoBehaviour
{
    [Header("Bullet Settings")]
    [SerializeField] private float speed = 7f;      // �Ѿ� �̵� �ӵ�
    [SerializeField] private float lifeTime = 5f;   // �Ѿ��� ������ �ִ� �ð�(��)

    private Vector3 moveDirection;   // �Ѿ��� ������ ���� (����ȭ�� ����)
    private float timer = 0f;        // �Ѿ� ���� �� ��� �ð� üũ��

    /// <summary>
    /// �Ѿ��� �̵� ������ �ʱ�ȭ�ϴ� �Լ�
    /// ������(����)�� �Ѿ��� �߻��� �� ȣ����
    /// </summary>
    /// <param name="direction">�߻� ���� ����</param>
    public void SetDirection(Vector3 direction)
    {
        moveDirection = direction.normalized; // ������ �������ͷ� ��ȯ�ؼ� ����
    }

    void Update()
    {
        // �Ѿ� ��ġ�� �� �����Ӹ��� �̵� ����� �ӵ��� ���� ����
        transform.position += moveDirection * speed * Time.deltaTime;

        // �Ѿ��� ���� �ð��� ����
        timer += Time.deltaTime;

        // ������ ���� �ð��� ������ �Ѿ� ������Ʈ �ı�
        if (timer > lifeTime)
        {
            Destroy(gameObject);
        }
    }

    /// <summary>
    /// �浹 ���� ó�� �Լ� (2D ���� �浹)
    /// �浹�� �ݶ��̴��� "Player" �±׸� ������ ������ ó�� �� �Ѿ� ����
    /// </summary>
    /// <param name="other">�浹�� �ݶ��̴�</param>
    void OnTriggerEnter2D(Collider2D other)
    {
        // �浹�� ������Ʈ�� �÷��̾��� ���
        if (other.CompareTag("Player"))
        {
            // �÷��̾� ��ũ��Ʈ���� ������ �޴� �Լ� ȣ�� (TakeDamage �Լ��� �ִٰ� ����)
            PlayerController player = other.GetComponent<PlayerController>();
            if (player != null)
            {
                player.TakeDamage(1); // ������ 1��ŭ �ֱ� (�ʿ信 ���� ���� ����)
            }

            // �Ѿ��� �浹 �� �ı�
            Destroy(gameObject);
        }
        else if (other.CompareTag("Wall") || other.CompareTag("Obstacle"))
        {
            // ���̳� ��ֹ��� �浹 �ÿ��� �Ѿ� �ı�
            Destroy(gameObject);
        }
    }
}
