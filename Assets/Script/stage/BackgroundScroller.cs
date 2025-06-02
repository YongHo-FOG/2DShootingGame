using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackgroundScroller : MonoBehaviour
{
    [SerializeField]
    private Transform target;
    [SerializeField]
    private float scrollRange = 31.5f;
    [SerializeField]
    private float moveSpeed = 10f;
    [SerializeField]
    private Vector3 moveDirection = Vector3.left;

    private void Update()
    {
        transform.position += moveDirection * moveSpeed * Time.deltaTime;

        if( transform.position.x <= -scrollRange )
        {
            transform.position = target.position + Vector3.right * scrollRange;
        }
    }
}
