using UnityEngine;

public class WeaponManager : MonoBehaviour
{
    public GameObject gun;
    public GameObject saber;
    public GameObject musket;
    public GameObject blunderbuss;

    private int currentWeaponIndex = 0;
    private GameObject[] weapons;

    void Start()
    {
        weapons = new GameObject[] { gun, saber, musket, blunderbuss };
        UpdateWeaponState(); // Ensure only one weapon is active at start
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Q))
        {
            SwitchWeapon();
        }

        if (Input.GetButtonDown("Attack")) // "Fire1" is Unity's default for left-click
        {
            ShootCurrentWeapon();
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
        UpdateWeaponState();
    }

    void UpdateWeaponState()
    {
        for (int i = 0; i < weapons.Length; i++)
        {
            if (weapons[i])
            {
                weapons[i].SetActive(i == currentWeaponIndex);
            }
        }
    }

    void ShootCurrentWeapon()
    {
        GameObject activeWeapon = weapons[currentWeaponIndex];

        if (activeWeapon)
        {
            Gun gunScript = activeWeapon.GetComponent<Gun>();
            Musket musketScript = activeWeapon.GetComponent<Musket>();
            Blunderbuss blunderbussScript = activeWeapon.GetComponent<Blunderbuss>();

            if (gunScript)
            {
                Debug.Log("Shooting with Gun!");
                gunScript.Shoot();
            }
            else if (musketScript)
            {
                Debug.Log("Shooting with Musket!");
                musketScript.Shoot();
            }
            else if (blunderbussScript)
            {
                Debug.Log("Shooting with Blunderbuss!");
                blunderbussScript.Shoot();
            }
            else
            {
                Debug.LogWarning("No valid weapon script found on active weapon!");
            }
        }
        else
        {
            Debug.LogError("No active weapon found in WeaponManager!");
        }
    }
}
