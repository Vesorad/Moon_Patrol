using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class DeadPanelTimer : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI timerText;
    GameManager gameManager;
    GameSession gameSession;
    AudioManager audioManager;
    GameTimer gameTimer;

    int time;

    void Awake()
    {
        gameTimer = FindObjectOfType<GameTimer>();
        gameTimer.StopTimer();

        audioManager = FindObjectOfType<AudioManager>();
        gameSession = FindObjectOfType<GameSession>();
        gameManager = FindObjectOfType<GameManager>();
        time = 5;
        StartCoroutine(Timer());
    }
    IEnumerator Timer()
    {
        for (int i = time; i >= 0; i--)
        {
            if (time > 0)
            {
                timerText.SetText(time.ToString());
                time--;
                yield return new WaitForSeconds(1f);
            }
            else
            {
                gameSession.SaveRecordScore();
                audioManager.Stop("Wave");
                audioManager.Stop("BMG");
                gameSession.RestScore();
                gameManager.LoadScene(0);
            }
        }

    }
}
