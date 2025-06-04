using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBulletMovement : MonoBehaviour
{
    [SerializeField]
    private float moveSpeed = 0.0f;
    [SerializeField]
    private Vector3 moveDirection = Vector3.zero;

    private void Update()
    {
        transform.position += moveDirection * moveSpeed * Time.deltaTime;
    }

    public void MoveTo(Vector3 Direction)
    {
        moveDirection = Direction;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        // 적 오브젝트에 Enemy 컴포넌트가 있는지 확인
        if (other.CompareTag("Monster"))
        {
            Enemy enemy = other.GetComponent<Enemy>();
            if (enemy != null)
            {
                enemy.TakeDamage(1);
            }

            Destroy(gameObject);
        }
        else
        {
            // 적이 아니더라도 맞은 게 벽 등이라면 제거
            if (other.gameObject.CompareTag("Wall"))
            {
                Destroy(gameObject);
            }
        }
    }

}
