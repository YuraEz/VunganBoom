using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Purchasing.Extension;
using UnityEngine.Purchasing;

public class PurchaseManager : MonoBehaviour, IDetailedStoreListener
{
    private static PurchaseManager _instance;
    public IStoreController _storeController;
    private IExtensionProvider _storeExtensionProvider;

    public static PurchaseManager Instance
    {
        get
        {
            if (_instance == null)
            {
                GameObject iapManager = new GameObject("PurchaseManager");
                DontDestroyOnLoad(iapManager);
                _instance = iapManager.AddComponent<PurchaseManager>();
            }
            return _instance;
        }
    }

    private void Awake()
    {
        DontDestroyOnLoad(gameObject);
    }

    public void InitializePurchasing()
    {
        if (IsInitialized())
            return;

        var builder = ConfigurationBuilder.Instance(StandardPurchasingModule.Instance());
        builder.AddProduct("premium", ProductType.NonConsumable);

        UnityPurchasing.Initialize(this, builder);
    }

    public bool IsInitialized()
    {
        return _storeController != null && _storeExtensionProvider != null;
    }

    public void OnInitialized(IStoreController controller, IExtensionProvider extensions)
    {
        _storeController = controller;
        _storeExtensionProvider = extensions;
    }

    public void OnInitializeFailed(InitializationFailureReason reason, string msg)
    {
        Debug.Log($"IAP Initialization Failed: {reason.ToString()} - {msg}");
    }

    public PurchaseProcessingResult ProcessPurchase(PurchaseEventArgs args)
    {
        switch (args.purchasedProduct.definition.id)
        {
            case "premium":
                Debug.Log("premium successfully purchased!");
                //HERE CHANGES!!!!
                SuccsesPurchasePremium();
                //HERE CHANGES!!!!
                break;
            default:
                Debug.Log($"Unexpected product ID: {args.purchasedProduct.definition.id}");
                BuyPremium.Instance.ShowFailed();
                break;
        }
        return PurchaseProcessingResult.Complete;
    }

    public void OnPurchaseFailed(Product product, PurchaseFailureReason failureReason)
    {
        BuyPremium.Instance.ShowFailed();
        Debug.Log($"Purchase of {product.definition.id} failed due to {failureReason}");
    }

    private void SuccsesPurchasePremium()
    {
        BuyPremium.Instance.ShowSuccess();
        PlayerPrefs.SetInt("PremiumIsPurchased", 1);
        print("Обновился плеер преф");
       /// PhenixsMenedjer.Instance.DobavPhenixov(10);
        PremiumScreen.Instance.ShowChtoNado();
    }

    public void OnInitializeFailed(InitializationFailureReason error)
    {
        Debug.Log($"IAP Initialization Failed: {error.ToString()}");
    }

    public void ReactivatePurchases()
    {
        if (Application.platform == RuntimePlatform.IPhonePlayer ||
            Application.platform == RuntimePlatform.OSXPlayer ||
            Application.isEditor)
        {
            Debug.Log("Starting purchase restoration...");

            var apple = _storeExtensionProvider.GetExtension<IAppleExtensions>();
            apple.RestoreTransactions((result, error) =>
            {
                if (result)
                {
                    Debug.Log("Purchases successfully restored.");
                    SuccsesPurchasePremium();
                }
                else
                {
                    Debug.Log($"Failed to restore purchases. Error: {error}");
                }
            });
        }
        else
        {
            Debug.Log("Restore purchases is not supported on this platform.");
        }
    }



    public void OnPurchaseFailed(Product product, PurchaseFailureDescription failureDescription)
    {
        BuyPremium.Instance.ShowFailed();
        Debug.Log($"Purchase of {product.definition.id} failed due to {failureDescription}");
    }
}

