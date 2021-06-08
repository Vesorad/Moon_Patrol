using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ground : MonoBehaviour
{
    GameObject background;
    PlayerMovement player;
    GameManager gameManager;
    [HideInInspector] public float deltaPosition;
    float simplePointDeltaPosition;

     public int numberOfMapState;

    [Header("Bacground prifabs")]
    public GameObject bacgroundPrifab;

    [Header("Ground options")]
    [Range(0, 10)] public float minSpeed;
    [Range(0, 10)] public float mediumSpeed;
    [Range(0, 10)] public float maxSpeed;
    public float actualSpeed;

    [Header("Checkpoints settings")]
    [Tooltip("W tym momencie trzeba tyko ustawić ostatni chacpoint na planszy a reszta ustala się automatycznie w 0.2/0.4/0.6/0.8 trasy")] public GameObject[] checkpoints = new GameObject[5];

    private void Awake()
    {
        numberOfMapState = 0;
        player = FindObjectOfType<PlayerMovement>();
        gameManager = FindObjectOfType<GameManager>();
        deltaPosition = checkpoints[4].transform.position.x - player.playerSpotPoint.x;
        background = Instantiate(bacgroundPrifab);
        SetCheckpointsPositions();
    }

    private void Update()
    {
        transform.Translate(Vector3.left * actualSpeed * Time.deltaTime);
        deltaPosition = checkpoints[4].transform.position.x - player.playerSpotPoint.x;
    }

    void SetCheckpointsPositions()
    {
        float delta = 0.2f;

        for (int i = 0; i < checkpoints.Length - 1; i++)
        {
            Vector3 newpos = checkpoints[i].transform.position;
            newpos.x = player.playerSpotPoint.x + (delta * deltaPosition);
            checkpoints[i].transform.position = newpos;

            delta += 0.2f;
        }
    }

    public void ResetBackground()
    {
        Destroy(background);
        background = Instantiate(bacgroundPrifab);
    }
}
