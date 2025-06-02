using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackgroundBoundsChecker : MonoBehaviour
{
    void Start()
    {
        SpriteRenderer sr = GetComponentInChildren<SpriteRenderer>();
        if (sr != null)
        {
            Bounds bounds = sr.bounds;
            Debug.Log($"Background Bounds size: {bounds.size}");
            Debug.Log($"Background Bounds center: {bounds.center}");
        }
        else
        {
            Debug.LogWarning("No SpriteRenderer found in children.");
        }
    }
}
