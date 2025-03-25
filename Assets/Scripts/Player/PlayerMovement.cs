using UnityEngine;

[RequireComponent(typeof(CharacterController))]
public class PlayerMovement : MonoBehaviour
{
    public float rotationSpeed = 450;
    public float walkSpeed = 8;
    public float runSpeed = 8;
    private float acceleration = 5;

    private Quaternion targetRotation;
    private Vector3 currentVelocityMod;

    private CharacterController Controller;
    private Camera cam;
    private float originalWalkSpeed;
    private float originalRunSpeed;

    // Reference to the drunk level
    public DMController dmController;

    void Start()
    {
        Controller = GetComponent<CharacterController>();
        cam = Camera.main;

        // Save the original speeds for restoration
        originalWalkSpeed = walkSpeed;
        originalRunSpeed = runSpeed;
    }

    void Update()
    {
        ControlMouse();
        ControlWASD();
        ApplyDizzyEffect();
    }

    void ControlMouse()
    {
        // Raycast from the camera towards the ground
        Ray ray = cam.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit, Mathf.Infinity, LayerMask.GetMask("Ground"))) // Ensure ground has the correct layer
        {
            Vector3 targetPoint = hit.point;
            targetRotation = Quaternion.LookRotation(targetPoint - new Vector3(transform.position.x, 0, transform.position.z));
            transform.rotation = Quaternion.RotateTowards(transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);
        }
    }

    void ControlWASD()
    {
        Vector3 input = new Vector3(Input.GetAxisRaw("Horizontal"), 0, Input.GetAxisRaw("Vertical"));
        currentVelocityMod = Vector3.MoveTowards(currentVelocityMod, input, acceleration * Time.deltaTime);
        Vector3 Motion = currentVelocityMod;
        Motion *= (Mathf.Abs(input.x) == 1 && Mathf.Abs(input.z) == 1) ? 0.7f : 1;

        // Apply run/walk speed
        Motion *= (Input.GetButton("Run")) ? runSpeed : walkSpeed;

        // Apply gravity
        Motion += Vector3.up * -8;

        Controller.Move(Motion * Time.deltaTime);
    }

    void ApplyDizzyEffect()
    {
        // Apply dizzy effects based on DLIndex
        if (dmController.DLIndex == 1)
        {
            walkSpeed = originalWalkSpeed * 1.2f; // Slightly increased speed
            runSpeed = originalRunSpeed * 1.2f;
        }
        else if (dmController.DLIndex == 2)
        {
            walkSpeed = originalWalkSpeed * 0.8f; // Slower walk speed
            runSpeed = originalRunSpeed * 0.8f;
        }
        else if (dmController.DLIndex == 3)
        {
            walkSpeed = originalWalkSpeed * 0.6f; // Much slower
            runSpeed = originalRunSpeed * 0.6f;
            // Randomize movement direction to simulate dizziness
            transform.Rotate(0, Random.Range(-5f, 5f), 0);
        }
        else if (dmController.DLIndex == 4)
        {
            walkSpeed = originalWalkSpeed * 0.5f; // Very slow
            runSpeed = originalRunSpeed * 0.5f;
            // Apply random rotation more often
            transform.Rotate(0, Random.Range(-10f, 10f), 0);
        }
        else
        {
            walkSpeed = originalWalkSpeed;
            runSpeed = originalRunSpeed;
        }
    }
}
