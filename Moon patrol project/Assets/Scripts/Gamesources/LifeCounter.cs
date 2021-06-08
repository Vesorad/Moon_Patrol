using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class LifeCounter : MonoBehaviour
{
    GameManager gameManager;

    [SerializeField] TextMeshProUGUI text;

    void Awake()
    {
        gameManager = FindObjectOfType<GameManager>();
        text.SetText(gameManager.playerLifes.ToString());
    }

    public void ChangeLifeCounter()
    {
        gameManager.playerLifes--;
    }

    private void Update()
    {
        if (gameManager.playerLifes >= 0)
            text.SetText(gameManager.playerLifes.ToString());
        else
            text.SetText("0");
    }
}
