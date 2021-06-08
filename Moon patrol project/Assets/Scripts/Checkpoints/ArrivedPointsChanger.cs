using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ArrivedPointsChanger : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI PointText;
    public string pointName;
    public bool isBig;
    public bool isEnd;
    public int avergeTime;
    [SerializeField] bool isE;
    [SerializeField] bool isJ;

    CheckpointsManager checkpointsManager;
    PlayerMovement playerMovement;
    Laser laser;
    Ground ground;

    private void Start()
    {
        ground = FindObjectOfType<Ground>();
        checkpointsManager = FindObjectOfType<CheckpointsManager>();
        playerMovement = FindObjectOfType<PlayerMovement>();
        laser = FindObjectOfType<Laser>();
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            GetComponent<Collider2D>().enabled = false;

            checkpointsManager.lastCheckpoint = this.gameObject;
            PointText.SetText(pointName);

            if (isBig)
            {
                laser.enabled = false;
                playerMovement.enabled = false;
                playerMovement.slowBox.SetActive(false);
                playerMovement.mediumBox.SetActive(false);
                playerMovement.speedBox.SetActive(false);
                checkpointsManager.LoadChackpoint();
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (isE)
            ground.numberOfMapState = 1;
        else if (isJ)
            ground.numberOfMapState = 2;
    }
}
