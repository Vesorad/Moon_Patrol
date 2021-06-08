using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameSession : MonoBehaviour
{
    [HideInInspector] public int score = 0;
    [HideInInspector] public int recordScore = 0;
    [HideInInspector] public int lastRecord;
    [HideInInspector] public string recordCheckpoint;

    private void Awake()
    {
        recordScore = PlayerPrefs.GetInt("Record");
        recordCheckpoint = PlayerPrefs.GetString("RecordPoint", recordCheckpoint);
    }
    private void Start()
    {
        score = 0;
    }

    private void Update()
    {
        if (recordScore < score)
        {
            lastRecord = recordScore;
            recordScore = score;
        }
    }

    public void AddToScore(int scoreValue)
    {
        score += scoreValue;
    }
    public void RestScore()
    {
        score = 0;
    }
    public void SaveRecordScore()
    {
        PlayerPrefs.SetInt("Record", recordScore);
        PlayerPrefs.SetString("RecordPoint", recordCheckpoint);
    }
}
