using System.Collections;
using System.Collections.Generic;
using UnityEngine;

    /// <summary>
    /// 적 총알 - 지정된 방향으로 직선 이동 + 충돌 처리 + 화면 밖 제거
    /// </summary>
    public class EnemyBulletController : MonoBehaviour
    {
        public float speed = 5f;
        public float lifetime = 5f; // 최대 생존 시간 (초)
        private Vector3 moveDirection;

        void Start()
        {
            // 일정 시간이 지나면 자동으로 파괴
            Destroy(gameObject, lifetime);
        }

        void Update()
        {
            transform.position += moveDirection * speed * Time.deltaTime;
        }

        /// <summary>
        /// EnemyA로부터 방향을 전달받음
        /// </summary>
        public void SetDirection(Vector3 direction)
        {
            moveDirection = direction.normalized;
        }

        /// <summary>
        /// 플레이어와 충돌 시 데미지 적용 후 제거
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
            else if (other.CompareTag("Obstacle")) // 필요 시 장애물 등도 처리 가능
            {
                Destroy(gameObject);
            }
        }
    }
