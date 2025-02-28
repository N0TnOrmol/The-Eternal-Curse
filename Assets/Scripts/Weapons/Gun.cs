using UnityEngine;
using System.Collections;

[RequireComponent (typeof(AudioSource))]
public class Gun : MonoBehaviour
{
    public enum GunType {Semi, Burst, Auto};
    public GunType gunType;
    public float rpm;
    public Transform spawn;
    // System:
    private float SecondsBetweenShots;
    private float nextPossibleShootTime;
    public string Target = "Enemy";

    void Start()
    {
        SecondsBetweenShots = 10/rpm;
    }
    public void Shoot()
    {
        if (CanShoot())
        {
            Ray ray = new Ray(spawn.position,spawn.forward);
            RaycastHit hit;
            float shotDistance = 20;
            if(Physics.Raycast(ray,out hit,shotDistance))
            {
                shotDistance = hit.distance;
                if (hit.collider.CompareTag(Target))
                {
                    DmgHp dmgHp = hit.collider.GetComponent<DmgHp>();
                    if (dmgHp != null)
                    {
                        dmgHp.TakeDamage();
                    }
                }
                Debug.DrawRay(ray.origin,ray.direction * shotDistance,Color.red,1);
                nextPossibleShootTime = Time.time + SecondsBetweenShots;
                GetComponent<AudioSource>().Play();
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
    private void ShootRayCast()
    {

    }
    
}