using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.UIElements;

[RequireComponent(typeof(CharacterController))]
public class PlayerMovement : MonoBehaviour
{   
    public float rotationSpeed = 450;
    public float walkSpeed = 5;
    public float runSpeed = 8;  

    private Quaternion targetRotation;

    public Gun gun;
    private CharacterController Controller;
    private Camera cam;

    void Start()
    {
        Controller = GetComponent<CharacterController>();
        cam = Camera.main;
    }

    void Update()
    {
        ControlMouse();
        //ControlWASD();

        if(Input.GetButtonDown("Shoot"))
        {
            gun.Shoot();
        }
        else if (Input.GetButton("Shoot"))
        {
            gun.ShootContinuous();
        }
    }   
    void ControlMouse()
    {
        Vector3 mousePos = Input.mousePosition;
        mousePos = cam.ScreenToWorldPoint(new Vector3(mousePos.x, mousePos.y, cam.transform.position.y - transform.position.y));
        targetRotation = Quaternion.LookRotation(mousePos - new Vector3(transform.position.x,0,transform.position.z));
        transform.rotation = Quaternion.RotateTowards(transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);

        Vector3 input = new Vector3(Input.GetAxisRaw("Horizontal"), 0, Input.GetAxisRaw("Vertical"));
        Vector3 Motion = input;
        Motion *= (Mathf.Abs(input.x) == 1 && MathF.Abs(input.z) == 1)?.7f:1;
        Motion *= (Input.GetButton("Run"))?runSpeed:walkSpeed;
        Motion += Vector3.up * -8;

        Controller.Move(Motion * Time.deltaTime);
    }
    void ControlWASD()
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