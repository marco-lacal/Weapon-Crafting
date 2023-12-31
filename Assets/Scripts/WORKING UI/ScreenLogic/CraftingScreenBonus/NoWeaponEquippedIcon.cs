using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class NoWeaponEquippedIcon : MonoBehaviour
{
    private Image icon;
    private TextMeshProUGUI text;

    private Color minOpacityIcon;
    private Color minOpacityText;

    private Color maxOpacityIcon;
    //maxOpacityText is Color.black

    private Coroutine fading;

    void Awake()
    {
        icon = transform.GetComponent<Image>();
        text = transform.GetChild(0).GetComponent<TextMeshProUGUI>();

        minOpacityIcon = icon.color;
        minOpacityIcon.a = 0f;
        minOpacityText = text.color;
        minOpacityText.a = 0f;

        maxOpacityIcon = icon.color;
    }

    public void ActivateIcon()
    {
        if(!transform.gameObject.activeSelf)
        {
            transform.gameObject.SetActive(true);
        }

        if(fading == null)
        {
            fading = StartCoroutine(ThenFadeIcon());
        }
        else
        {
            StopCoroutine(fading);
            fading = StartCoroutine(ThenFadeIcon());
        }
    }

    IEnumerator ThenFadeIcon()
    {
        icon.color = maxOpacityIcon;
        text.color = Color.black;

        float oneSecond = 0;

        while(oneSecond < 1f)
        {
            oneSecond += Time.unscaledDeltaTime;

            yield return null;
        }

        while(icon.color != minOpacityIcon)
        {
            icon.color = Color.Lerp(icon.color, minOpacityIcon, Time.fixedUnscaledDeltaTime / 2f);
            text.color = Color.Lerp(text.color, minOpacityText, Time.fixedUnscaledDeltaTime / 2f);

            yield return null;
        }

        transform.gameObject.SetActive(false);
    }
}
