using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ScoreDisplay : MonoBehaviour
{
    TextMeshProUGUI scoreText;
    GameSession gameSession;
    CheckpointsManager checkpointsManager;

    [SerializeField] bool isRecordScoreText;



    void Start()
    {
        checkpointsManager = FindObjectOfType<CheckpointsManager>();
        scoreText = GetComponent<TextMeshProUGUI>();
        gameSession = FindObjectOfType<GameSession>();

        if (isRecordScoreText)
            scoreText.text = gameSession.recordScore.ToString() + " - " + gameSession.recordCheckpoint;
        else if (!isRecordScoreText)
            scoreText.text = gameSession.score.ToString();
    }

    void Update()
    {
        if(checkpointsManager.lastCheckpoint != null)
        gameSession.recordCheckpoint = checkpointsManager.lastCheckpoint.GetComponent<ArrivedPointsChanger>().pointName;

        if (isRecordScoreText)
            scoreText.text = gameSession.recordScore.ToString() + " - " + gameSession.recordCheckpoint;
        else if (!isRecordScoreText)
            scoreText.text = gameSession.score.ToString();
    }
}
