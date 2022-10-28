using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class PooledObject : MonoBehaviour
{
    private bool isInUse;
    public bool IsInUse => isInUse; 
    public virtual void SetActive()
    {
        isInUse = true;
        gameObject.SetActive(true);
    }

    public virtual void Dismiss()
    {
        isInUse = false;
        gameObject.SetActive(false);
    }
    public abstract void OnObjectSpawn();
}
