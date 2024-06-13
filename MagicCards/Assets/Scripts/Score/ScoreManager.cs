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
    public int Score { get { return score; } }

    public Action<int> onBalanceChange;

    private void OnEnable()
    {
        score = PlayerPrefs.GetInt("score", 0);
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
        PlayerPrefs.SetInt("score", score);
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
