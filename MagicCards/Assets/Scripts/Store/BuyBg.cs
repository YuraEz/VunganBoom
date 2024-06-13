using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BuyBg : MonoBehaviour
{
    [SerializeField] private string bgPlayerPref;
    [SerializeField] private int bgPrice= 2500;
    private Button buyBtn;

    private ScoreManager scoreManager;

    private void Start()
    {
        buyBtn = GetComponent<Button>();
        scoreManager = ServiceLocator.GetService<ScoreManager>();
        buyBtn.onClick.AddListener(BuyBG);
        int hasBG = PlayerPrefs.GetInt(bgPlayerPref, 0);
        if (hasBG == 1) 
        {
            buyBtn.gameObject.SetActive(false);
        }
    }

    private void BuyBG()
    {
        print("Buy");
        if (PlayerPrefs.GetInt("score", 0) >= bgPrice)
        {
            scoreManager.UpdateScore(bgPrice);
            buyBtn.gameObject.SetActive(false);
            PlayerPrefs.SetInt(bgPlayerPref, 1);
        }
    }
}
