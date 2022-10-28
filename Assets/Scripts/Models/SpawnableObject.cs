using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnableObject :  PooledObject
{
    [SerializeField] float upForce = 2f;
    [SerializeField] float downForce = 0.1f;
    [SerializeField] Rigidbody rb;
    public void Start()
    {
        
    }

    public override void OnObjectSpawn()
    {
        transform.localPosition = new Vector3(Random.Range(-0.5f, 0.5f), 0.5f, Random.Range(-0.5f, 0.5f));
        float xForce = Random.Range(-downForce, downForce);
        float yForce = Random.Range(upForce / 2f, upForce);
        float zForce = Random.Range(-downForce, downForce);

        Vector3 force = new Vector3(xForce, yForce, zForce);
        rb.velocity = force;
    }
    
    
}
