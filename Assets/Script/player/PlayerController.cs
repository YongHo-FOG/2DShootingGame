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

    // ¹«Àû ¹× ±ôºýÀÓ
    public float invincibleDuration = 2f;
    public float blinkInterval = 0.4f;
    private bool isInvincible = false;
    private float invincibleTimer = 0f;
    private float blinkTimer = 0f;

    private SpriteRenderer spriteRenderer;
    private float shotTimer = 0f;

    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    void Update()
    {
        Move();
        HandleShooting();
        HandleBomb();

        // ¹«Àû »óÅÂ ±ôºýÀÓ Ã³¸®
        if (isInvincible)
        {
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
            // ÆøÅº ¿äÃ»À» GameManager¿¡ Àü´Þ
            GameManager.Instance.TryUseBomb(transform.position);
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
        Debug.Log("[Player] »ç¸Á Ã³¸®");
        gameObject.SetActive(false);
        GameManager.Instance.OnPlayerDead();
    }

    private void SetSpriteAlpha(float alpha)
    {
        Color c = spriteRenderer.color;
        c.a = alpha;
        spriteRenderer.color = c;
    }
}
