using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIPremiumMode : MonoBehaviour
{
    private Button buyBtn;

    private UIManager manager;
    private void OnEnable()
    {
        manager = ServiceLocator.GetService<UIManager>();

        buyBtn = GetComponent<Button>();

        buyBtn.onClick.AddListener(ChangeUI);

        int hasBG = PlayerPrefs.GetInt("PremiumIsPurchased", 0);
        if (hasBG == 1)
        {
            buyBtn.gameObject.SetActive(false);
        }
    }

    void ChangeUI()
    {
        manager.ChangeScreen("premium");
    }

}
