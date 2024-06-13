using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Purchasing;

public class CarouselsSkinsManager : MonoBehaviour
{
    [SerializeField] private CarouselInfo[] allSkins;
    [SerializeField] private CarouselInfo currentSkin;

   // private MoneyManager moneyManager;

    public Action<CarouselInfo> onSkinEquip;

    public CarouselInfo[] AllSkins { get => allSkins; }
    public CarouselInfo CurrentSkin { get => currentSkin; }

   // [Inject]
    //private void Construct(MoneyManager moneyManager)
   // {
    //    this.moneyManager = moneyManager;
   // }

    public void EquipNew(int index, bool tryBuy = false)
    {
        CarouselInfo newSkin = allSkins[index];
        if (newSkin.RealMoney)
        {
            //int buyed = PlayerPrefs.GetInt(newSkin.BuyID, 0);
            //newSkin.Buyed = buyed == 1;

            if (newSkin.Buyed == false)
            {
                TryBuyProduct(newSkin.BuyID);
                //newSkin.Buyed = true;
                //PlayerPrefs.SetInt(newSkin.BuyID, 1);
            }
        }
        else if (tryBuy && !newSkin.Buyed)
        {
        //    if (moneyManager.CurMoney < newSkin.Cost) return;

     //       moneyManager.CurMoney -= newSkin.Cost;
      ///      newSkin.Buyed = true;
        }

        currentSkin = newSkin;
        onSkinEquip?.Invoke(currentSkin);
    }

    private void TryBuyProduct(string stringId)
    {
        if (!PurchaseManager.Instance.IsInitialized())
        {
            Debug.Log("IAP is not initialized.");
        //    PopukayuMagazinAz.Instance.ShowFailed();
            return;
        }

        Product product = PurchaseManager.Instance._storeController.products.WithID(stringId);

      //  PopukayuMagazinAz.Instance.ShowLoading();

        if (product != null && product.availableToPurchase)
        {
            Debug.Log($"Purchasing product asynchronously: '{product.definition.id}'");
            PurchaseManager.Instance._storeController.InitiatePurchase(product);
        }
        else
        {
            Debug.Log($"Could not initiate purchase for product ID: {stringId}. It might not be available for purchase.");
       //     PopukayuMagazinAz.Instance.ShowFailed();
        }
    }
}
