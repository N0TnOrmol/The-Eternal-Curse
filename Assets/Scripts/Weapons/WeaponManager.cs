using UnityEngine;

public class WeaponManager : MonoBehaviour
{
    public GameObject gun;  // Reference to the gun
    public GameObject meleeWeapon;  // Reference to the melee weapon (saber)
    private Gun gunScript;  // Reference to the Gun script
    private bool isGunActive = true;  // Bool to track whether the gun is active or not

    void Start()
    {
        // Get the Gun component from the gun GameObject
        gunScript = gun.GetComponent<Gun>();  // Get the Gun script

        // Initially, enable the gun and disable the melee weapon
        gun.SetActive(true);
        meleeWeapon.SetActive(false);
        gunScript.enabled = true;  // Enable the gun shooting script
        gunScript.SetShootingAllowed(true);  // Allow shooting initially
    }

    void Update()
    {
        // Detect the weapon switch input (e.g., pressing the "Q" key)
        if (Input.GetKeyDown(KeyCode.Q))
        {
            SwitchWeapon();
        }

        // Allow shooting only if the gun is equipped and shooting is allowed
        if (isGunActive && Input.GetButton("Attack"))
        {
            gunScript.ShootContinuous(); // Call the shooting logic from the Gun script
        }
    }

    // Method to switch weapons
    void SwitchWeapon()
    {
        if (isGunActive)  // If the gun is currently active, switch to the melee weapon
        {
            gun.SetActive(false);  // Disable the gun
            meleeWeapon.SetActive(true);  // Enable the melee weapon
            gunScript.enabled = false;  // Disable the Gun script to stop shooting
            gunScript.SetShootingAllowed(false);  // Disable shooting
            isGunActive = false;  // Set the bool to false (melee weapon active)
            Debug.Log("Switched to melee weapon, gun is disabled.");
        }
        else  // If the melee weapon is active, switch to the gun
        {
            gun.SetActive(true);  // Enable the gun
            meleeWeapon.SetActive(false);  // Disable the melee weapon
            gunScript.enabled = true;  // Enable the Gun script to allow shooting
            gunScript.SetShootingAllowed(true);  // Allow shooting
            isGunActive = true;  // Set the bool to true (gun is active)
            Debug.Log("Switched to gun, gun is enabled.");
        }
    }
}
