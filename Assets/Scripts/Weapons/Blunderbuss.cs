using UnityEngine;

public class Blunderbuss : MonoBehaviour
{
    public float rpm = 20f;
    public Transform spawnPoint;
    public GameObject bulletPrefab;
    public int pelletCount = 6;
    public float spreadAngle = 10f;

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

        for (int i = 0; i < pelletCount; i++)
        {
            GameObject bullet = Instantiate(bulletPrefab, spawnPoint.position, spawnPoint.rotation);
            Rigidbody bulletRb = bullet.GetComponent<Rigidbody>();

            if (bulletRb)
            {
                bulletRb.isKinematic = false;
                bulletRb.useGravity = false;

                // Random spread direction
                Vector3 spread = spawnPoint.forward +
                                 new Vector3(Random.Range(-spreadAngle, spreadAngle) * 0.01f,
                                             Random.Range(-spreadAngle, spreadAngle) * 0.01f, 
                                             0);
                bulletRb.AddForce(spread.normalized * 800f, ForceMode.Impulse);
            }
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
