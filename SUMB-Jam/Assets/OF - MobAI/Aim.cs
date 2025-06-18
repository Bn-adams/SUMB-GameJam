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

        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;

        transform.position = player.transform.position; 
        transform.rotation = Quaternion.Euler(0, 0, angle - 90);

        direction = theStrap.transform.position - reticle.transform.position;
        angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        theStrap.transform.rotation = Quaternion.Euler(0, 0, angle);
    }
}
