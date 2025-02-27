using UnityEngine;

public class Gun : MonoBehaviour
{
    public enum GunType {Semi, Burst, Auto};
    public GunType gunType;
    public Transform spawn;
    public string Target = "Enemy";
    public void Shoot()
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
        }
        Debug.DrawRay(ray.origin,ray.direction * shotDistance,Color.red,1);
    }
    public void ShootContinuous()
    {
        if(gunType == GunType.Auto)
        {
            Shoot();
        }
    } 
    private void ShootRayCast()
    {

    }
}
