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
        if (GetComponent<AudioSource>().isPlaying)
            return; // Don't shoot while audio is playing

        if (CanShoot())
        {
            // Debugging: Log when a bullet is spawned
            Debug.Log("Bullet spawned at: " + spawnPoint.position);
            
            // Instantiate the bullet
            GameObject bullet = Instantiate(bulletPrefab, spawnPoint.position, spawnPoint.rotation);

            // Move the bullet forward
            bullet.transform.forward = spawnPoint.forward;

            // Play shooting sound
            GetComponent<AudioSource>().Play();

            nextPossibleShootTime = Time.time + secondsBetweenShots;
        }
    }
    void Update()
    {
        if (Input.GetButton("Shoot"))  // Fire1 (usually left mouse button or Ctrl key)
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
        return Time.time >= nextPossibleShootTime;
    }
}
