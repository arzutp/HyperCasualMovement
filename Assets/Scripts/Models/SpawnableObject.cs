using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnableObject :  PooledObject
{
    [SerializeField] float upForce = 1f;
    [SerializeField] float downForce = 0.1f;
    [SerializeField] Rigidbody rb;
    public void Start()
    {
        Dismiss();
    }

    public override void OnObjectSpawn()
    {
        float xForce = Random.Range(-downForce, downForce);
        float yForce = Random.Range(upForce / 2f, upForce);
        float zForce = Random.Range(-downForce, downForce);

        Vector3 force = new Vector3(xForce, yForce, zForce);
        rb.velocity = force;
    }
    
    
}
