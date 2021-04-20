using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MyPlayer : MonoBehaviour
{
    float moveSpeed = 3f;
    float smoothRotationTime = 0.25f;
    float currentVelocity;
    Transform cameraTransform;
    bool isJumping = false;
    public float jumpSpeed = 5f;
    public bool enableMobileInputs = false;
    public FixedJoystick joystick;
    public FixedButton button;

    private void Start()
    {
        cameraTransform = Camera.main.transform;
    }

    // Update is called once per frame
    void Update()
    {
        Vector2 input = Vector2.zero;
        if (enableMobileInputs)
        {
            input = new Vector2(joystick.input.x, joystick.input.y);
        }
        else
        {
            input = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));
        }
        Vector2 inputDir = input.normalized;

        if(inputDir != Vector2.zero)
        {
            float rotation = Mathf.Atan2(inputDir.x, inputDir.y) * Mathf.Rad2Deg + cameraTransform.eulerAngles.y;
            transform.eulerAngles = Vector3.up * Mathf.SmoothDampAngle(transform.eulerAngles.y, rotation, ref currentVelocity, smoothRotationTime); ;
        }
        float targetSpeed = moveSpeed * inputDir.magnitude;

        /*if (Input.GetKeyDown(KeyCode.Space))
        {
            GetComponent<Actions>().Jump();
        }*/
        if(inputDir.magnitude > 0)
        {
            GetComponent<Actions>().Run();
        }
        else
        {
            GetComponent<Actions>().Stay();
        }

        transform.Translate(transform.forward * (moveSpeed * inputDir.magnitude) * Time.deltaTime, Space.World);
    }

    public void Jump()
    {
        if (!isJumping)
        {
            isJumping = true;
        }

        GetComponent<Actions>().Jump();
    }

    private void resetIsJumping()
    {
        isJumping = false;
    }
}
