using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class FollowCamera : MonoBehaviour
{
    public Transform FollowTagret;
    public Vector3 offset;
    public float FollowStrength = 5f;


    // Update is called once per frame
    void Update()
    {
        if (FollowTagret == null) return;

        Vector3 desiredPosition = FollowTagret.position + offset;
        Vector3 direction = new Vector3(desiredPosition.x - transform.position.x, desiredPosition.y - transform.position.y, transform.position.z);
        float distance = direction.magnitude;

        // Speed scales with distance
        float speed = FollowStrength * distance;
        // Move toward target
        transform.position += direction.normalized * speed * Time.deltaTime;
    }
}
