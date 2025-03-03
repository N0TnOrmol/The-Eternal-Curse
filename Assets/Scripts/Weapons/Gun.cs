using UnityEngine;
using System.Collections;

[RequireComponent(typeof(AudioSource))]
public class Gun : MonoBehaviour
{
    public enum GunType { Semi, Burst, Auto };
    public GunType gunType;
    // Components
    public Transform Spawn;
    // System:
    private float nextPossibleShootTime;
    public string Target = "Enemy";

    void Start()
    {

    }

    public void Shoot()
    {
        // Check if audio is playing, if so, don't shoot
        if (GetComponent<AudioSource>().isPlaying)
            return; // Early return to prevent shooting if audio is playing

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
                GetComponent<AudioSource>().Play();
                Debug.DrawRay(ray.origin, ray.direction * shotDistance,Color.red,1);

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


}
