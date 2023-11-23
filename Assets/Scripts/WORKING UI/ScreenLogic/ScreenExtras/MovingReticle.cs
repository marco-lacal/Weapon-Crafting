using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingReticle : MonoBehaviour, ZoomObserver, ShootObserver
{

    [SerializeField] private Vector2[] startPositions;

    [SerializeField] private RectTransform top;     // 0
    [SerializeField] private RectTransform left;    // 1
    [SerializeField] private RectTransform bottom;  // 2
    [SerializeField] private RectTransform right;   // 3

    void OnEnable()
    {
        ScreenManager.Instance.WSM.GetComponent<WSMSubject>().AddSObserver((ShootObserver)this);
        ScreenManager.Instance.WSM.GetComponent<WSMSubject>().AddZObserver((ZoomObserver)this);
    }

    void OnDisable()
    {
        ScreenManager.Instance.WSM.GetComponent<WSMSubject>().RemoveSObserver((ShootObserver)this);
        ScreenManager.Instance.WSM.GetComponent<WSMSubject>().RemoveZObserver((ZoomObserver)this);
    }

    void Update()
    {
        // Lerp the reticle components to their ending position
        top.anchoredPosition = Vector2.Lerp(top.anchoredPosition, startPositions[0], 0.01f);
        bottom.anchoredPosition = Vector2.Lerp(bottom.anchoredPosition, startPositions[2], 0.01f);

        left.anchoredPosition = Vector2.Lerp(left.anchoredPosition, startPositions[1], 0.01f);
        right.anchoredPosition = Vector2.Lerp(right.anchoredPosition, startPositions[3], 0.01f);
    }

    // make the reticle jump be determined by recoil and stability

    public void OnNotify_ShootRecoil(int stability, int recoilControl, WeaponType firingType, bool isADS)
    {
        float isADSScalar = isADS ? 0.75f: 1;

        float xRecoil;
        float yRecoil;

        switch(firingType)
        {
            case WeaponType.FullAuto:
                xRecoil = (200 / recoilControl) * isADSScalar;
                yRecoil = (200 / stability) * isADSScalar;

                break;
            case WeaponType.BurstFire:
                xRecoil = (250 / recoilControl) * isADSScalar;
                yRecoil = (150 / stability) * isADSScalar;

                break;
            case WeaponType.SemiAuto:
                xRecoil = (300 / recoilControl) * isADSScalar;
                yRecoil = (150 / stability) * isADSScalar;

                break;
            default:
                Debug.Log("Big error");
                xRecoil = yRecoil = 10;
                break;
        }

        xRecoil *= 2;
        yRecoil *= 2;

        float recoilValue = xRecoil + yRecoil;

        // Add 
        top.anchoredPosition = new Vector2(top.anchoredPosition.x, top.anchoredPosition.y + recoilValue);
        bottom.anchoredPosition = new Vector2(bottom.anchoredPosition.x, bottom.anchoredPosition.y - recoilValue);

        left.anchoredPosition = new Vector2(left.anchoredPosition.x - recoilValue, left.anchoredPosition.y);
        right.anchoredPosition = new Vector2(right.anchoredPosition.x + recoilValue, right.anchoredPosition.y);
    }

    public void OnNotify_Shoot(){}

    public void OnNotify_ZoomIn(int zoom, float adsSpeed)
    {
        float newAlpha = gameObject.GetComponent<CanvasGroup>().alpha;
        newAlpha = Mathf.Lerp(newAlpha, 0, adsSpeed * 2);

        gameObject.GetComponent<CanvasGroup>().alpha = newAlpha;
    }

    public void OnNotify_ZoomOut(float adsSpeed)
    {
        float newAlpha = gameObject.GetComponent<CanvasGroup>().alpha;
        newAlpha = Mathf.Lerp(newAlpha, 1, adsSpeed * 0.5f);

        gameObject.GetComponent<CanvasGroup>().alpha = newAlpha;
    }
}
