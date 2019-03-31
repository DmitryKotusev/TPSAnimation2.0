using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputController : MonoBehaviour
{
    public float horizontal;
    public float vertical;
    public Vector2 mouseInput;

    public bool fire1;
    public bool reload;
    public bool isWalking;
    public bool isRunning;
    public bool isCrouching;
    public bool isSprinting;
    public bool mouseWheelUp;
    public bool mouseWheelDown;

    void Update()
    {
        horizontal = Input.GetAxis("Horizontal");
        vertical = Input.GetAxis("Vertical");
        mouseInput = new Vector2(Input.GetAxis("Mouse X"), Input.GetAxis("Mouse Y"));
        fire1 = Input.GetButton("Fire1");
        reload = Input.GetKey(KeyCode.R);
        isWalking = Input.GetKey(KeyCode.LeftControl);
        isRunning = Input.GetKey(KeyCode.LeftShift);
        isCrouching = Input.GetKey(KeyCode.C);
        isSprinting = Input.GetKey(KeyCode.LeftShift);
        mouseWheelUp = Input.GetAxis("Mouse ScrollWheel") > 0;
        mouseWheelDown = Input.GetAxis("Mouse ScrollWheel") < 0;
    }
}
