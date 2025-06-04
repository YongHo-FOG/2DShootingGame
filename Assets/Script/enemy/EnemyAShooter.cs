using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAShooter : MonoBehaviour
{
    public GameObject bulletPrefab;
    public float bulletSpeed = 5f;
    public Transform firePoint;

    private bool hasFired = false;
    private Transform player;

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player")?.transform;

        if (firePoint == null)
            firePoint = transform;

        FireOnceAtPlayer();  // 한 번만 발사
    }

    void FireOnceAtPlayer()
    {
        if (hasFired || player == null || bulletPrefab == null) return;

        hasFired = true;

        Vector2 dir = (player.position - firePoint.position).normalized;

        GameObject bullet = Instantiate(bulletPrefab, firePoint.position, Quaternion.identity);
        Rigidbody2D rb = bullet.GetComponent<Rigidbody2D>();
        if (rb != null)
        {
            rb.velocity = dir * bulletSpeed;
        }
    }
}
