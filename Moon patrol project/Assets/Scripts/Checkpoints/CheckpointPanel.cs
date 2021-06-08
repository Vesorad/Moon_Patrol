using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class CheckpointPanel : MonoBehaviour
{
    AudioManager audioManager;
    CheckpointsManager checkpointsManager;
    GameTimer gameTimer;
    GameSession gameSession;
    ArrivedPointsChanger pointsChanger;
    PlayerMovement playerMovement;
    Laser laser;
    Ground ground;
    DestroyHole[] destroyHole = new DestroyHole[100];

    [SerializeField] GameObject endPanel;

    [Header("Text options")]
    [SerializeField] float pointsCounterTime = 0.5f;
    [SerializeField] float showTextDelay;
    [SerializeField] string[] fullText;
    string currentText = "";
    [SerializeField] TextMeshProUGUI titleText;
    [SerializeField] TextMeshProUGUI playerTimeText;
    [SerializeField] TextMeshProUGUI avergeTimeText;
    [SerializeField] TextMeshProUGUI topRecordText;
    [SerializeField] TextMeshProUGUI goodBonusPointsText;
    [SerializeField] TextMeshProUGUI brokeRecordText;
    [SerializeField] TextMeshProUGUI sorryText;

    int avergeTime;
    int playerTime;
    int topTime;
    string chackpointName;
    int bonusPoints;
    string[] lastFullText = new string[6];


    void Awake()
    {
            destroyHole = FindObjectsOfType<DestroyHole>();
        for (int i = 0; i < destroyHole.Length; i++)
        {
            destroyHole[i].resetPanel = true;
        }
           
        
        
        ground = FindObjectOfType<Ground>();
        laser = FindObjectOfType<Laser>();
        audioManager = FindObjectOfType<AudioManager>();
        bonusPoints = 1000;
        checkpointsManager = FindObjectOfType<CheckpointsManager>();
        gameTimer = FindObjectOfType<GameTimer>();
        gameSession = FindObjectOfType<GameSession>();
        checkpointsManager = FindObjectOfType<CheckpointsManager>();
        playerMovement = FindObjectOfType<PlayerMovement>();



        pointsChanger = checkpointsManager.lastCheckpoint.gameObject.GetComponent<ArrivedPointsChanger>();
        chackpointName = pointsChanger.pointName;
        topTime = pointsChanger.avergeTime;
        avergeTime = topTime;

        playerTime = gameTimer.time;
        gameTimer.ResetTimer();

        for (int i = 0; i < lastFullText.Length; i++)
        {
            lastFullText[i] = fullText[i];
        }

        fullText[0] = fullText[0] + chackpointName;
        fullText[1] = fullText[1] + playerTime.ToString();
        fullText[2] = fullText[2] + avergeTime.ToString();
        fullText[3] = fullText[3] + topTime.ToString();
        fullText[4] = fullText[4] + bonusPoints.ToString();

        StartCoroutine(PanelShowCorutine());
    }

    IEnumerator PanelShowCorutine()
    {
        for (int i = 0; i <= fullText[0].Length; i++)
        {
            currentText = fullText[0].Substring(0, i);
            titleText.SetText(currentText);
            yield return new WaitForSeconds(showTextDelay);
        }

        playerTimeText.gameObject.SetActive(true);
        for (int i = 0; i <= fullText[1].Length; i++)
        {
            currentText = fullText[1].Substring(0, i);
            playerTimeText.SetText(currentText);
            yield return new WaitForSeconds(showTextDelay);
        }

        avergeTimeText.gameObject.SetActive(true);
        for (int i = 0; i <= fullText[2].Length; i++)
        {
            currentText = fullText[2].Substring(0, i);
            avergeTimeText.SetText(currentText);
            yield return new WaitForSeconds(showTextDelay);
        }

        topRecordText.gameObject.SetActive(true);
        for (int i = 0; i <= fullText[3].Length; i++)
        {
            currentText = fullText[3].Substring(0, i);
            topRecordText.SetText(currentText);
            yield return new WaitForSeconds(showTextDelay);
        }

        if (playerTime < avergeTime)
        {
            goodBonusPointsText.gameObject.SetActive(true);
            for (int i = 0; i <= fullText[4].Length; i++)
            {
                currentText = fullText[4].Substring(0, i);
                goodBonusPointsText.SetText(currentText);
                yield return new WaitForSeconds(showTextDelay);
            }

            brokeRecordText.gameObject.SetActive(true);
            for (int i = 0; i <= fullText[5].Length; i++)
            {
                currentText = fullText[5].Substring(0, i);
                brokeRecordText.SetText(currentText);
                yield return new WaitForSeconds(showTextDelay);
            }

            for (int i = avergeTime; i > playerTime; i--)
            {
                audioManager.Play("Bonus");
                avergeTime--;
                bonusPoints += 100;
                avergeTimeText.SetText(lastFullText[2] + avergeTime.ToString());
                goodBonusPointsText.SetText(lastFullText[4] + bonusPoints.ToString());
                yield return new WaitForSeconds(pointsCounterTime);
            }
            gameSession.AddToScore(bonusPoints);
        }
        else if (avergeTime <= playerTime)
        {
            sorryText.gameObject.SetActive(true);
            for (int i = 0; i <= fullText[6].Length; i++)
            {
                currentText = fullText[6].Substring(0, i);
                sorryText.SetText(currentText);
                yield return new WaitForSeconds(showTextDelay);
            }
            yield return new WaitForSeconds(2f);
        }

        if (!pointsChanger.isEnd)
        {
            gameTimer.StartTimer();
            playerMovement.enabled = true;
            laser.enabled = true;
            playerMovement.slowBox.SetActive(true);
            playerMovement.mediumBox.SetActive(true);
            playerMovement.speedBox.SetActive(true);
            audioManager.Play("BMG");
            ground.ResetBackground();
            Destroy(this.gameObject);
        }
        else if(pointsChanger.isEnd)
        {
            Instantiate(endPanel);
        }

    }
}
