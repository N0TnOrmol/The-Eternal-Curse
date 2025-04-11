using System.Collections;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class Gun : MonoBehaviour
{
    public enum GunType { Semi, Burst, Auto }
    public GunType gunType;
    public float rpm;
    public Transform spawn;
    public Transform shellEjectionPoint;
    public Rigidbody shell;
    public LineRenderer tracer;
    private Animator animator;
    private float shotDistance = 20f;
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
        if (PauseGame.isPaused) return;

        if (CanShoot())
        {
            animator.SetBool("IsShooting_Gun", true);
            StartCoroutine(StopShootingAnimation("IsShooting_Gun"));
            HandleShootingLogic();
        }
    }
    private void HandleShootingLogic()
    {
        Vector3 direction = GetMouseAimDirection();
        Ray ray = new Ray(spawn.position, direction);
        RaycastHit hit;
        Vector3 endPosition = spawn.position + direction * shotDistance;
        if (Physics.Raycast(ray, out hit, shotDistance))
        {
            endPosition = hit.point;

            if (hit.collider.TryGetComponent(out DmgHp enemy))
                enemy.TakeDamageEnemy();

            if (hit.collider.CompareTag("Explosive"))
                hit.collider.GetComponent<PowderKeg>().Explode();
        }
        nextPossibleShootTime = Time.time + secondsBetweenShots;
        isPlayingAudio = true;
        audioSource.Play();
        StartCoroutine(WaitForSoundToEnd());
        if (tracer) StartCoroutine(RenderTracer(endPosition));
        Rigidbody newShell = Instantiate(shell, shellEjectionPoint.position, Quaternion.identity);
        newShell.AddForce(shellEjectionPoint.up * Random.Range(105f, 200f));
    }
    private Vector3 GetMouseAimDirection()
    {
        Ray camRay = Camera.main.ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast(camRay, out RaycastHit camHit, Mathf.Infinity, LayerMask.GetMask("Ground")))
        {
            return (camHit.point - spawn.position).normalized;
        }
        return spawn.forward;
    }
    public void ResetShootingState()
    {
        nextPossibleShootTime = Time.time;
        isPlayingAudio = false;
        animator.SetBool("IsShooting_Gun", false);
    }
    private bool CanShoot() => Time.time >= nextPossibleShootTime && !isPlayingAudio;
    IEnumerator StopShootingAnimation(string animationBool)
    {
        yield return new WaitForSecondsRealtime(0.2f);
        animator.SetBool(animationBool, false);
    }
    IEnumerator RenderTracer(Vector3 hitPoint)
    {
        tracer.enabled = true;
        tracer.SetPositions(new Vector3[] { spawn.position, hitPoint });
        yield return new WaitForSecondsRealtime(0.1f);
        tracer.enabled = false;
    }
    IEnumerator WaitForSoundToEnd()
    {
        yield return new WaitUntil(() => !audioSource.isPlaying);
        isPlayingAudio = false;
    }
}
