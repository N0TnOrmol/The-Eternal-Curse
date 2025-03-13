using UnityEngine;

public class WeaponManager : MonoBehaviour
{
    public GameObject gun;
    public GameObject meleeWeapon;
    public GameObject musket;
    public GameObject blunderbuss;

    private Gun gunScript;
    private Musket musketScript;
    private Blunderbuss blunderbussScript;

    private int currentWeaponIndex = 0;
    private GameObject[] weapons;

    void Start()
    {
        // Store weapons in an array for easier switching
        weapons = new GameObject[] { gun, meleeWeapon, musket, blunderbuss };

        // Get scripts if available
        if (gun) gunScript = gun.GetComponent<Gun>();
        if (musket) musketScript = musket.GetComponent<Musket>();
        if (blunderbuss) blunderbussScript = blunderbuss.GetComponent<Blunderbuss>();

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
            if (currentWeaponIndex == 2 && musketScript) musketScript.ShootContinuous();
            if (currentWeaponIndex == 3 && blunderbussScript) blunderbussScript.ShootContinuous();
        }
    }

    void SwitchWeapon()
    {
        // Deactivate all weapons
        foreach (GameObject weapon in weapons)
        {
            if (weapon) weapon.SetActive(false);
        }

        // Switch to the next weapon
        currentWeaponIndex = (currentWeaponIndex + 1) % weapons.Length;

        // Activate the new weapon
        if (weapons[currentWeaponIndex])
        {
            weapons[currentWeaponIndex].SetActive(true);
        }
    }

    void UpdateWeaponState()
    {
        // Deactivate all weapons except the currently selected one
        for (int i = 0; i < weapons.Length; i++)
        {
            if (weapons[i])
            {
                weapons[i].SetActive(i == currentWeaponIndex);
            }
        }
    }
}
