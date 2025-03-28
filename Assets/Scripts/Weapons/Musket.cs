using System.Collections;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class Musket : MonoBehaviour
{
    public float rpm;
    public Transform spawn;
    public Transform shellEjectionPoint;
    public Rigidbody shell;
    public LineRenderer tracer;
    private Animator animator;

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

        animator = GetComponentInParent<Animator>();
    }

    public void Shoot()
    {
        if (CanShoot())
        {
            if (animator != null)
            {
                animator.SetBool("IsShooting_Musket", true);
                StartCoroutine(StopShootingAnimation("IsShooting_Musket"));
            }

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
            DmgHp enemy = hit.collider.GetComponent<DmgHp>();
            if (enemy != null)
            {
                enemy.TakeDamageEnemy();
            }

            if (hit.collider.CompareTag("Explosive"))
            {
                PowderKeg keg = hit.collider.GetComponent<PowderKeg>();
                if (keg != null)
                {
                    keg.Explode();
                }
            }
        }

        nextPossibleShootTime = Time.time + secondsBetweenShots;
        isPlayingAudio = true;
        audioSource.Play();
        StartCoroutine(WaitForSoundToEnd());

        if (tracer != null)
        {
            StartCoroutine(RenderTracer(endPosition));
        }

        Rigidbody newShell = Instantiate(shell, shellEjectionPoint.position, Quaternion.identity);
        newShell.AddForce(shellEjectionPoint.up * Random.Range(105f, 200f));
    }

    public void ResetShootingState()
    {
        nextPossibleShootTime = Time.time;
        isPlayingAudio = false;
        if (animator != null)
        {
            animator.SetBool("IsShooting_Musket", false);
        }
    }

    private bool CanShoot()
    {
        return Time.time >= nextPossibleShootTime && !isPlayingAudio;
    }

    IEnumerator StopShootingAnimation(string parameterName)
    {
        yield return new WaitForSeconds(0.1f); // Adjust time as needed
        if (animator != null)
        {
            animator.SetBool(parameterName, false);
        }
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
        tracer.SetPosition(1, hitPoint);
        yield return new WaitForSeconds(0.1f);
        tracer.enabled = false;
    }
}
