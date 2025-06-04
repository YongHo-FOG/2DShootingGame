using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 모든 적 공통 베이스 클래스
/// - 움직임, 공격, 피격 처리 등 기본 틀 정의
/// </summary>
public class Enemy : MonoBehaviour
{
    public float speed = 2f;           // 기본 이동 속도
    public GameObject bulletPrefab;    // 발사할 탄환 프리팹
    public float fireRate = 2f;        // 발사 주기
    private float fireTimer = 0f;

    void Update()
    {
        MovePattern();                 // 움직임 처리
        HandleShooting();             // 발사 주기 계산
    }

    /// <summary>
    /// 자식 클래스에서 움직임 커스터마이징 가능
    /// </summary>
    protected virtual void MovePattern()
    {
        // 기본은 아래로 직진
        transform.Translate(Vector3.down * speed * Time.deltaTime);
    }

    /// <summary>
    /// 자식 클래스에서 발사 방식 커스터마이징 가능
    /// </summary>
    protected virtual void Fire()
    {
        Instantiate(bulletPrefab, transform.position, Quaternion.identity);
    }

    /// <summary>
    /// 일정 주기로 Fire() 호출
    /// </summary>
    void HandleShooting()
    {
        fireTimer += Time.deltaTime;
        if (fireTimer >= fireRate)
        {
            fireTimer = 0f;
            Fire();
        }
    }


    // 이 아래로 피격, 체력, 드랍 등 추가 예정
}
