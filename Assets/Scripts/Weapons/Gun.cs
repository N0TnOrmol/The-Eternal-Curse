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
    private Animator animator; 

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
        tracer = GetComponent<LineRenderer>();
        animator = GetComponentInParent<Animator>();
    }

    public void Shoot()
    {
        if (CanShoot())
        {
            animator?.SetBool("IsShooting_Gun", true); 
            StartCoroutine(StopShootingAnimation("IsShooting_Gun"));

            HandleShootingLogic();
        }
    }

    private void HandleShootingLogic()
    {
        Ray ray = new Ray(spawn.position, spawn.up);
        RaycastHit hit;
        Vector3 endPosition = spawn.position + spawn.up * shotDistance;

        if (Physics.Raycast(ray, out hit, shotDistance))
        {
            endPosition = hit.point;
            if (hit.collider.GetComponent<DmgHp>() != null)
                hit.collider.GetComponent<DmgHp>().TakeDamageEnemy();
            else if (hit.collider.CompareTag("Explosive"))
                hit.collider.GetComponent<PowderKeg>()?.Explode();
        }

        nextPossibleShootTime = Time.time + secondsBetweenShots;
        isPlayingAudio = true;
        audioSource.Play();
        StartCoroutine(WaitForSoundToEnd());

        if (tracer) StartCoroutine(RenderTracer(endPosition));

        Rigidbody newShell = Instantiate(shell, shellEjectionPoint.position, Quaternion.identity);
        newShell.AddForce(shellEjectionPoint.up * Random.Range(105f, 200f));
    }

    public void ResetShootingState()
    {
        nextPossibleShootTime = Time.time;  
        isPlayingAudio = false;
        animator?.SetBool("IsShooting_Gun", false);  
    }

    IEnumerator StopShootingAnimation(string animationBool)
    {
        yield return new WaitForSeconds(0.2f);
        animator?.SetBool(animationBool, false);
    }

    IEnumerator RenderTracer(Vector3 hitPoint)
    {
        tracer.enabled = true;
        tracer.SetPositions(new Vector3[] { spawn.position, hitPoint });
        yield return new WaitForSeconds(0.1f);
        tracer.enabled = false;
    }

    private bool CanShoot() => Time.time >= nextPossibleShootTime && !isPlayingAudio;

    IEnumerator WaitForSoundToEnd()
    {
        yield return new WaitWhile(() => audioSource.isPlaying);
        isPlayingAudio = false;
    }
}
