using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// ��� �� ���� ���̽� Ŭ����
/// - ������, ����, �ǰ� ó�� �� �⺻ Ʋ ����
/// </summary>
public class Enemy : MonoBehaviour
{
    public float speed = 2f;           // �⺻ �̵� �ӵ�
    public GameObject bulletPrefab;    // �߻��� źȯ ������
    public float fireRate = 2f;        // �߻� �ֱ�
    private float fireTimer = 0f;

    void Update()
    {
        MovePattern();                 // ������ ó��
        HandleShooting();             // �߻� �ֱ� ���
    }

    /// <summary>
    /// �ڽ� Ŭ�������� ������ Ŀ���͸���¡ ����
    /// </summary>
    protected virtual void MovePattern()
    {
        // �⺻�� �Ʒ��� ����
        transform.Translate(Vector3.down * speed * Time.deltaTime);
    }

    /// <summary>
    /// �ڽ� Ŭ�������� �߻� ��� Ŀ���͸���¡ ����
    /// </summary>
    protected virtual void Fire()
    {
        Instantiate(bulletPrefab, transform.position, Quaternion.identity);
    }

    /// <summary>
    /// ���� �ֱ�� Fire() ȣ��
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


    // �� �Ʒ��� �ǰ�, ü��, ��� �� �߰� ����
}
