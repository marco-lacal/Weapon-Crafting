using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;

public class PartsCollectionBox : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    [SerializeField] private RectTransform backDrop;
    [SerializeField] private Transform WPSHolder;

    [SerializeField] private GameObject WPS;

    private List<GameObject> WPSList;
    private CanvasGroup entireUI;
    private Coroutine fading;

    //will get this from some variable 2d array that stores what parts are collected and what arent. 0 = not, 1 = collected
    private int[][] partsCollection;

    public void MakeCollectionBoxSizeAndWPS(int[][] partsArray)
    {
        WPSList = new List<GameObject>();
        entireUI = transform.GetComponent<CanvasGroup>();

        partsCollection = partsArray;

        backDrop.sizeDelta = new Vector2(backDrop.sizeDelta.x + 30 * (partsCollection[0].Length - 1), backDrop.sizeDelta.y);

        CreateWeaponModelSheets();
    }

    private void CreateWeaponModelSheets()
    {
        for(int i = 0; i < partsCollection[0].Length; i++)
        {
            GameObject temp = Instantiate(WPS, WPSHolder);

            temp.GetComponentInChildren<TextMeshProUGUI>().text = (i+1).ToString();
            temp.transform.localPosition = new Vector2(30 * i, 0);

            WPSList.Add(temp);
        }

        for(int i = 0; i < partsCollection[0].Length; i++)
        {
            Transform basePlate = WPSList[i].transform.GetChild(1);
            
            for(int j = 0; j < partsCollection.Length; j++)
            {
                basePlate.GetChild(j).gameObject.SetActive(partsCollection[j][i] == 0 ?  false : true);
            }
        }
    }

    void OnEnable()
    {
        entireUI.alpha = 1;
    }

    void Destroy()
    {
        foreach(GameObject go in WPSList)
        {
            Destroy(go);
        }

        WPSList.Clear();
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        if(fading != null)
        {
            StopCoroutine(fading);
            entireUI.alpha = 1f;
        }
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        Debug.Log("Goodbye");

        if(fading == null)
        {
            fading = StartCoroutine(FadeAway());
        }
        else
        {
            StopCoroutine(fading);
            fading = StartCoroutine(FadeAway());
        }
    }

    IEnumerator FadeAway()
    {
        entireUI.alpha = 1f;

        yield return new WaitForSecondsRealtime(2f);

        float count = .00003f;

        while(entireUI.alpha > 0.1f)
        {
            entireUI.alpha = Mathf.Lerp(entireUI.alpha, 0f, count);
            count += 0.00003f;

            yield return null;
        }

        transform.gameObject.SetActive(false);
    }

    // [SerializeField] private RectTransform backDrop;
    // [SerializeField] private Transform WPSHolder;

    // [SerializeField] private GameObject WPS;

    // [Header("Only here while testing")]
    // [SerializeField] private int numWeapons;

    // private List<GameObject> WPSList;
    // private CanvasGroup entireUI;
    // private Coroutine fading;

    // //will get this from some variable 2d array that stores what parts are collected and what arent. 0 = not, 1 = collected
    // private int[,] tempCollection;

    // // Start is called before the first frame update
    // void Start()
    // {
    //     WPSList = new List<GameObject>();
    //     entireUI = transform.GetComponent<CanvasGroup>();

    //     tempCollection = new int[numWeapons, 6];

    //     for(int i = 0; i < numWeapons; i++)
    //     {
    //         for(int j = 0; j < 6; j++)
    //         {
    //             tempCollection[i, j] = UnityEngine.Random.Range(0, 2);
    //         }
    //     }

    //     backDrop.sizeDelta = new Vector2(150 + 30 * (numWeapons - 1), backDrop.sizeDelta.y);

    //     CreateWeaponModelSheets();
    // }

    // private void CreateWeaponModelSheets()
    // {
    //     for(int i = 0; i < numWeapons; i++)
    //     {
    //         GameObject temp = Instantiate(WPS, WPSHolder);

    //         temp.GetComponentInChildren<TextMeshProUGUI>().text = (i+1).ToString();
    //         temp.transform.localPosition = new UnityEngine.Vector2(30 * i, 0);

    //         WPSList.Add(temp);
    //     }

    //     for(int i = 0; i < numWeapons; i++)
    //     {
    //         Transform basePlate = WPSList[i].transform.GetChild(0);
            
    //         for(int j = 0; j < 6; j++)
    //         {
    //             basePlate.GetChild(j).gameObject.SetActive(tempCollection[i, j] == 0 ?  false : true);
    //         }
    //     }
    // }

    // void OnDisable()
    // {
    //     foreach(GameObject go in WPSList)
    //     {
    //         Destroy(go);
    //     }

    //     WPSList.Clear();
    // }

    // public void OnPointerEnter(PointerEventData eventData)
    // {
    //     Debug.Log("Hello");

    //     if(fading != null)
    //     {
    //         StopCoroutine(fading);
    //         entireUI.alpha = 1f;
    //     }
    // }

    // public void OnPointerExit(PointerEventData eventData)
    // {
    //     Debug.Log("Goodbye");

    //     if(fading == null)
    //     {
    //         fading = StartCoroutine(FadeAway());
    //     }
    //     else
    //     {
    //         StopCoroutine(fading);
    //         fading = StartCoroutine(FadeAway());
    //     }
    // }

    // IEnumerator FadeAway()
    // {
    //     entireUI.alpha = 1f;

    //     yield return new WaitForSecondsRealtime(2f);

    //     float count = .00003f;

    //     while(entireUI.alpha > 0.1f)
    //     {
    //         entireUI.alpha = Mathf.Lerp(entireUI.alpha, 0f, count);
    //         count += 0.00003f;

    //         yield return null;
    //     }

    //     transform.gameObject.SetActive(false);
    // }
}
