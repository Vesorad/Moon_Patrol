using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ScoreAllWave : MonoBehaviour
{
    [SerializeField] int scoreValue = 500;
    AudioManager audioManager;
    bool canDestroy;
    bool addScore = true;
    [HideInInspector] public bool showBonus;
    Vector3 childTransform;
    [SerializeField] GameObject bonusText;

    private void Start()
    {
        bonusText.GetComponent<TextMeshProUGUI>().fontSize = 0;
        bonusText.GetComponent<TextMeshProUGUI>().SetText(scoreValue.ToString());
        showBonus = false;
        canDestroy = false;
        audioManager = FindObjectOfType<AudioManager>();
        audioManager.Play("Wave");
        StartCoroutine(StopMusic());
        StartCoroutine(WaitForDestroy());
    }
    void Update()
    {
        if (canDestroy)
            Wait();
    }

    void Wait()
    {
        if (transform.childCount <= 1)
        {
            audioManager.Stop("Wave");

            if (addScore == true)
            {
                FindObjectOfType<GameSession>().AddToScore(scoreValue);
                addScore = false;
            }
            if (scoreValue > 0)
                bonusText.GetComponent<TextMeshProUGUI>().fontSize = 50;

            StartCoroutine(WaitForValue());
        }
        else if (transform.childCount == 2)
        {
            childTransform = this.gameObject.transform.GetChild(1).position;

            Vector3 bonusPosition = Camera.main.WorldToScreenPoint(childTransform);
            if (bonusText != null)
            {
                bonusText.transform.position = bonusPosition;
            }
        }
    }
    IEnumerator WaitForDestroy()
    {
        yield return new WaitForSeconds(2);
        canDestroy = true;
    }
    IEnumerator StopMusic()
    {
        yield return new WaitForSeconds(12);
        audioManager.Stop("Wave");
    }
    IEnumerator WaitForValue()
    {
        yield return new WaitForSeconds(2);
        Destroy(gameObject);
    }
}
