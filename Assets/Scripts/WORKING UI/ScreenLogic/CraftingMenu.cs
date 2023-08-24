using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CraftingMenu : MonoBehaviour
{
    [SerializeField] private GameObject craftingScreen;
    [SerializeField] private GameObject reforgeScreen;

    [SerializeField] private GameObject noWeaponEquippedGO;

    // Start is called before the first frame update
    public void CraftButton()
    {
        ScreenManager.Instance.Push(craftingScreen);
    }

    public void ReforgeButton()
    {
        if(ScreenManager.Instance.EquippedWeapon == null)
        {
            noWeaponEquippedGO.GetComponent<NoWeaponEquippedIcon>().ActivateIcon();
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
