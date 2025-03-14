using System.Collections;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class Blunderbuss : MonoBehaviour
{
    public enum GunType { Semi, Burst, Auto }
    public GunType gunType;
    public float rpm;

    // Components
    public Transform spawn;
    public Transform shellEjectionPoint;
    public Rigidbody shell;
    public LineRenderer tracer;

    // System
    private float shotDistance = 20;
    private float secondsBetweenShots;
    private float nextPossibleShootTime;
    private AudioSource audioSource;
    private bool isPlayingAudio = false;

    void Start()
    {
        secondsBetweenShots = 60 / rpm;
        audioSource = GetComponent<AudioSource>();

        if (GetComponent<LineRenderer>())
        {
            tracer = GetComponent<LineRenderer>();
        }
    }

    public void Shoot()
    {
        if (CanShoot())
        {
            Ray ray = new Ray(spawn.position, spawn.up);
            RaycastHit hit;
            float tracerDistance = shotDistance; // Cache original shot distance

            if (Physics.Raycast(ray, out hit, shotDistance))
            {
                tracerDistance = hit.distance; // Update tracer distance

                // Check if we hit an enemy
                DmgHp enemy = hit.collider.GetComponent<DmgHp>();
                if (enemy != null)
                {
                    enemy.TakeDamageEnemy();
                }
            }

            nextPossibleShootTime = Time.time + secondsBetweenShots;
            isPlayingAudio = true;
            audioSource.Play();
            StartCoroutine(WaitForSoundToEnd());

            if (tracer)
            {
                StartCoroutine(RenderTracer(ray.direction * tracerDistance)); // Use cached distance
            }

            // Spawn multiple shells
            int shellCount = Random.Range(3, 6);
            for (int i = 0; i < shellCount; i++)
            {
                Rigidbody newShell = Instantiate(shell, shellEjectionPoint.position, Quaternion.identity);
                Vector3 randomForce = shellEjectionPoint.up * Random.Range(105f, 200f) +
                                    spawn.up * Random.Range(-20f, 20f) +
                                    spawn.right * Random.Range(-30f, 30f);
                newShell.AddForce(randomForce);
            }
        }
    }



    public void ShootContinuous()
    {
        if (gunType == GunType.Auto)
        {
            Shoot();
        }
    }

    private bool CanShoot()
    {
        return Time.time >= nextPossibleShootTime && !isPlayingAudio;
    }

    IEnumerator WaitForSoundToEnd()
    {
        yield return new WaitWhile(() => audioSource.isPlaying);
        isPlayingAudio = false;
    }

    IEnumerator RenderTracer(Vector3 hitPoint)
    {
        tracer.enabled = true;
        tracer.SetPosition(0, spawn.position);
        tracer.SetPosition(1, spawn.position + hitPoint);

        yield return new WaitForSeconds(0.5f);
        tracer.enabled = false;
    }
}
