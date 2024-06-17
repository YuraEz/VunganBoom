using System;
using System.Collections;
using System.Collections.Generic;
using NaughtyAttributes;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ScoreManager : MonoBehaviour
{
    [ReadOnly, SerializeField] private int score;
    public int results;
    public int Score { get { return score; } }

    public Action<int> onBalanceChange;

    private void OnEnable()
    {
       // score = PlayerPrefs.GetInt("score", 0);
        ServiceLocator.AddService(this);
    }

    private void OnDisable()
    {
        ServiceLocator.RemoveService(this);
    }

    public void ChangeValue(int value)
    {
        score += value; 
        onBalanceChange?.Invoke(score);
    }

    public void FinishGame()
    {
        results = score;
        PlayerPrefs.SetInt("score", score + PlayerPrefs.GetInt("score", 0));
        if (PlayerPrefs.GetInt("record", 0) < results) PlayerPrefs.SetInt("record", results);
        if (results >= 7500) PlayerPrefs.SetInt("Goal6", PlayerPrefs.GetInt("Goal6", 0) + 1);
        if (results >= 500) PlayerPrefs.SetInt("Goal4", PlayerPrefs.GetInt("Goal4", 0) + 1);
        if (results >= 350) PlayerPrefs.SetInt("Goal2", PlayerPrefs.GetInt("Goal2", 0) + 1);

    }


    public int scoreInGameBfUpt;
    public void Finish(bool e)
    {

    }
    public void UpdateGame(int few)
    {

    }
    public void UpdateScore(int fw)
    {

    }
}
