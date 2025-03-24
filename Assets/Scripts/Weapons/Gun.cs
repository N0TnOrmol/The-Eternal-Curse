using System.Collections;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class Gun : MonoBehaviour
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
                StartCoroutine(RenderTracer(ray.origin + ray.direction * tracerDistance)); // Use cached distance
            }

            // Spawn ONE shell
            Rigidbody newShell = Instantiate(shell, shellEjectionPoint.position, Quaternion.identity);
            Vector3 shellForce = shellEjectionPoint.up * Random.Range(105f, 200f) +
                                spawn.up * Random.Range(-10f, 10f);
            newShell.AddForce(shellForce);
        }
    }

        public void ResetShootingState()
    {
        nextPossibleShootTime = Time.time; // Allows instant shooting after switching weapons
        isPlayingAudio = false; // Ensure audio doesn't interfere
    }

    IEnumerator RenderTracer(Vector3 hitPoint)
    {
        tracer.enabled = true;
        tracer.SetPosition(0, spawn.position);
        tracer.SetPosition(1, hitPoint); // Always extend to the full cached distance

        yield return new WaitForSeconds(0.5f);
        tracer.enabled = false;
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

}
