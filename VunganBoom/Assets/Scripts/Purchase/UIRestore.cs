using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIRestore : MonoBehaviour
{
    private Button buyBtn;

  
    private void OnEnable()
    {
        buyBtn = GetComponent<Button>();

        buyBtn.onClick.AddListener(ChangeUI);
    }

    void ChangeUI()
    {
        PurchaseManager.Instance.ReactivatePurchases();
    }
}
