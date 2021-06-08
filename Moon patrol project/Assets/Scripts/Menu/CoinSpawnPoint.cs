using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinSpawnPoint : MonoBehaviour
{
    [SerializeField] GameObject coin;
    public GameObject rejectButton;
    GameObject coinInGame;
    GameManager gameManager;
    [HideInInspector] public bool paid;

    void Start()
    {
        rejectButton.SetActive(false);
        paid = false;
        gameManager = FindObjectOfType<GameManager>();
        CreateCoin();
    }

    public void CreateCoin()
    {
        rejectButton.SetActive(false);

        if (gameManager.playerLifes > 0)
            gameManager.playerLifes = 0;

        coinInGame = Instantiate(coin);
        Vector3 newPos = coinInGame.transform.position;
        newPos.x = transform.position.x;
        newPos.y = transform.position.y;
        coinInGame.transform.position = newPos;
    }
}
