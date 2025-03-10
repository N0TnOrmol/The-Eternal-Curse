using UnityEngine;

public class Gun : MonoBehaviour
{
    public float rpm;
    public Transform spawnPoint;
    public GameObject bulletPrefab;
    private float secondsBetweenShots;
    private float nextPossibleShootTime;
    private bool isShootingAllowed = false;

    private AudioSource audioSource;

    void Start()
    {
        secondsBetweenShots = 60 / rpm;
        audioSource = GetComponent<AudioSource>(); // Get the AudioSource
    }

    public void Shoot()
    {
        // Only allow shooting if the gun is allowed to shoot, the time is right, and the audio is not playing
        if (!isShootingAllowed || !CanShoot() || audioSource.isPlaying) 
            return;

        // Play gun sound
        if (audioSource != null)
            audioSource.Play();

        // Instantiate bullet
        GameObject bullet = Instantiate(bulletPrefab, spawnPoint.position, spawnPoint.rotation);
        bullet.transform.forward = spawnPoint.forward;

        // Set next shot time
        nextPossibleShootTime = Time.time + secondsBetweenShots;
    }

    void Update()
    {
        if (isShootingAllowed && Input.GetButton("Attack") && enabled)
        {
            ShootContinuous();
        }
    }

    private bool CanShoot()
    {
        return Time.time >= nextPossibleShootTime;
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
