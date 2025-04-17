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
    private Animator animator;
    public DMController dmController;
    void Start()
    {
        Controller = GetComponent<CharacterController>();
        cam = Camera.main;
        animator = GetComponentInChildren<Animator>();
        animator.updateMode = AnimatorUpdateMode.UnscaledTime;
        originalWalkSpeed = walkSpeed;
        originalRunSpeed = runSpeed;
    }
    void Update()
    {
        ControlMouse();
        ControlWASD();
        ApplyDizzyEffect();
        UpdateAnimation(); 
    }
    void ControlMouse()
    {
        Ray ray = cam.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit, 1000f, LayerMask.GetMask("Ground")))
        {
            Vector3 hitPoint = hit.point;

            // Match player's Y level with the hit point
            Vector3 lookTarget = new Vector3(hitPoint.x, transform.position.y, hitPoint.z);

            Vector3 direction = lookTarget - transform.position;

            if (direction.sqrMagnitude > 0.01f)
            {
                Quaternion lookRotation = Quaternion.LookRotation(direction);
                transform.rotation = Quaternion.RotateTowards(transform.rotation, lookRotation, rotationSpeed * Time.deltaTime);
            }
        }
    }

    void ControlWASD()
    {
        Vector3 input = new Vector3(Input.GetAxisRaw("Horizontal"), 0, Input.GetAxisRaw("Vertical"));
        currentVelocityMod = Vector3.MoveTowards(currentVelocityMod, input, acceleration * Time.deltaTime);
        Vector3 Motion = currentVelocityMod;
        Motion *= (Mathf.Abs(input.x) == 1 && Mathf.Abs(input.z) == 1) ? 0.7f : 1;

        Motion *= walkSpeed;  
        Motion += Vector3.up * -8;
        Controller.Move(Motion * Time.deltaTime);
    }
    void ApplyDizzyEffect()
    {
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
    void UpdateAnimation()
    {
        if (animator != null)
        {
            bool isMoving = Input.GetAxisRaw("Horizontal") != 0 || Input.GetAxisRaw("Vertical") != 0;
            animator.SetBool("IsRunning", isMoving);
        }
    }
}
