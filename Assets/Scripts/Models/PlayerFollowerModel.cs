using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerFollowerModel : MonoBehaviour
{
    [SerializeField] Animator animator;
    [SerializeField] private float followSpeed;
    public void SetRun(bool isRun)
    {
        animator.SetBool("Run", isRun);
    }
}
