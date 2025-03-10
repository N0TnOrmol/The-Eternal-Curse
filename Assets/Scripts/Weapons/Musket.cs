using UnityEngine;

public class Musket : MonoBehaviour
{
    public float rpm;               // Rate of fire (shots per minute)
    public Transform spawnPoint;     // The point where the bullet spawns
    public GameObject bulletPrefab; // The bullet prefab
    
    private float secondsBetweenShots;   // Time between shots
    private float nextPossibleShootTime;  // The next time the player can shoot
    private bool isShootingAllowed = false; // If shooting is allowed
    
    private AudioSource audioSource; // For playing the gunshot sound

    void Start()
    {
        secondsBetweenShots = 60 / rpm; // Calculate the time between shots
        audioSource = GetComponent<AudioSource>(); // Get the AudioSource component
    }

    public void Shoot()
    {
        // Check if shooting is allowed and if enough time has passed since the last shot
        if (!isShootingAllowed || !CanShoot()) 
            return;

        // Play the gunshot sound ONLY when shooting
        if (audioSource != null)
        {
            audioSource.Play();
        }

        // Instantiate the bullet at the spawn point with the correct rotation
        GameObject bullet = Instantiate(bulletPrefab, spawnPoint.position, spawnPoint.rotation);
        
        // Get the Rigidbody component of the bullet
        Rigidbody bulletRb = bullet.GetComponent<Rigidbody>();
        if (bulletRb != null)
        {
            // Set the velocity to move along the Z-axis (spawnPoint.forward is typically aligned with the Z-axis)
            bulletRb.linearVelocity = spawnPoint.forward * 20f; // Bullet speed, adjust as needed
        }

        // Set the time for the next possible shot
        nextPossibleShootTime = Time.time + secondsBetweenShots;
    }

    void Update()
    {
        // If shooting is allowed and the attack button is pressed, shoot continuously
        if (isShootingAllowed && Input.GetButton("Attack") && enabled)
        {
            ShootContinuous();
        }
    }

    private bool CanShoot()
    {
        // Ensure enough time has passed and the audio is not currently playing
        return Time.time >= nextPossibleShootTime && (audioSource == null || !audioSource.isPlaying);
    }

    public void ShootContinuous()
    {
        if (CanShoot())
        {
            Shoot();
        }
    }

    public void SetShootingAllowed(bool allowed)
    {
        isShootingAllowed = allowed;
    }
}
