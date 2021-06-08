using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseExitOnInput : MonoBehaviour
{
    GameManager gameManager;
    PausePanel pausePanel;
     public bool canOffOnClick;

    void Start()
    {
        gameManager = FindObjectOfType<GameManager>();
        pausePanel = FindObjectOfType<PausePanel>();
    }

    void Update()
    {
        if (Input.GetKeyDown(gameManager.pause) && canOffOnClick)
        {
            pausePanel.pausePanel.SetActive(false);
            pausePanel.panelActive = false;
            Time.timeScale = 1;
            Cursor.visible = false;
            canOffOnClick = false;
        }
    }
}
