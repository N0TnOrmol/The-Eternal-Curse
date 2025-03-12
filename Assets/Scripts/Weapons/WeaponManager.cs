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

    void Start()
    {
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
        currentWeaponIndex = (currentWeaponIndex + 1) % 4;
        UpdateWeaponState();
    }

    void UpdateWeaponState()
    {
        gun.SetActive(currentWeaponIndex == 0);
        meleeWeapon.SetActive(currentWeaponIndex == 1);
        musket.SetActive(currentWeaponIndex == 2);
        blunderbuss.SetActive(currentWeaponIndex == 3);


    }
}
