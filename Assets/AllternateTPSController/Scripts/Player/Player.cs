using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(MoveController))]
public class Player : MonoBehaviour
{
    [System.Serializable]
    public class MouseInput
    {
        public Vector2 damping;
        public Vector2 sensitivity;
        public bool lockMouse;
    }

    Vector3 previousPosition;

    [SerializeField]
    float runSpeed;
    [SerializeField]
    float walkSpeed;
    [SerializeField]
    float crouchSpeed;
    [SerializeField]
    float spintSpeed;
    [SerializeField]
    MouseInput mouseControl;
    [SerializeField]
    AudioController footSteps;
    [SerializeField]
    float minimumMoveTresHold;

    CrossHair m_CrossHair;
    CrossHair CrossHair
    {
        get
        {
            if(m_CrossHair == null)
            {
                m_CrossHair = GetComponentInChildren<CrossHair>();
            }
            return m_CrossHair;
        }
    }

    MoveController m_MoveController;
    public MoveController moveController
    {
        get
        {
            if(m_MoveController == null)
            {
                m_MoveController = GetComponent<MoveController>();
            }
            return m_MoveController;
        }
    }

    InputController playerInput;
    Vector2 mouseInput;
    void Start()
    {
        playerInput = GameManager.Instance.InputController;
        GameManager.Instance.LocalPlayer = this;

        if(mouseControl.lockMouse)
        {
            Cursor.visible = false;
            Cursor.lockState = CursorLockMode.Locked;
        }
    }

    void Update()
    {
        Move();

        MouseControl();
    }

    private void MouseControl()
    {
        mouseInput.x = Mathf.Lerp(mouseInput.x, playerInput.mouseInput.x, 1f / mouseControl.damping.x);
        mouseInput.y = Mathf.Lerp(mouseInput.y, playerInput.mouseInput.y, 1f / mouseControl.damping.y);
        // mouseInput.y = playerInput.mouseInput.y;

        transform.Rotate(Vector3.up * mouseInput.x * mouseControl.sensitivity.x);
        CrossHair.LookHeight(mouseInput.y * mouseControl.sensitivity.y);
    }

    private void Move()
    {
        float moveSpeed = runSpeed;

        if(playerInput.isWalking)
        {
            moveSpeed = walkSpeed;
        }
        if (playerInput.isSprinting)
        {
            moveSpeed = spintSpeed;
        }
        if (playerInput.isCrouching)
        {
            moveSpeed = crouchSpeed;
        }

        Vector2 direction = new Vector2(playerInput.vertical * moveSpeed, playerInput.horizontal * moveSpeed);
        moveController.Move(direction);

        if(Vector3.Distance(previousPosition, transform.position) > minimumMoveTresHold)
        {
            footSteps.Play();
        }

        previousPosition = transform.position;
    }
}
