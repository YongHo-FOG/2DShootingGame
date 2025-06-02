using System.Collections;
using System.Collections.Generic;
using UnityEngine;

    /// <summary>
    /// �� �Ѿ� - ������ �������� ���� �̵� + �浹 ó�� + ȭ�� �� ����
    /// </summary>
    public class EnemyBulletController : MonoBehaviour
    {
        public float speed = 5f;
        public float lifetime = 5f; // �ִ� ���� �ð� (��)
        private Vector3 moveDirection;

        void Start()
        {
            // ���� �ð��� ������ �ڵ����� �ı�
            Destroy(gameObject, lifetime);
        }

        void Update()
        {
            transform.position += moveDirection * speed * Time.deltaTime;
        }

        /// <summary>
        /// EnemyA�κ��� ������ ���޹���
        /// </summary>
        public void SetDirection(Vector3 direction)
        {
            moveDirection = direction.normalized;
        }

        /// <summary>
        /// �÷��̾�� �浹 �� ������ ���� �� ����
        /// </summary>
        void OnTriggerEnter2D(Collider2D other)
        {
            if (other.CompareTag("Player"))
            {
                PlayerController player = other.GetComponent<PlayerController>();
                if (player != null)
                {
                    player.TakeDamage(1);
                }

                Destroy(gameObject);
            }
            else if (other.CompareTag("Obstacle")) // �ʿ� �� ��ֹ� � ó�� ����
            {
                Destroy(gameObject);
            }
        }
    }
