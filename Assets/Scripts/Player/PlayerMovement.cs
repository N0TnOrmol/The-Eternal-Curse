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

    private Animator animator; // Reference to the Animator

    // Reference to the drunk level
    public DMController dmController;

    void Start()
    {
        Controller = GetComponent<CharacterController>();
        cam = Camera.main;

        // Find and assign the Animator from "Player animated"
        GameObject animatedPlayer = GameObject.Find("Player animated");
        if (animatedPlayer != null)
        {
            animator = animatedPlayer.GetComponent<Animator>();
        }
        else
        {
            Debug.LogError("Player animated object not found! Animations won't work.");
        }

        // Save original speeds
        originalWalkSpeed = walkSpeed;
        originalRunSpeed = runSpeed;
    }

    void Update()
    {
        ControlMouse();
        ControlWASD();
        ApplyDizzyEffect();
        UpdateAnimations();
    }

    void ControlMouse()
    {
        // Raycast from the camera towards the ground
        Ray ray = cam.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit, Mathf.Infinity, LayerMask.GetMask("Ground"))) 
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
        bool isRunning = Input.GetButton("Run");
        Motion *= isRunning ? runSpeed : walkSpeed;

        // Apply gravity
        Motion += Vector3.up * -8;

        Controller.Move(Motion * Time.deltaTime);
    }

    void ApplyDizzyEffect()
    {
        if (dmController == null) return;

        if (dmController.DLIndex == 1)
        {
            walkSpeed = originalWalkSpeed * 1.2f;
            runSpeed = originalRunSpeed * 1.2f;
        }
        else if (dmController.DLIndex == 2)
        {
            walkSpeed = originalWalkSpeed * 0.8f;
            runSpeed = originalRunSpeed * 0.8f;
        }
        else if (dmController.DLIndex == 3)
        {
            walkSpeed = originalWalkSpeed * 0.6f;
            runSpeed = originalRunSpeed * 0.6f;
            transform.Rotate(0, Random.Range(-5f, 5f), 0);
        }
        else if (dmController.DLIndex == 4)
        {
            walkSpeed = originalWalkSpeed * 0.5f;
            runSpeed = originalRunSpeed * 0.5f;
            transform.Rotate(0, Random.Range(-10f, 10f), 0);
        }
        else
        {
            walkSpeed = originalWalkSpeed;
            runSpeed = originalRunSpeed;
        }
    }

    void UpdateAnimations()
    {
        if (animator == null) return;

        // Movement animation
        bool isMoving = currentVelocityMod.magnitude > 0.1f;
        animator.SetBool("IsRunning", isMoving && Input.GetButton("Run"));

        // Shooting animation (should be triggered externally by the weapon script)
        if (Input.GetButtonDown("Attack")) 
        {
            animator.SetBool("IsShooting", true);
            Invoke(nameof(ResetShootingAnimation), 0.1f);
        }

        // Melee animation (should also be triggered externally)
        if (Input.GetButtonDown("Attack")) 
        {
            animator.SetBool("IsMelee", true);
            Invoke(nameof(ResetMeleeAnimation), 0.5f);
        }
    }

    void ResetShootingAnimation()
    {
        animator.SetBool("IsShooting", false);
    }

    void ResetMeleeAnimation()
    {
        animator.SetBool("IsMelee", false);
    }
}
