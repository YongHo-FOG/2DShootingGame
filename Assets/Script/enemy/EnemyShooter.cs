using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// EnemyShooter: �Ѿ� �߻縦 ����ϴ� ������Ʈ
/// - Player �������� �Ѿ��� �߻�
/// - �ܺο��� ShootAtPlayer()�� ȣ���ؾ� ��
/// </summary>
public class EnemyShooter : MonoBehaviour
{
    [Header("Bullet Settings")]
    public GameObject bulletPrefab;  // �߻��� �Ѿ� ������
    public Transform firePoint;      // �Ѿ� ���� ��ġ (�ѱ�)

    private Transform player;        // �÷��̾� ��ġ ����

    void Start()
    {
        // �±׷� �÷��̾� ������Ʈ ã��
        player = GameObject.FindGameObjectWithTag("Player")?.transform;

        // firePoint�� �������� �ʾ����� ���� ��ġ ���
        if (firePoint == null)
            firePoint = this.transform;
    }

    /// <summary>
    /// �÷��̾ ���� �Ѿ��� �߻� (EnemyA���� ȣ��)
    /// </summary>
    public void ShootAtPlayer()
    {
        if (player == null || bulletPrefab == null)
            return;

        // �Ѿ� ����
        GameObject bullet = Instantiate(bulletPrefab, firePoint.position, Quaternion.identity);

        // �÷��̾� ���� ���
        Vector3 direction = (player.position - firePoint.position).normalized;

        // �Ѿ˿� ���� ����
        EnemyBulletController bulletController = bullet.GetComponent<EnemyBulletController>();
        if (bulletController != null)
            bulletController.SetDirection(direction);

        // ����� ���
        Debug.Log($"[EnemyShooter] Bullet fired at direction: {direction}");
    }
}
