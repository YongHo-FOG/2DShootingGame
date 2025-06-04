using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBulletMovement : MonoBehaviour
{
    [SerializeField]
    private float moveSpeed = 0.0f;
    [SerializeField]
    private Vector3 moveDirection = Vector3.zero;

    private void Update()
    {
        transform.position += moveDirection * moveSpeed * Time.deltaTime;
    }

    public void MoveTo(Vector3 Direction)
    {
        moveDirection = Direction;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        // �� ������Ʈ�� Enemy ������Ʈ�� �ִ��� Ȯ��
        if (other.CompareTag("Monster"))
        {
            Enemy enemy = other.GetComponent<Enemy>();
            if (enemy != null)
            {
                enemy.TakeDamage(1);
            }

            Destroy(gameObject);
        }
        else
        {
            // ���� �ƴϴ��� ���� �� �� ���̶�� ����
            if (other.gameObject.CompareTag("Wall"))
            {
                Destroy(gameObject);
            }
        }
    }

}
