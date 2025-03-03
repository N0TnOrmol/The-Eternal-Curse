using UnityEngine;
using System.Collections;

[RequireComponent(typeof(AudioSource))]
public class Gun : MonoBehaviour
{
    public enum GunType { Semi, Burst, Auto };
    public GunType gunType;
    public float rpm;
    // Components
    public Transform Spawn;
    private LineRenderer tracer;
    // System:
    private float secondsBetweenShots;
    private float nextPossibleShootTime;
    public string Target = "Enemy";

    void Start()
    {
        secondsBetweenShots = 60 / rpm;
        if (GetComponent<LineRenderer>())
        {
            tracer = GetComponent<LineRenderer>();
            tracer.enabled = false; // Start with LineRenderer disabled
            tracer.startWidth = 0.05f; // Ensure it's visible
            tracer.endWidth = 0.05f;
            tracer.material = new Material(Shader.Find("Unlit/Color"));
            tracer.material.color = Color.red; // Set color for testing
        }
    }

    public void Shoot()
    {
        if (CanShoot())
        {
            Ray ray = new Ray(Spawn.position, Spawn.forward);
            RaycastHit hit;
            float shotDistance = 20;

            if (Physics.Raycast(ray, out hit, shotDistance))
            {
                shotDistance = hit.distance;

                // Process the hit object
                if (hit.collider.CompareTag(Target))
                {
                    DmgHp dmgHp = hit.collider.GetComponent<DmgHp>();
                    if (dmgHp != null)
                    {
                        dmgHp.TakeDamage();
                    }
                }

                nextPossibleShootTime = Time.time + secondsBetweenShots;
                GetComponent<AudioSource>().Play();

                // Start tracer effect
                if (tracer)
                {
                    Debug.Log("shoot");
                    StartCoroutine(RenderTracer(hit.point)); // Use hit.point directly for accuracy
                }
            }
        }
    }

    public void ShootContinuous()  // Ensure this method is public
    {
        if (gunType == GunType.Auto)
        {
            Shoot(); // Call Shoot method in Auto mode
        }
    }

    private bool CanShoot()
    {
        return Time.time >= nextPossibleShootTime;
    }

    IEnumerator RenderTracer(Vector3 hitPoint)
    {
        tracer.enabled = true;
        tracer.SetPosition(0, Spawn.position);
        tracer.SetPosition(1, hitPoint); // Directly set the hit point for tracer end
        yield return new WaitForSeconds(0.1f); // Shorter time to render, you can adjust this
        tracer.enabled = false;
    }
}
