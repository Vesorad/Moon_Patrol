using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class StartScene : MonoBehaviour
{
    Laser laser;
    JumpPlayerCollider playerCollider;
    AudioManager audioManager;
    GameManager gameManager;
    GameTimer gameTimer;
    [SerializeField] Ground ground;
    [SerializeField] PlayerMovement playerMovement;
    [SerializeField] GameObject pressText;
    TextMeshProUGUI proUGUI;
    bool start;
    float actualSpeed;
    float movementSection;

    [SerializeField] bool test;

    void Start()
    {
        laser = FindObjectOfType<Laser>();
        playerCollider = FindObjectOfType<JumpPlayerCollider>();
        audioManager = FindObjectOfType<AudioManager>();
        audioManager.Stop("BMG");
        audioManager.Play("Start");
        start = false;
        proUGUI = pressText.GetComponent<TextMeshProUGUI>();
        StartCoroutine(TextCourutine());

        gameTimer = FindObjectOfType<GameTimer>();
        gameManager = FindObjectOfType<GameManager>();
        playerMovement.enabled = false;
        playerCollider.enabled = false;
        laser.enabled = false;
        ground.actualSpeed = 0f;
        movementSection = Mathf.Abs(playerMovement.gameObject.transform.position.x - playerMovement.rightLimitBorder);
        actualSpeed = 0;

        if (!test)
            StartCoroutine(StartSceneCourutine());
    }

    void Update()
    {
        if (test)
            if (Input.GetKeyDown(gameManager.jump) && !start)
            {
                audioManager.Stop("Start");
                audioManager.Play("BMG");
                pressText.SetActive(false);
                start = true;
                StartCoroutine(gameTimer.StartTimerCourutine());
            }
        StartGameScene();
    }

    void StartGameScene()
    {
        if (start)
        {
            playerMovement.WheelsMovement();

            if (Mathf.Abs(transform.position.x - playerMovement.rightLimitBorder) > movementSection * 0.2f)
                actualSpeed = Mathf.Lerp(actualSpeed, playerMovement.playerMaxSpeed, playerMovement.movementAccelerationTime * Time.deltaTime);
            else
                actualSpeed = Mathf.Lerp(actualSpeed, playerMovement.playerMinSpeed, playerMovement.movementSlowTime * Time.deltaTime);

            if (playerMovement.gameObject.transform.position.x < -2.5f)
            {
                ground.actualSpeed = Mathf.Lerp(ground.actualSpeed, ground.maxSpeed, playerMovement.boxTime * Time.deltaTime);
                playerMovement.gameObject.transform.Translate(Vector2.right * actualSpeed * Time.deltaTime);
            }
            else
            {
                laser.enabled = true;
                playerMovement.enabled = true;
                playerCollider.enabled = true;
                start = false;
                playerMovement.slowBox.SetActive(true);
                playerMovement.mediumBox.SetActive(true);
                playerMovement.speedBox.SetActive(true);
                enabled = false;
            }
        }
        else
        {
            for (int i = 0; i < playerMovement.wheelsSpotPoints.Length; i++)
            {
                playerMovement.wheelsSpotPoints[i].x = playerMovement.gameObject.transform.position.x - playerMovement.deltaX[i];
                playerMovement.wheelsSpotPoints[i].y = playerMovement.gameObject.transform.position.y - playerMovement.deltaY[i];
            }

            for (int i = 0; i < playerMovement.wheels.Length; i++)
            {
                Vector3 newPos = playerMovement.wheels[i].gameObject.transform.position;
                newPos.x = playerMovement.wheelsSpotPoints[i].x;

                if (playerMovement.wheels[i].transform.position.y < (playerMovement.wheelsSpotPoints[i].y - 0.33f))
                    newPos.y = playerMovement.wheelsSpotPoints[i].y;

                playerMovement.wheels[i].transform.position = newPos;
            }
        }
    }

    IEnumerator TextCourutine()
    {
        do
        {
            proUGUI.fontSize = 90;
            yield return new WaitForSeconds(0.5f);
            proUGUI.fontSize = 100;
            yield return new WaitForSeconds(0.5f);
        } while (!start);
    }
    IEnumerator StartSceneCourutine()
    {
        yield return new WaitForSeconds(6f);
        audioManager.Play("BMG");
        pressText.SetActive(false);
        start = true;
        StartCoroutine(gameTimer.StartTimerCourutine());
    }
}
