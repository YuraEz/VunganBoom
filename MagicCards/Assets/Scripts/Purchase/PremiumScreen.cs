using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Purchasing;

public class PremiumScreen : MonoBehaviour
{
    public static PremiumScreen Instance;
    public GameObject kuplenoOkno;
    public GameObject kupitTytOkno;

    public Button kupitPremiumKnopka;

    private void Awake()
    {
        kupitPremiumKnopka.onClick.AddListener(KupitPremium);
        PurchaseManager.Instance.InitializePurchasing();

        if (Instance != null)
        {
            Destroy(gameObject);
        }
        else
        {
            Instance = this;
        }
    }

    private void OnEnable()
    {
        ShowChtoNado();
    }

    public void ShowChtoNado()
    {
        bool kuplenPremium = PlayerPrefs.GetInt("PremiumIsPurchased", 0) == 1;
        if (kuplenPremium)
        {
            kuplenoOkno.SetActive(true);
            kupitTytOkno.SetActive(false);
        }
        else
        {
            kuplenoOkno.SetActive(false);
            kupitTytOkno.SetActive(true);
        }
    }

    public void KupitPremium()
    {
        TryBuyProduct("premium");
    }

    private void TryBuyProduct(string stringId)
    {
        if (!PurchaseManager.Instance.IsInitialized())
        {
            Debug.Log("IAP is not initialized.");
            BuyPremium.Instance.ShowFailed();
            return;
        }

        Product product = PurchaseManager.Instance._storeController.products.WithID(stringId);

        BuyPremium.Instance.ShowLoading();

        if (product != null && product.availableToPurchase)
        {
            Debug.Log($"Purchasing product asynchronously: '{product.definition.id}'");
            PurchaseManager.Instance._storeController.InitiatePurchase(product);
        }
        else
        {
            Debug.Log($"Could not initiate purchase for product ID: {stringId}. It might not be available for purchase.");
            BuyPremium.Instance.ShowFailed();
        }
    }
}
