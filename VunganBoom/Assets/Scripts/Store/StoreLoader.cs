using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class StoreLoader : MonoBehaviour
{
    [SerializeField] private Sprite[] bgList;
    [SerializeField] private Rockets[] rockets;
    [SerializeField] private Image bgImg;
    [SerializeField] private Gun gun;

    void Start()
    {
        int CurrentBG = PlayerPrefs.GetInt("CurrentBG", 0);
        int CurrentRocket = PlayerPrefs.GetInt("CurrentRocket", 0);

        gun.colorSprites = rockets[CurrentRocket].rocketsModel;
        bgImg.sprite = bgList[CurrentBG];

        gun.PlaceNewBall();
    }
}
