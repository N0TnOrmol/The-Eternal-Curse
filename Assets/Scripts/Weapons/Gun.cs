using UnityEngine;

public class Gun : MonoBehaviour
{
    public float rpm = 600f;   // Rate of fire (shots per minute)
    public Transform spawnPoint;
    public GameObject bulletPrefab;

    private float secondsBetweenShots;
    private float nextPossibleShootTime;
    private bool isShootingAllowed = false;
    private AudioSource audioSource;

    void Start()
    {
        secondsBetweenShots = 60f / rpm;
        audioSource = GetComponent<AudioSource>();
    }

    public void Shoot()
    {
        if (!isShootingAllowed || Time.time < nextPossibleShootTime) return;

        if (audioSource) audioSource.Play();

        GameObject bullet = Instantiate(bulletPrefab, spawnPoint.position, spawnPoint.rotation);
        Rigidbody bulletRb = bullet.GetComponent<Rigidbody>();

        if (bulletRb)
        {
            bulletRb.isKinematic = false;
            bulletRb.useGravity = false;
            bulletRb.AddForce(spawnPoint.forward * 1000f, ForceMode.Impulse);
        }

        nextPossibleShootTime = Time.time + secondsBetweenShots;
    }

    public void ShootContinuous()
    {
        if (Time.time >= nextPossibleShootTime) Shoot();
    }

    public void SetShootingAllowed(bool allowed)
    {
        isShootingAllowed = allowed;
    }
}
