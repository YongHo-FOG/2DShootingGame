using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [Header("Movement Setting")]
    public float moveSpeed = 5f;
    public float slowMoveMultiplier = 0.5f;

    [Header("Shooting Setting")]
    public GameObject bulletPrefab;
    public Transform shotPoint;
    public float shotInterval = 0.2f;

    [Header("Invincibility Settings")]
    public float invincibleDuration = 2f;
    public float blinkInterval = 0.4f;

    private bool isInvincible = false;
    private float invincibleTimer = 0f;
    private float blinkTimer = 0f;
    private float shotTimer = 0f;

    private SpriteRenderer spriteRenderer;

    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    void Update()
    {
        Move();
        HandleShooting();
        HandleBomb();
        HandleInvincibility(); // 깜빡임 코드 분리

        ClampToScreen(); // 🔹 화면 밖으로 못 나가게 제한

        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            TakeDamage(1);
        }
    }

    void Move()
    {
        float h = Input.GetAxisRaw("Horizontal");
        float v = Input.GetAxisRaw("Vertical");
        bool isSlow = Input.GetKey(KeyCode.LeftShift);
        float currentSpeed = isSlow ? moveSpeed * slowMoveMultiplier : moveSpeed;

        Vector3 moveDir = new Vector3(h, v, 0).normalized;
        transform.position += moveDir * currentSpeed * Time.deltaTime;
    }

    void HandleShooting()
    {
        shotTimer += Time.deltaTime;

        if (Input.GetKey(KeyCode.Z) && shotTimer >= shotInterval)
        {
            Instantiate(bulletPrefab, shotPoint.position, Quaternion.identity);
            shotTimer = 0f;
        }
    }

    void HandleBomb()
    {
        if (Input.GetKeyDown(KeyCode.X))
        {
            GameManager.Instance.TryUseBomb(transform.position);
        }
    }

    void HandleInvincibility()
    {
        if (!isInvincible) return;

        invincibleTimer += Time.deltaTime;
        blinkTimer += Time.deltaTime;

        if (blinkTimer >= blinkInterval)
        {
            SetSpriteAlpha(spriteRenderer.color.a == 1f ? 0.3f : 1f);
            blinkTimer = 0f;
        }

        if (invincibleTimer >= invincibleDuration)
        {
            SetSpriteAlpha(1f);
            isInvincible = false;
            invincibleTimer = 0f;
        }
    }

    public void TakeDamage(int damage)
    {
        if (isInvincible) return;

        GameManager.Instance.PlayerTakeDamage(damage);
        isInvincible = true;
        invincibleTimer = 0f;
        blinkTimer = 0f;
    }

    public void Die()
    {
        Debug.Log("[Player] 사망 처리");
        gameObject.SetActive(false);
        GameManager.Instance.OnPlayerDead();
    }

    private void SetSpriteAlpha(float alpha)
    {
        Color c = spriteRenderer.color;
        c.a = alpha;
        spriteRenderer.color = c;
    }

    /// <summary>
    /// 🔹 플레이어가 카메라 화면 밖으로 나가지 않도록 위치 제한
    /// </summary>
    void ClampToScreen()
    {
        Vector3 pos = transform.position;

        Camera cam = Camera.main;

        Vector3 min = cam.ViewportToWorldPoint(new Vector3(0, 0, cam.nearClipPlane));
        Vector3 max = cam.ViewportToWorldPoint(new Vector3(1, 1, cam.nearClipPlane));

        float halfWidth = spriteRenderer.bounds.extents.x;
        float halfHeight = spriteRenderer.bounds.extents.y;

        pos.x = Mathf.Clamp(pos.x, min.x + halfWidth, max.x - halfWidth);
        pos.y = Mathf.Clamp(pos.y, min.y + halfHeight, max.y - halfHeight);

        transform.position = pos;
    }
}
