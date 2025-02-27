using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(CharacterController))]
public class PlayerMovement : MonoBehaviour
{   
    public float rotationSpeed = 450;
    public float walkSpeed = 5;
    public float runSpeed = 8;  

    private Quaternion targetRotation;
    private CharacterController Controller;

    void Start()
    {
        Controller = GetComponent<CharacterController>();
    }

    void Update()
    {
        Vector3 input = new Vector3(Input.GetAxisRaw("Horizontal"), 0, Input.GetAxisRaw("Vertical"));

        if(input != Vector3.zero)
        {
            // Calculate target rotation based on input
            targetRotation = Quaternion.LookRotation(input);

            // Smoothly rotate towards the target rotation
            transform.rotation = Quaternion.RotateTowards(transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);
        }
        Vector3 Motion = input;
        Motion *= (Mathf.Abs(input.x) == 1 && MathF.Abs(input.z) == 1)?.7f:1;
        Motion *= (Input.GetButton("Run"))?runSpeed:walkSpeed;
        Motion += Vector3.up * -8;

        Controller.Move(Motion * Time.deltaTime);
    }
}