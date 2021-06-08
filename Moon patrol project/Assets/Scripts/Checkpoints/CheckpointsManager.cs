using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckpointsManager : MonoBehaviour
{
    AudioManager audioManager;
    Ground ground;
    [HideInInspector] public PlayerMovement playerMovement;
    PlayerDeath playerDeath;
    GameManager gameManager;
    Laser laser;

    [SerializeField] GameObject checkpointsPanel;
    [SerializeField] GameObject deadPanel;
    [HideInInspector] public GameObject lastCheckpoint;

    void Start()
    {
        audioManager = FindObjectOfType<AudioManager>();
        ground = FindObjectOfType<Ground>();
        playerMovement = FindObjectOfType<PlayerMovement>();
        playerDeath = FindObjectOfType<PlayerDeath>();
        gameManager = FindObjectOfType<GameManager>();
        laser = FindObjectOfType<Laser>();

        ground.numberOfMapState = 0;
    }

    public void LoadChackpoint()
    {
        ground.actualSpeed = 0f;
        lastCheckpoint.GetComponent<SpriteRenderer>().enabled = false;
        lastCheckpoint.GetComponent<Collider2D>().enabled = false;
        lastCheckpoint.GetComponent<OnOfLayers>().OffObstacles();

        Vector3 newPos = ground.gameObject.transform.position;
        newPos.x = lastCheckpoint.GetComponent<OnOfLayers>().groundResetX;
        ground.gameObject.transform.position = newPos;

        playerMovement.gameObject.transform.position = playerMovement.playerSpotPoint;

        if (gameManager.playerLifes >= 0 && playerDeath.die)
        {
            audioManager.Stop("Dead");
            playerMovement.enabled = true;
            laser.enabled = true;
            playerMovement.slowBox.SetActive(true);
            playerMovement.mediumBox.SetActive(true);
            playerMovement.speedBox.SetActive(true);
        }
        else if (gameManager.playerLifes >= 0 && lastCheckpoint.GetComponent<ArrivedPointsChanger>().isBig == true && !playerDeath.die)
        {
            playerMovement.enabled = false; 
            audioManager.Stop("BMG");
            audioManager.Play("PointsPanel");
            Instantiate(checkpointsPanel);
        }
        else if (gameManager.playerLifes < 0)
        {
            gameManager.playerLifes = 0;
            Instantiate(deadPanel);
            Cursor.visible = true;
        }
        playerDeath.die = false;
    }
}
