using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class StartButton : MonoBehaviour
{
    GameManager gameManager;

    [HideInInspector] public bool isActive;
    [SerializeField] TextMeshProUGUI text;
    [SerializeField] Color unactiveStartButtonColor;
    Color color;

    void Awake()
    {
        gameManager = FindObjectOfType<GameManager>();
        isActive = false;
        color = text.color;
    }

    void Update()
    {
        if (gameManager.playerLifes > 0)
            isActive = true;
        else
            isActive = false;

        if (!isActive)
        {
            text.fontSize = 80;
            text.color = unactiveStartButtonColor;
        }
        else
        {
            text.fontSize = 100;
            text.color = color;
        }
    }
}
