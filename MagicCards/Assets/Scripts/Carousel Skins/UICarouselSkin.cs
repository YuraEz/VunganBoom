using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UICarouselSkin : MonoBehaviour
{
    [SerializeField] private Image borderImg;
    [SerializeField] private Image centerImg;
    [SerializeField] private Image iconImg;
    [SerializeField] private TextMeshProUGUI costText;
    [SerializeField] private GameObject costObj;
    [SerializeField] private GameObject buyIcon;
    [SerializeField] private GameObject equipIcon;
    [SerializeField] private GameObject equiped;
    [SerializeField] private GameObject equiped1;
    [SerializeField] private GameObject equiped2;
    [SerializeField] private Button equipBtn;
    [SerializeField] private GameObject realmoney;

    private CarouselInfo skinInfo;
    private int skinInfoIndex;

    public Action<int> onEquip;

    private void Awake()
    {
        equipBtn.onClick.AddListener(() =>
        {
            onEquip?.Invoke(skinInfoIndex);
        });
    }

    public void SetSkin(int index, bool isEquiped, CarouselInfo skinInfo)
    {
        skinInfoIndex = index;
        this.skinInfo = skinInfo;

        borderImg.sprite = skinInfo.SkinBorder;
        centerImg.sprite = skinInfo.SkinCenter;
        
        if (skinInfo.SkinIcon)
        {
            iconImg.sprite = skinInfo.SkinIcon;
        }
        else
        {
            iconImg.gameObject.SetActive(false);
        }

        if (skinInfo.Buyed)
        {
            costText.text = "";
            costObj.SetActive(false);
            buyIcon.SetActive(false);
            realmoney.SetActive(false);

            if (isEquiped)
            {
                iconImg.gameObject.SetActive(false);
            }
        }
        else
        {
            costText.text = skinInfo.Cost.ToString();
            costText.gameObject.SetActive(!skinInfo.RealMoney);
            costObj.SetActive(!skinInfo.RealMoney);
            buyIcon.SetActive(true);
            realmoney.SetActive(skinInfo.RealMoney);
        }

        equiped.SetActive(isEquiped);
        equiped1.SetActive(isEquiped);
        equiped2.SetActive(isEquiped);
    }
}
