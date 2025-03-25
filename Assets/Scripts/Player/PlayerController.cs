using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float moveSpeed = 5f;
    private Camera mainCamera;
    private Vector3 movement;

    void Start()
    {
        mainCamera = Camera.main;
    }

    void Update()
    {
        MovePlayer();
        RotateToMouse();
    }

    void MovePlayer()
    {
        float moveX = Input.GetAxis("Horizontal");
        float moveZ = Input.GetAxis("Vertical");

        movement = new Vector3(moveX, 0, moveZ).normalized;
        transform.position += movement * moveSpeed * Time.deltaTime;
    }

    void RotateToMouse()
    {
        Ray ray = mainCamera.ScreenPointToRay(Input.mousePosition);
        Plane groundPlane = new Plane(Vector3.up, Vector3.zero);
        float rayDistance;

        if (groundPlane.Raycast(ray, out rayDistance))
        {
            Vector3 point = ray.GetPoint(rayDistance);
            Vector3 direction = (point - transform.position);
            direction.y = 0; // Keep rotation on the XZ plane

            transform.rotation = Quaternion.LookRotation(direction);
        }
    }
}
