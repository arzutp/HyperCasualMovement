using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerModel : MonoBehaviour
{
    [SerializeField] Animator animator;
    public void SetRun(bool isRun)
    {
        animator.SetBool("Run",isRun);
    }

    public void SetAnimReset()
    {
        animator.Rebind();
    }
}
