using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.RuleTile.TilingRuleOutput;

/// <summary>
/// EnemyB: ��鸮�� �̵� + Ȯ�� ���
/// - �¿�� ��鸮�� ������
/// - ���� �ֱ⸶�� 3�������� źȯ �߻�
/// </summary>
public class EnemyB : Enemy
{
    private float moveAngle = 0f;                    // ��鸲�� ���� ���� ����
    public float horizontalAmplitude = 2f;           // �¿� �̵� ��

    /// <summary>
    /// �¿�� ��鸮�� �Ʒ��� �������� ������ ����
    /// </summary>
    protected override void MovePattern()
    {
        moveAngle += Time.deltaTime * 2f;  // �����ϼ��� ���� ����
        float x = Mathf.Sin(moveAngle) * horizontalAmplitude;

        // �¿� ��鸲 + �Ʒ��� �̵�
        transform.Translate(new Vector3(x, -speed, 0) * Time.deltaTime);
    }

    /// <summary>
    /// ���� + �¿� 15�� ������ źȯ�� 3�� �߻�
    /// </summary>
    protected override void Fire()
    {
        float angleStep = 15f;

        for (int i = -1; i <= 1; i++)  // -1, 0, 1 �� �� 3��
        {
            Quaternion rot = Quaternion.Euler(0, 0, i * angleStep);
            Instantiate(bulletPrefab, transform.position, rot);
        }
    }
}