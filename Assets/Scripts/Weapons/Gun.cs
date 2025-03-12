using System.Collections;
using UnityEditor.Search;
using UnityEngine;

  [RequireComponent (typeof(AudioSource))]
public class Gun : MonoBehaviour
{
    public enum GunType {Semi,Burst,Auto}
    public GunType gunType;
    public float rpm;

    // Componenets
    public Transform spawn;
    public Transform shellEjectionPoint;
    public LineRenderer tracer;
    
    // System
    float shotDistance = 20;
    private float secondsBetweenShots;
    private float nextPossibleShootTime;
    
    void Start()
    {
        secondsBetweenShots = 60/rpm;
        if(GetComponent<LineRenderer>())
        {
            tracer = GetComponent<LineRenderer>();
        }
    }
    public void Shoot()
    {
        if(CanShoot())
        {
            Ray ray = new Ray(spawn.position,spawn.forward);
            RaycastHit hit;

            
            if(Physics.Raycast(ray, out hit, shotDistance))
            {
                shotDistance = hit.distance;
            }
        
        nextPossibleShootTime = Time.time + secondsBetweenShots;

        AudioSource audio = GetComponent<AudioSource>();
        audio.Play();

        if(tracer)
        {
            StartCoroutine("RenderTracer", ray.direction * shotDistance);
        }
        }

    }
    public void ShootContinuous()
    {
        if(gunType == GunType.Auto)
        {
            Shoot();
        }
    }
    private bool CanShoot()
    {
        bool CanShoot = true;
        
        if(Time.time < nextPossibleShootTime)
        {
            CanShoot = false;
        }
        return CanShoot;
    }
    IEnumerator RenderTracer(Vector3 hitPoint)
    {
        tracer.enabled = true;
        tracer.SetPosition(0,spawn.position);
        tracer.SetPosition(1,spawn.position + hitPoint);

        yield return new WaitForSeconds(2);
        tracer.enabled = false;
    }
}
