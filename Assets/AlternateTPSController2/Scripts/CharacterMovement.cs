using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterMovement : MonoBehaviour
{
    [SerializeField]
    Animator anim;
    [SerializeField]
    float vertical;
    [SerializeField]
    float horizontal;
    public float moveAmount;

    public void MoveUpdate()
    {
        vertical = Input.GetAxis("Vertical");
        horizontal = Input.GetAxis("Horizontal");
        moveAmount = Mathf.Clamp01(Mathf.Abs(vertical) + Mathf.Abs(horizontal));

        anim.SetFloat("vertical", vertical, 0.15f, Time.deltaTime);
        anim.SetFloat("horizontal", horizontal, 0.15f, Time.deltaTime);
    }
}
