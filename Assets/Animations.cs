using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Animations : MonoBehaviour
{
    public CharacterController CC;
    public Transform CamTransform;
    public Animator animations;
    public float MoveSpeed = 10;
    public float currentBaseSpeed = 10;
    public float MouseSensitivity;
    private float camRotation = 0f;
    public float gravity = -9.8f;
    public float runMultiplier = 5f;
    public float yVelocity = 0f;
    public Vector3 movement;

    // Update is called once per frame
    void Update()
    {
        // X/Z movement
        movement = Vector3.zero;

        float forwardMovement = Input.GetAxis("Vertical") * MoveSpeed * Time.deltaTime;
        float sideMovement = Input.GetAxis("Horizontal") * MoveSpeed * Time.deltaTime;
        //yVelocity += gravity *= Time.deltaTime;
        movement += (transform.forward * forwardMovement) + (transform.right * sideMovement);

        /*movement += (transform.forward * forwardMovement) + (transform.right * sideMovement) + (transform.up * yVelocity);*/
        CC.Move(movement);

        //cam movement
        float mouseInputY = Input.GetAxis("Mouse Y") * MouseSensitivity;
        camRotation -= mouseInputY;
        camRotation = Mathf.Clamp(camRotation, 9f, 45f);
        CamTransform.localRotation = Quaternion.Euler(new Vector3(camRotation, 0f, 0f));

        float mouseInputX = Input.GetAxis("Mouse X") * MouseSensitivity;
        transform.rotation = Quaternion.Euler(transform.eulerAngles + new Vector3(0f, mouseInputX, 0f));

        if(movement.x == 0 && movement.y <= 0 && movement.z == 0 && !animations.GetBool("isRun"))
        {
            animations.SetBool("isIdle", true);
        }
        else if(!animations.GetBool("isRun"))
        {
            animations.SetBool("isWalk", true);
            animations.SetBool("isIdle", false);
        }

        if(Input.GetKeyDown(KeyCode.LeftShift))
        {
            Run();
        }

        if(Input.GetKeyUp(KeyCode.LeftShift))
        {
            endRun();
        }
    }

    public void Run()
    {
        MoveSpeed *= runMultiplier;
        animations.SetBool("isWalk", false);
        animations.SetBool("isIdle", false);
        animations.SetBool("isRun", true);
    }

    public void endRun()
    {
        MoveSpeed /= runMultiplier;
        animations.SetBool("isRun", false);
    }
}
