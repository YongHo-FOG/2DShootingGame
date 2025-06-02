using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// EnemyC: ������ ��
/// - ���� �Ÿ����� �ϰ� �� ���� �� ���ڱ� ���� ����
/// - ���� ����
/// </summary>
public class EnemyC : Enemy
{
    // �ൿ ����
    private enum State { Descending, Pausing, Escaping }
    private State state = State.Descending;

    private float pauseTime = 1.5f;  // �����ִ� �ð�
    private float timer = 0f;

    /// <summary>
    /// ���¿� ���� �̵� ��� ����
    /// </summary>
    protected override void MovePattern()
    {
        switch (state)
        {
            case State.Descending:
                // �Ʒ��� �̵��ϴٰ� Ư�� ��ġ���� ����
                transform.Translate(Vector3.down * speed * Time.deltaTime);
                if (transform.position.y <= 3.5f)
                {
                    state = State.Pausing;
                    timer = 0f;
                }
                break;

            case State.Pausing:
                // ���� ���� ����
                timer += Time.deltaTime;
                if (timer >= pauseTime)
                {
                    state = State.Escaping;
                }
                break;

            case State.Escaping:
                // �������� ����
                transform.Translate(Vector3.left * speed * 2f * Time.deltaTime);
                break;
        }
    }

    /// <summary>
    /// �� ���� �������� ����
    /// </summary>
    protected override void Fire()
    {
        // �ƹ��͵� ���� ����
    }
}
