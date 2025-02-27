using UnityEngine;

public class CameraController : MonoBehaviour
{
    public Transform player; 
    public float smoothSpeed = 0.125f; 
    public Vector3 offset; 
    public bool lockRotation = true; 
    public Vector3 fixedRotation = new Vector3(45f, 0f, 0f);

    void Start()
    {
       
        if (lockRotation)
        {
            transform.rotation = Quaternion.Euler(fixedRotation);
        }
    }

    void LateUpdate()
    {
        if (player == null)
        {
            Debug.LogWarning("Hello");
            return;
        }

       
        Vector3 desiredPosition = player.position + offset;

        
        Vector3 smoothedPosition = Vector3.Lerp(transform.position, desiredPosition, smoothSpeed);
        transform.position = smoothedPosition;

      
        if (lockRotation)
        {
            transform.rotation = Quaternion.Euler(fixedRotation);
        }
    }

}