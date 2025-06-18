using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Threading;
using System.Threading.Tasks;

public class PlayerProjectile : MonoBehaviour
{
    public GameObject target;
    Vector3 x;
    public int speed;

    private void Start()
    {
        x = target.transform.position; 
        x = (x - transform.position).normalized;
        Delayer(2000);
    }

    void Update()
    {
        transform.position += x * Time.deltaTime * speed;
    }
    private async void Delayer(int timer)
    {
        await Task.Delay(timer);
        GameObject.Destroy(this.gameObject);
    }
}
