using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class Gun : MonoBehaviour
{
    public enum GunType { Semi, Burst, Auto };
    public GunType gunType;
    public float rpm;
    public Transform spawnPoint; // Where the bullet will be spawned
    public GameObject bulletPrefab; // Reference to the 3D bullet prefab

    // System:
    private float secondsBetweenShots;
    private float nextPossibleShootTime;

    void Start()
    {
        secondsBetweenShots = 60 / rpm; // Calculate the time between shots
    }

    public void Shoot()
    {
        // Don't allow shooting if the audio is already playing
        if (GetComponent<AudioSource>().isPlaying)
            return;

        if (CanShoot())  // Only shoot if the cooldown period allows
        {
            // Play the shooting sound immediately when the gun fires
            GetComponent<AudioSource>().Play();

            // Instantiate the bullet at the spawn point
            GameObject bullet = Instantiate(bulletPrefab, spawnPoint.position, spawnPoint.rotation);

            // Optional: Set bullet's direction to move forward
            bullet.transform.forward = spawnPoint.forward;

            // Log when the bullet is spawned (for debugging)
            Debug.Log("Bullet spawned at: " + spawnPoint.position);

            // Set the next time you can shoot based on RPM (rate of fire)
            nextPossibleShootTime = Time.time + secondsBetweenShots;
        }
    }

    void Update()
    {
        // Detect shooting input (e.g., left mouse button or a key)
        if (Input.GetButton("Attack"))  // Fire1 is usually left mouse button or Ctrl key
        {
            ShootContinuous();
        }
    }

    // This method should handle continuous firing if the gun is in auto mode
    public void ShootContinuous()
    {
        if (gunType == GunType.Auto)
        {
            Shoot(); // Call Shoot method for continuous shooting
        }
    }

    private bool CanShoot()
    {
        return Time.time >= nextPossibleShootTime; // Check if enough time has passed since the last shot
    }
}
