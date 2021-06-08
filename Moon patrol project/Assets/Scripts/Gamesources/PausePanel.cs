using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PausePanel : MonoBehaviour
{
    GameManager gameManager;
    GameSession gameSession;
    AudioManager audioManager;

    public GameObject pausePanel;

    [HideInInspector] public bool panelActive;

    void Start()
    {
        audioManager = FindObjectOfType<AudioManager>();
        gameSession = GetComponent<GameSession>();
        gameManager = FindObjectOfType<GameManager>();
        panelActive = false;
    }

    void Update()
    {
        if (!panelActive && Input.GetKeyDown(gameManager.pause))
        {
            pausePanel.SetActive(true);
            panelActive = true;
            Time.timeScale = 0;
            Cursor.visible = true;
            pausePanel.GetComponent<PauseExitOnInput>().canOffOnClick = true;
        }
    }

    public void ReasumeGame()
    {
        pausePanel.SetActive(false);
        panelActive = false;
        Time.timeScale = 1;
        Cursor.visible = false;
    }
    public void GoToMenu()
    {
        Time.timeScale = 1;
        audioManager.Stop("BMG");
        audioManager.Stop("Wave");
        gameManager.LoadScene(0);
        gameSession.SaveRecordScore();
    }
    public void ExitGame()
    {
        gameSession.SaveRecordScore();
        Application.Quit();
    }
}
