using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Aim : MonoBehaviour
{
    public GameObject player; // Reference to player transform
    public GameObject theStrap;
    public GameObject reticle;


    void Update()
    {
        Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        mousePos.z = 0f; 
        Vector3 direction = mousePos - player.transform.position;
        transform.position = player.transform.position;
        Vector2 direction2D = (player.transform.position - mousePos).normalized;
        float angle2D = Mathf.Atan2(direction.y, direction.x ) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(0, 0, angle2D - 90);
        theStrap.transform.rotation = Quaternion.Euler(0, 0, angle2D + 180 );
    }
}
