using TMPro;
using UnityEngine;
using UnityEngine.UI;

public enum SelectionType
{
    Rifle,
    SMG,
    Pistol,
    Melee
}

public class CraftingScreen : MonoBehaviour
{
    [SerializeField] private Transform descriptionObject;
    [SerializeField] private Transform craftButton;

    [Header("Crafting Option Screens")]
    [SerializeField] private GameObject RifleScreen;
    [SerializeField] private GameObject SMGScreen;
    [SerializeField] private GameObject PistolScreen;
    [SerializeField] private GameObject SwordScreen;

    private bool isButtonPressed;
    private string selectedOption;
    private int optionNum;
    private TextMeshProUGUI descriptionText;

    void Awake()
    {
        isButtonPressed = false;

        descriptionText = descriptionObject.GetChild(0).GetComponent<TextMeshProUGUI>();

        Debug.Log(descriptionText);
    }

    public void OnOptionClick(Transform buttonPressed)
    {
        if(!isButtonPressed)
        {
            isButtonPressed = true;

            descriptionObject.gameObject.SetActive(true);
            craftButton.gameObject.SetActive(true);
        }

        descriptionObject.GetComponent<Image>().color = buttonPressed.gameObject.GetComponent<Image>().color;
        craftButton.GetComponent<Image>().color = buttonPressed.gameObject.GetComponent<Image>().color;

        selectedOption = buttonPressed.name.Replace("Button", "");

        descriptionText.text = "Completed " + selectedOption + " Frames: x <br><br> Total " + selectedOption + " Parts: y";
    }

    public void OnCraftClick()
    {
        Debug.Log("Going to the " + selectedOption + " crafting section");

        switch(selectedOption)
        {
            case "Rifle":
                ScreenManager.Instance.Push(RifleScreen);
                break;
            case "SMG":
                ScreenManager.Instance.Push(SMGScreen);
                break;
            case "Pistol":
                ScreenManager.Instance.Push(PistolScreen);
                break;
            case "Sword":
                ScreenManager.Instance.Push(SwordScreen);
                break;
            default:
                break;
        }
    }

    public void Cancel()
    {
        ScreenManager.Instance.PopScreen();
    }
}