using UnityEngine;

public class WeaponManager : MonoBehaviour
{
    // References to the three weapon GameObjects
    public GameObject gun;
    public GameObject meleeWeapon;
    public GameObject musket;

    // Scripts for the gun and musket (since both are ranged weapons)
    private Gun gunScript;
    private Musket musketScript;

    // Current weapon index (0 = Gun, 1 = Melee, 2 = Musket)
    private int currentWeaponIndex = 0;

    void Start()
    {
        // Get the gun and musket scripts to enable/disable shooting
        gunScript = gun.GetComponent<Gun>();
        musketScript = musket.GetComponent<Musket>();

        // Set the default weapon as the gun
        UpdateWeaponState();
    }

    void Update()
    {
        // If the player presses the weapon switch key (Q), cycle through the weapons
        if (Input.GetKeyDown(KeyCode.Q))
        {
            SwitchWeapon();
        }

        // Handle shooting when using a ranged weapon
        if (currentWeaponIndex == 0 && Input.GetButton("Attack"))
        {
            gunScript.ShootContinuous();  // Gun shooting
        }
        else if (currentWeaponIndex == 2 && Input.GetButton("Attack"))
        {
            musketScript.ShootContinuous();  // Musket shooting
        }
    }

    // Switches to the next weapon in the cycle (Gun → Melee → Musket → Gun)
    void SwitchWeapon()
    {
        currentWeaponIndex = (currentWeaponIndex + 1) % 3;  // Cycle between 0, 1, and 2
        UpdateWeaponState();
    }

    // Updates which weapon is active and sets shooting permissions accordingly
    void UpdateWeaponState()
    {
        // Enable the currently selected weapon and disable the others
        gun.SetActive(currentWeaponIndex == 0);
        meleeWeapon.SetActive(currentWeaponIndex == 1);
        musket.SetActive(currentWeaponIndex == 2);

        // Enable shooting only for the active ranged weapon
        gunScript.enabled = (currentWeaponIndex == 0);
        musketScript.enabled = (currentWeaponIndex == 2);

        // Allow shooting only for the active ranged weapon
        gunScript.SetShootingAllowed(currentWeaponIndex == 0);
        musketScript.SetShootingAllowed(currentWeaponIndex == 2);

        // Log the currently selected weapon
        string weaponName = currentWeaponIndex == 0 ? "Gun" : currentWeaponIndex == 1 ? "Melee" : "Musket";
        Debug.Log("Switched to: " + weaponName);
    }
}
