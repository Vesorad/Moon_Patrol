using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class UiManager : MonoBehaviour
{
    [SerializeField] GameObject creditsPanel;
    [SerializeField] GameObject settingsPanel;
    StartButton startButton;
    CoinSpawnPoint spawnPointCoin;
    GameManager gameManager;
    AudioManager audioManager;

    private void Awake()
    {
        audioManager = FindObjectOfType<AudioManager>();

        gameManager = FindObjectOfType<GameManager>();
        startButton = FindObjectOfType<StartButton>();
        spawnPointCoin = FindObjectOfType<CoinSpawnPoint>();
    }

    private void Start()
    {
         audioManager.Play("Menu");
        creditsPanel.SetActive(false);
    }

    public void StartNewGame()
    {
        if (startButton.isActive)
            StartCoroutine(StartNewGameCourutine());
    }

    public void OpenSettings()
    {
        settingsPanel.SetActive(true);
    }
    public void CloseSettings()
    {
        settingsPanel.SetActive(false);
    }

    public void OpenCreddits()
    {
        creditsPanel.SetActive(true);
    }
    public void CloseCreddits()
    {
        creditsPanel.SetActive(false);
    }

    public void Exit()
    {
        Application.Quit();
    }

    public void RejectCoin()
    {
        if(spawnPointCoin.paid)
        {
            spawnPointCoin.CreateCoin();
            spawnPointCoin.paid = false;
            spawnPointCoin.rejectButton.SetActive(false);
        }
    }

    IEnumerator StartNewGameCourutine()
    {
        audioManager.Play("StartButton");
        yield return new WaitForSeconds(0.89f);
        audioManager.Stop("Menu");
        gameManager.LoadScene(1);
    }
}
