using UnityEngine;

public class WeaponManager : MonoBehaviour
{
    public GameObject gun;
    public GameObject meleeWeapon;
    private Gun gunScript;
    private int currentWeaponIndex = 0;

    void Start()
    {
        if (gun) gunScript = gun.GetComponent<Gun>();
        UpdateWeaponState();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Q))
        {
            SwitchWeapon();
        }

        if (Input.GetButton("Attack"))
        {
            if (currentWeaponIndex == 0 && gunScript) gunScript.ShootContinuous();
        }
    }

    void SwitchWeapon()
    {
        currentWeaponIndex = (currentWeaponIndex + 1) % 4;
        UpdateWeaponState();
    }

    void UpdateWeaponState()
    {
        gun.SetActive(currentWeaponIndex == 0);
        meleeWeapon.SetActive(currentWeaponIndex == 1);

        if (gunScript) gunScript.SetShootingAllowed(currentWeaponIndex == 0);
    }
}