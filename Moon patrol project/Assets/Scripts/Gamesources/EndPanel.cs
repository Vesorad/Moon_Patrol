using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EndPanel : MonoBehaviour
{
    GameSession gameSession;
    AudioManager audioManager;

    private void Awake()
    {
        audioManager = FindObjectOfType<AudioManager>();
        gameSession = FindObjectOfType<GameSession>();
        Cursor.visible = true;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            gameSession.SaveRecordScore();
            Application.Quit();
        }
    }

    public void Exit()
    {
        gameSession.SaveRecordScore();
        audioManager.Stop("Wave");
        audioManager.Stop("BMG");
        SceneManager.LoadScene(0);
    }

    public void ResetGame()
    {
        Cursor.visible = false;
        SceneManager.LoadScene(1);
    }
}
