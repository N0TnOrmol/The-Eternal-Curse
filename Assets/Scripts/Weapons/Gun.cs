using UnityEngine;
public class Gun : MonoBehaviour
{
    public enum GunType { Semi, Burst, Auto };
    public GunType gunType;
    public float rpm;
    public Transform spawnPoint;
    public GameObject bulletPrefab; 
    private float secondsBetweenShots;
    private float nextPossibleShootTime;
    private bool isShootingAllowed = false;
    void Start()
    {
        secondsBetweenShots = 60 / rpm;
    }
    public void Shoot()
    {
        if (!isShootingAllowed || GetComponent<AudioSource>().isPlaying)
            return;

        if (CanShoot())  
        {
            GetComponent<AudioSource>().Play();
            GameObject bullet = Instantiate(bulletPrefab, spawnPoint.position, spawnPoint.rotation);
            bullet.transform.forward = spawnPoint.forward;

            Debug.Log("Bullet spawned at: " + spawnPoint.position);

            nextPossibleShootTime = Time.time + secondsBetweenShots;
        }
    }
    void Update()
    {
        // Only allow shooting if the gun is active and shooting is allowed
        if (isShootingAllowed && Input.GetButton("Attack") && enabled)
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
    // Set whether shooting is allowed or not
    public void SetShootingAllowed(bool allowed)
    {
        isShootingAllowed = allowed;
    }
}