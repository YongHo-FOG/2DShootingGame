using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    // ========================
    // ▼ 이동 관련 설정값들 ▼
    // ========================

    [Header("Movement Setting")]
    public float moveSpeed = 5f; // 기본 이동 속도
    public float slowMoveMultiplier = 0.5f; // 느린 이동 시 곱해질 비율 (e.g. 0.5배 느려짐)

    // ========================
    // ▼ 샷 발사 관련 설정 ▼
    // ========================

    [Header("Shooting Setting")]
    public GameObject bulletPrefab; // 발사할 총알 프리팹
    public Transform shotPoint; // 총알이 나갈 위치 (플레이어 앞쪽에 빈 오브젝트)
    public float shotInterval = 0.2f; // 총알 발사 간격 (초 단위)

    // ========================
    // ▼ 폭탄 관련 설정 ▼
    // ========================

    [Header("Bomb Setting")]
    public GameObject bombPrefab; // 폭탄 효과 프리팹
    public int bombCount = 3; // 사용 가능한 폭탄 개수

    // ========================
    // ▼ 무적 및 깜빡임 처리 ▼
    // ========================

    public float invincibleDuration = 2f; // 무적 시간(피격 후, 2초)
    public float blinkInterval = 0.4f; // 깜빡임 주기 (초)
    private bool isInvincible = false;
    private float invincibleTimer = 0f;
    private float blinkTimer = 0f;

    private SpriteRenderer spriteRenderer;

    // 내부에서 사용할 타이머 변수
    private float shotTimer = 0f;

    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    void Update()
    {
        // 매 프레임마다 이동, 샷, 폭탄 입력을 확인
        Move();
        HandleShooting();
        HandleBomb();

        // 무적 상태일 때 타이머 및 깜빡임 처리
        if (isInvincible)
        {
            invincibleTimer += Time.deltaTime;
            blinkTimer += Time.deltaTime;

            if (blinkTimer >= blinkInterval)
            {
                // 투명도 토글 (1f <-> 0.3f)
                if (spriteRenderer.color.a == 1f)
                    SetSpriteAlpha(0.3f);
                else
                    SetSpriteAlpha(1f);

                blinkTimer = 0f;
            }

            // 무적 시간이 끝나면 무적 해제 및 투명도 원복
            if (invincibleTimer >= invincibleDuration)
            {
                SetSpriteAlpha(1f);
                isInvincible = false;
                invincibleTimer = 0f;
            }
        }
    }

    // ========================
    // ■ 플레이어 이동 처리
    // ========================
    void Move()
    {
        // 입력 값 받아오기 (왼쪽/오른쪽: A/D or ←/→, 위/아래: W/S or ↑/↓)
        float h = Input.GetAxisRaw("Horizontal");
        float v = Input.GetAxisRaw("Vertical");

        // Shift 키를 누르면 느린 이동 모드
        bool isSlow = Input.GetKey(KeyCode.LeftShift);

        // 이동 속도 계산 (느린 모드 적용 여부에 따라 속도 결정)
        float currentSpeed = isSlow ? moveSpeed * slowMoveMultiplier : moveSpeed;

        // 방향 벡터 생성 (Z는 0, XY만 사용)
        Vector3 moveDir = new Vector3(h, v, 0).normalized;

        // 실제 이동 처리
        transform.position += moveDir * currentSpeed * Time.deltaTime;
    }

    // ========================
    // ■ 자동 샷 처리
    // ========================
    void HandleShooting()
    {
        // 시간 누적
        shotTimer += Time.deltaTime;

        // Z 키를 누르고 있고, 샷 쿨타임이 지났다면 발사
        if (Input.GetKey(KeyCode.Z) && shotTimer >= shotInterval)
        {
            // 총알 생성 (shotPoint 위치에서)
            Instantiate(bulletPrefab, shotPoint.position, Quaternion.identity);

            // 타이머 초기화
            shotTimer = 0f;
        }
    }

    // ========================
    // ■ 폭탄 사용 처리
    // ========================
    void HandleBomb()
    {
        // X 키를 눌렀고, 남은 폭탄이 있을 때만 사용
        if (Input.GetKeyDown(KeyCode.X) && bombCount > 0)
        {
            // 폭탄 생성 (플레이어 위치에)
            Instantiate(bombPrefab, transform.position, Quaternion.identity);

            // 폭탄 수 차감
            bombCount--;
        }
    }

    // ========================
    // ■ 데미지 처리 함수
    // ========================
    public void TakeDamage(int damage)
    {
        // 무적 상태면 데미지 무시
        if (isInvincible) return;

        // GameManager에게 체력 관리 요청
        GameManager.Instance.PlayerTakeDamage(damage);

        // 데미지 입었을 때 무적 상태 시작 및 타이머 초기화
        isInvincible = true;
        invincibleTimer = 0f;
        blinkTimer = 0f;

        // TODO: 데미지 맞았을 때 효과음, 이펙트 등 처리 가능
    }

    // ========================
    // ■ 플레이어 죽음 처리 함수 (GameManager 호출)
    // ========================
    public void Die()
    {
        Debug.Log("Player Dead");

        // 플레이어 오브젝트 비활성화 (죽음 상태 표현)
        gameObject.SetActive(false);

        // 게임매니저에 플레이어 사망 알림 (게임오버 처리 담당)
        GameManager.Instance.OnPlayerDead();
    }

    // ========================
    // ■ 스프라이트 투명도 조절 함수
    // ========================
    private void SetSpriteAlpha(float alpha)
    {
        Color c = spriteRenderer.color;
        c.a = alpha;
        spriteRenderer.color = c;
    }
}