using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBulletController : MonoBehaviour
{
    [Header("Bullet Settings")]
    [SerializeField] private float speed = 7f;      // 총알 이동 속도
    [SerializeField] private float lifeTime = 5f;   // 총알이 존재할 최대 시간(초)

    private Vector3 moveDirection;   // 총알이 움직일 방향 (정규화된 벡터)
    private float timer = 0f;        // 총알 생성 후 경과 시간 체크용

    /// <summary>
    /// 총알의 이동 방향을 초기화하는 함수
    /// 공격자(몬스터)가 총알을 발사할 때 호출함
    /// </summary>
    /// <param name="direction">발사 방향 벡터</param>
    public void SetDirection(Vector3 direction)
    {
        moveDirection = direction.normalized; // 방향을 단위벡터로 변환해서 저장
    }

    void Update()
    {
        // 총알 위치를 매 프레임마다 이동 방향과 속도에 맞춰 갱신
        transform.position += moveDirection * speed * Time.deltaTime;

        // 총알의 존재 시간을 누적
        timer += Time.deltaTime;

        // 설정된 생존 시간을 넘으면 총알 오브젝트 파괴
        if (timer > lifeTime)
        {
            Destroy(gameObject);
        }
    }

    /// <summary>
    /// 충돌 감지 처리 함수 (2D 물리 충돌)
    /// 충돌한 콜라이더가 "Player" 태그를 가지면 데미지 처리 후 총알 삭제
    /// </summary>
    /// <param name="other">충돌한 콜라이더</param>
    void OnTriggerEnter2D(Collider2D other)
    {
        // 충돌한 오브젝트가 플레이어일 경우
        if (other.CompareTag("Player"))
        {
            // 플레이어 스크립트에서 데미지 받는 함수 호출 (TakeDamage 함수가 있다고 가정)
            PlayerController player = other.GetComponent<PlayerController>();
            if (player != null)
            {
                player.TakeDamage(1); // 데미지 1만큼 주기 (필요에 따라 조절 가능)
            }

            // 총알은 충돌 후 파괴
            Destroy(gameObject);
        }
        else if (other.CompareTag("Wall") || other.CompareTag("Obstacle"))
        {
            // 벽이나 장애물과 충돌 시에도 총알 파괴
            Destroy(gameObject);
        }
    }
}
