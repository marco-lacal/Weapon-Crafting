using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CraftingMenu : MonoBehaviour
{
    [SerializeField] private GameObject craftingScreen;
    [SerializeField] private GameObject reforgeScreen;

    [SerializeField] private NoWeaponEquippedIcon noWeaponEquipped;

    // Start is called before the first frame update
    public void CraftButton()
    {
        ScreenManager.Instance.Push(craftingScreen);
    }

    public void ReforgeButton()
    {
        if(ScreenManager.Instance.WSM.GetComponent<WeaponStateManager>().EquippedWeapon == null)
        {
            noWeaponEquipped.ActivateIcon();
            return;
        }
        else
        {
            ScreenManager.Instance.Push(reforgeScreen);
        }
    }

    public void Cancel()
    {
        ScreenManager.Instance.PopScreen();
    }
}
