using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// EnemyShooter: 총알 발사를 담당하는 컴포넌트
/// - Player 방향으로 총알을 발사
/// - 외부에서 ShootAtPlayer()를 호출해야 함
/// </summary>
public class EnemyShooter : MonoBehaviour
{
    [Header("Bullet Settings")]
    public GameObject bulletPrefab;  // 발사할 총알 프리팹
    public Transform firePoint;      // 총알 생성 위치 (총구)

    private Transform player;        // 플레이어 위치 참조

    void Start()
    {
        // 태그로 플레이어 오브젝트 찾기
        player = GameObject.FindGameObjectWithTag("Player")?.transform;

        // firePoint가 설정되지 않았으면 현재 위치 사용
        if (firePoint == null)
            firePoint = this.transform;
    }

    /// <summary>
    /// 플레이어를 향해 총알을 발사 (EnemyA에서 호출)
    /// </summary>
    public void ShootAtPlayer()
    {
        if (player == null || bulletPrefab == null)
            return;

        // 총알 생성
        GameObject bullet = Instantiate(bulletPrefab, firePoint.position, Quaternion.identity);

        // 플레이어 방향 계산
        Vector3 direction = (player.position - firePoint.position).normalized;

        // 총알에 방향 전달
        EnemyBulletController bulletController = bullet.GetComponent<EnemyBulletController>();
        if (bulletController != null)
            bulletController.SetDirection(direction);

        // 디버그 출력
        Debug.Log($"[EnemyShooter] Bullet fired at direction: {direction}");
    }
}
