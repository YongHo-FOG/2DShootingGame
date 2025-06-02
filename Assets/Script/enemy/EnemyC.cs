using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// EnemyC: 도주형 적
/// - 일정 거리까지 하강 → 정지 → 갑자기 좌측 도주
/// - 공격 없음
/// </summary>
public class EnemyC : Enemy
{
    // 행동 상태
    private enum State { Descending, Pausing, Escaping }
    private State state = State.Descending;

    private float pauseTime = 1.5f;  // 멈춰있는 시간
    private float timer = 0f;

    /// <summary>
    /// 상태에 따라 이동 방식 변경
    /// </summary>
    protected override void MovePattern()
    {
        switch (state)
        {
            case State.Descending:
                // 아래로 이동하다가 특정 위치에서 멈춤
                transform.Translate(Vector3.down * speed * Time.deltaTime);
                if (transform.position.y <= 3.5f)
                {
                    state = State.Pausing;
                    timer = 0f;
                }
                break;

            case State.Pausing:
                // 정지 상태 유지
                timer += Time.deltaTime;
                if (timer >= pauseTime)
                {
                    state = State.Escaping;
                }
                break;

            case State.Escaping:
                // 왼쪽으로 도주
                transform.Translate(Vector3.left * speed * 2f * Time.deltaTime);
                break;
        }
    }

    /// <summary>
    /// 이 적은 공격하지 않음
    /// </summary>
    protected override void Fire()
    {
        // 아무것도 하지 않음
    }
}
