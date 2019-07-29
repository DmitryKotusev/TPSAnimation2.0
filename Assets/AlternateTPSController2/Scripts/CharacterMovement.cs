using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterMovement : MonoBehaviour
{
    [SerializeField]
    Transform cameraTransform;
    [SerializeField]
    CharacterStatus characterStatus;
    [SerializeField]
    Animator anim;
    [SerializeField]
    float vertical;
    [SerializeField]
    float horizontal;
    [SerializeField]
    float rotationSpeed = 100f;
    public float moveAmount;

    Vector3 rotationDirection;
    Vector3 moveDirection;

    public void MoveUpdate()
    {
        vertical = Input.GetAxis("Vertical");
        horizontal = Input.GetAxis("Horizontal");
        moveAmount = Mathf.Clamp01(Mathf.Abs(vertical) + Mathf.Abs(horizontal));

        anim.SetFloat("vertical", moveAmount, 0.15f, Time.deltaTime);
        // anim.SetFloat("horizontal", horizontal, 0.15f, Time.deltaTime);

        Vector3 moveDir = cameraTransform.forward * vertical;
        moveDir += cameraTransform.right * horizontal;
        moveDir.Normalize();
        moveDirection = moveDir;
        rotationDirection = cameraTransform.forward;

        RotationNormal();
    }

    public void RotationNormal()
    {
        if (!characterStatus.isAiming)
        {
            rotationDirection = moveDirection;
        }

        Vector3 targetDirection = rotationDirection;
        targetDirection.y = 0;

        if (targetDirection == Vector3.zero)
        {
            targetDirection = transform.forward;
        }

        Quaternion lookDir = Quaternion.LookRotation(targetDirection);
        Quaternion targetRot = Quaternion.Slerp(transform.rotation, lookDir, rotationSpeed * Time.deltaTime);
        transform.rotation = targetRot;
    }
}
