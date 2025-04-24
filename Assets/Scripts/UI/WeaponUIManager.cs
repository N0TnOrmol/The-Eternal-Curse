using UnityEngine;
using UnityEngine.UI;

public class WeaponUIManager : MonoBehaviour
{
    public Image pistolUI;
    public Image musketUI;
    public Image blunderbussUI;
    public Image bombUI;

    // Hides all weapon UI elements
    public void HideAll()
    {
        pistolUI.enabled = false;
        musketUI.enabled = false;
        blunderbussUI.enabled = false;
        bombUI.enabled = false;
    }

    // Shows the UI based on the selected weapon index
    public void ShowWeaponUI(int index)
    {
        HideAll(); // First hide everything

        switch (index)
        {
            case 0:
                pistolUI.enabled = true;
                break;
            case 1:
                musketUI.enabled = true;
                break;
            case 2:
                blunderbussUI.enabled = true;
                break;
            case 3:
                bombUI.enabled = true;
                break;
        }
    }
}
