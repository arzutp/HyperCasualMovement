using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class ObjectPool : MonoBehaviour
{
    private Queue<PooledObject> spawnableObjects;
    [SerializeField] SpawnableObject spawnablePrefab;
    [SerializeField] int poolSize = 16;

    private void Awake()
    {
        BallPoolObjects();
    }

    public void BallPoolObjects()
    {
        spawnableObjects = new Queue<PooledObject>();
        for (int i = 0; i < poolSize; i++)
        {
            PooledObject spawnable = Instantiate(spawnablePrefab, parent: transform);
            spawnable.Dismiss();
            spawnableObjects.Enqueue(spawnable);
        }
    }

    public PooledObject GetPoolObject()
    {
        PooledObject spaw = spawnableObjects.Where(i => i.IsInUse == false).FirstOrDefault();
        if (spaw != null) { 
            spaw.SetActive();
            spaw.OnObjectSpawn();
            spawnableObjects.Enqueue(spaw);
            return spaw;
        }
        return null;
    }

    public void EnqueueBall(PooledObject pooledObject)
    {
        pooledObject.Dismiss();
    }
}

