using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimation : MonoBehaviour
{
    Animator animator;
    void Awake()
    {
        animator = GetComponentInChildren<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        animator.SetFloat("Vertical", GameManager.Instance.InputController.vertical);
        animator.SetFloat("Horizontal", GameManager.Instance.InputController.horizontal);
        animator.SetBool("IsWalking", GameManager.Instance.InputController.isWalking);
        animator.SetBool("IsSprinting", GameManager.Instance.InputController.isSprinting);
        animator.SetBool("IsCrouching", GameManager.Instance.InputController.isCrouching);
        animator.applyRootMotion = false;
    }
}
