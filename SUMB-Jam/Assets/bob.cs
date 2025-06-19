using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.UIElements;

public class UIBobbingScalingWobble : MonoBehaviour
{
    [Header("Bobbing Settings")]
    public float bobSpeed = 2f;
    public float bobHeight = 10f;

    [Header("Scaling Settings")]
    public float scaleSpeed = 2f;
    public float scaleAmount = 0.05f;

    [Header("Wobble Settings")]
    public float wobbleSpeed = 2.5f;
    public float wobbleAngle = 5f; // Degrees to tilt left/right

    private Vector3 originalPosition;
    private Vector3 originalScale;
    private Quaternion originalRotation;

    void Start()
    {
        originalPosition = transform.localPosition;
        originalScale = transform.localScale;
        originalRotation = transform.localRotation;
    }

    void Update()
    {
        float time = Time.time;

        // Bobbing up and down
        float bobOffset = Mathf.Sin(time * bobSpeed) * bobHeight;
        transform.localPosition = originalPosition + new Vector3(0, bobOffset, 0);

        // Scaling
        float scaleOffset = Mathf.Sin(time * scaleSpeed) * scaleAmount;
        transform.localScale = originalScale * (1f + scaleOffset);

        // Wobble (Z-axis rotation)
        float wobble = Mathf.Sin(time * wobbleSpeed) * wobbleAngle;
        transform.localRotation = originalRotation * Quaternion.Euler(0, 0, wobble);
    }
}
