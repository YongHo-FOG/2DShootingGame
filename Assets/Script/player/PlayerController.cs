using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    // ========================
    // �� �̵� ���� �������� ��
    // ========================

    [Header("Movement Setting")]
    public float moveSpeed = 5f; // �⺻ �̵� �ӵ�
    public float slowMoveMultiplier = 0.5f; // ���� �̵� �� ������ ���� (e.g. 0.5�� ������)

    // ========================
    // �� �� �߻� ���� ���� ��
    // ========================

    [Header("Shooting Setting")]
    public GameObject bulletPrefab; // �߻��� �Ѿ� ������
    public Transform shotPoint; // �Ѿ��� ���� ��ġ (�÷��̾� ���ʿ� �� ������Ʈ)
    public float shotInterval = 0.2f; // �Ѿ� �߻� ���� (�� ����)

    // ========================
    // �� ��ź ���� ���� ��
    // ========================

    [Header("Bomb Setting")]
    public GameObject bombPrefab; // ��ź ȿ�� ������
    public int bombCount = 3; // ��� ������ ��ź ����

    // ========================
    // �� ���� �� ������ ó�� ��
    // ========================

    public float invincibleDuration = 2f; // ���� �ð�(�ǰ� ��, 2��)
    public float blinkInterval = 0.4f; // ������ �ֱ� (��)
    private bool isInvincible = false;
    private float invincibleTimer = 0f;
    private float blinkTimer = 0f;

    private SpriteRenderer spriteRenderer;

    // ���ο��� ����� Ÿ�̸� ����
    private float shotTimer = 0f;

    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    void Update()
    {
        // �� �����Ӹ��� �̵�, ��, ��ź �Է��� Ȯ��
        Move();
        HandleShooting();
        HandleBomb();

        // ���� ������ �� Ÿ�̸� �� ������ ó��
        if (isInvincible)
        {
            invincibleTimer += Time.deltaTime;
            blinkTimer += Time.deltaTime;

            if (blinkTimer >= blinkInterval)
            {
                // ���� ��� (1f <-> 0.3f)
                if (spriteRenderer.color.a == 1f)
                    SetSpriteAlpha(0.3f);
                else
                    SetSpriteAlpha(1f);

                blinkTimer = 0f;
            }

            // ���� �ð��� ������ ���� ���� �� ���� ����
            if (invincibleTimer >= invincibleDuration)
            {
                SetSpriteAlpha(1f);
                isInvincible = false;
                invincibleTimer = 0f;
            }
        }
    }

    // ========================
    // �� �÷��̾� �̵� ó��
    // ========================
    void Move()
    {
        // �Է� �� �޾ƿ��� (����/������: A/D or ��/��, ��/�Ʒ�: W/S or ��/��)
        float h = Input.GetAxisRaw("Horizontal");
        float v = Input.GetAxisRaw("Vertical");

        // Shift Ű�� ������ ���� �̵� ���
        bool isSlow = Input.GetKey(KeyCode.LeftShift);

        // �̵� �ӵ� ��� (���� ��� ���� ���ο� ���� �ӵ� ����)
        float currentSpeed = isSlow ? moveSpeed * slowMoveMultiplier : moveSpeed;

        // ���� ���� ���� (Z�� 0, XY�� ���)
        Vector3 moveDir = new Vector3(h, v, 0).normalized;

        // ���� �̵� ó��
        transform.position += moveDir * currentSpeed * Time.deltaTime;
    }

    // ========================
    // �� �ڵ� �� ó��
    // ========================
    void HandleShooting()
    {
        // �ð� ����
        shotTimer += Time.deltaTime;

        // Z Ű�� ������ �ְ�, �� ��Ÿ���� �����ٸ� �߻�
        if (Input.GetKey(KeyCode.Z) && shotTimer >= shotInterval)
        {
            // �Ѿ� ���� (shotPoint ��ġ����)
            Instantiate(bulletPrefab, shotPoint.position, Quaternion.identity);

            // Ÿ�̸� �ʱ�ȭ
            shotTimer = 0f;
        }
    }

    // ========================
    // �� ��ź ��� ó��
    // ========================
    void HandleBomb()
    {
        // X Ű�� ������, ���� ��ź�� ���� ���� ���
        if (Input.GetKeyDown(KeyCode.X) && bombCount > 0)
        {
            // ��ź ���� (�÷��̾� ��ġ��)
            Instantiate(bombPrefab, transform.position, Quaternion.identity);

            // ��ź �� ����
            bombCount--;
        }
    }

    // ========================
    // �� ������ ó�� �Լ�
    // ========================
    public void TakeDamage(int damage)
    {
        // ���� ���¸� ������ ����
        if (isInvincible) return;

        // GameManager���� ü�� ���� ��û
        GameManager.Instance.PlayerTakeDamage(damage);

        // ������ �Ծ��� �� ���� ���� ���� �� Ÿ�̸� �ʱ�ȭ
        isInvincible = true;
        invincibleTimer = 0f;
        blinkTimer = 0f;

        // TODO: ������ �¾��� �� ȿ����, ����Ʈ �� ó�� ����
    }

    // ========================
    // �� �÷��̾� ���� ó�� �Լ� (GameManager ȣ��)
    // ========================
    public void Die()
    {
        Debug.Log("Player Dead");

        // �÷��̾� ������Ʈ ��Ȱ��ȭ (���� ���� ǥ��)
        gameObject.SetActive(false);

        // ���ӸŴ����� �÷��̾� ��� �˸� (���ӿ��� ó�� ���)
        GameManager.Instance.OnPlayerDead();
    }

    // ========================
    // �� ��������Ʈ ���� ���� �Լ�
    // ========================
    private void SetSpriteAlpha(float alpha)
    {
        Color c = spriteRenderer.color;
        c.a = alpha;
        spriteRenderer.color = c;
    }
}