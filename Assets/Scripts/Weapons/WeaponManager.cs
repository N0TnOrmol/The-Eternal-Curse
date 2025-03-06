using UnityEngine;

public class WeaponManager : MonoBehaviour
{
    public GameObject gun;  // Reference to the gun
    public GameObject meleeWeapon;  // Reference to the melee weapon (saber)
    private Gun gunScript;  // Reference to the Gun script

    void Start()
    {
        // Get the Gun component from the gun GameObject
        gunScript = gun.GetComponent<Gun>();  // Get the Gun script

        // Initially, enable the gun and disable the melee weapon
        gun.SetActive(true);
        meleeWeapon.SetActive(false);
        gunScript.enabled = true;  // Enable the gun shooting script
    }

    void Update()
    {
        // Detect the weapon switch input (e.g., pressing the "Q" key)
        if (Input.GetKeyDown(KeyCode.Q))
        {
            SwitchWeapon();
        }

        // Allow shooting only if the gun is equipped
        if (gun.activeSelf)
        {
            if (Input.GetButton("Attack"))
            {
                gunScript.ShootContinuous(); // Call the shooting logic from the Gun script
            }
        }
    }

    // Method to switch weapons
    void SwitchWeapon()
    {
        if (gun.activeSelf)  // If the gun is currently active, switch to the melee weapon
        {
            gun.SetActive(false);  // Disable the gun
            meleeWeapon.SetActive(true);  // Enable the melee weapon
            gunScript.enabled = false;  // Disable the Gun script to stop shooting
        }
        else  // If the melee weapon is active, switch to the gun
        {
            gun.SetActive(true);  // Enable the gun
            meleeWeapon.SetActive(false);  // Disable the melee weapon
            gunScript.enabled = true;  // Enable the Gun script to allow shooting
        }
    }
}
