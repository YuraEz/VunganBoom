using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class TakeGoal : MonoBehaviour
{
    public string alrDonePlayerPref;
    public string goalTaken;
   // public Button button;
    public int bonus;

    public int needAmount;
    public TextMeshProUGUI text; 
    public Slider slider;

    private ScoreManager scoreManager;

    //[Space]
   // public TextMeshProUGUI takenText;
   // public string taken;

    //private void OnEnable()
    //{
    //    slider.value = (float)PlayerPrefs.GetInt(alrDonePlayerPref, 0) / needAmount;
    //    text.text = $"{PlayerPrefs.GetInt(alrDonePlayerPref, 0)}/{needAmount}";

    //      if (PlayerPrefs.GetInt(alrDonePlayerPref, 0) < needAmount) return;
    //      button.onClick.AddListener(getGoal);
    //if (PlayerPrefs.GetInt(goalTaken, 0) == 1) return;

    // }

    //  void getGoal()
    //  {
    //      if (PlayerPrefs.GetInt(goalTaken, 0) == 1) return;
    //     scoreManager.increaseValue(bonus);
    //     PlayerPrefs.SetInt(goalTaken, 1);
    // }

    private void OnEnable()
    {
        UpdateUI();
        CheckGoalCompletion();
    }

    private void UpdateUI()
    {
        int progress = PlayerPrefs.GetInt(alrDonePlayerPref, 0);
        slider.value = (float)progress / needAmount;
        text.text = $"{progress}/{needAmount}";
        if (PlayerPrefs.GetInt(goalTaken, 0) == 1)
        {
     //       takenText.SetText(taken);
        }
    }

    private void CheckGoalCompletion()
    {
        int progress = PlayerPrefs.GetInt(alrDonePlayerPref, 0);
        int goalStatus = PlayerPrefs.GetInt(goalTaken, 0);

      //  button.onClick.RemoveAllListeners(); // Удалить всех слушателей

        if (progress >= needAmount && goalStatus == 0)
        {
       //     button.onClick.AddListener(getGoal);
        }
    }

    void getGoal()
    {
        if (PlayerPrefs.GetInt(goalTaken, 0) == 1) return;

       // scoreManager.increaseValue(bonus);
        //scoreManager.scoreText.SetText($"{bonus}");
        PlayerPrefs.SetInt(goalTaken, 1);

        // Обновляем UI и проверяем условия после получения награды
        UpdateUI();
        CheckGoalCompletion();
    }
}
