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

    // ���ο��� ����� Ÿ�̸� ����
    private float shotTimer = 0f;

    void Update()
    {
        // �� �����Ӹ��� �̵�, ��, ��ź �Է��� Ȯ��
        Move();
        HandleShooting();
        HandleBomb();
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
}
