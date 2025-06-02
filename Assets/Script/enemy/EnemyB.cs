using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.RuleTile.TilingRuleOutput;

/// <summary>
/// EnemyB: 흔들리는 이동 + 확산 사격
/// - 좌우로 흔들리며 내려옴
/// - 일정 주기마다 3방향으로 탄환 발사
/// </summary>
public class EnemyB : Enemy
{
    private float moveAngle = 0f;                    // 흔들림을 위한 각도 누적
    public float horizontalAmplitude = 2f;           // 좌우 이동 폭

    /// <summary>
    /// 좌우로 흔들리며 아래로 내려오는 움직임 패턴
    /// </summary>
    protected override void MovePattern()
    {
        moveAngle += Time.deltaTime * 2f;  // 움직일수록 각도 증가
        float x = Mathf.Sin(moveAngle) * horizontalAmplitude;

        // 좌우 흔들림 + 아래로 이동
        transform.Translate(new Vector3(x, -speed, 0) * Time.deltaTime);
    }

    /// <summary>
    /// 정면 + 좌우 15도 방향의 탄환을 3발 발사
    /// </summary>
    protected override void Fire()
    {
        float angleStep = 15f;

        for (int i = -1; i <= 1; i++)  // -1, 0, 1 → 총 3발
        {
            Quaternion rot = Quaternion.Euler(0, 0, i * angleStep);
            Instantiate(bulletPrefab, transform.position, rot);
        }
    }
}