using System.Collections;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class Musket : MonoBehaviour
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
            tracer.enabled = false; // Disable tracer at start
        }
    }

    public void Shoot()
    {
        if (CanShoot())
        {
            Ray ray = new Ray(spawn.position, spawn.up);
            RaycastHit hit;
            Vector3 tracerEnd = spawn.position + spawn.up * shotDistance; // Default end point

            if (Physics.Raycast(ray, out hit, shotDistance))
            {
                tracerEnd = hit.point; // Stop tracer at the hit point

                // Damage the enemy if hit
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
                StartCoroutine(RenderTracer(tracerEnd)); // Draw tracer up to the hit point
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
        tracer.SetPosition(0, spawn.position); // Start at gun muzzle
        tracer.SetPosition(1, hitPoint);       // End at the hit point

        yield return new WaitForSeconds(0.05f); // Quick flash effect
        tracer.enabled = false;
    }
}
