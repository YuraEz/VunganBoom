using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class StoreLoader : MonoBehaviour
{
    [SerializeField] private Sprite[] bgList;
    [SerializeField] private Image bgImg;

    void Start()
    {
        int CurrentBG = PlayerPrefs.GetInt("CurrentBG", 0);
        bgImg.sprite = bgList[CurrentBG];
    }
}
