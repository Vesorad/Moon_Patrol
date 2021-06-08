using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyHole : MonoBehaviour
{
    [SerializeField] float timeToDestory = 0.5f;
    PlayerDeath playerDeath;
    ArrivedPointsChanger arrivedPointsChanger;
    CheckpointPanel checkpointPanel;
    [HideInInspector] public bool resetPanel;

    private void Start()
    {
        checkpointPanel = FindObjectOfType<CheckpointPanel>();
        playerDeath = FindObjectOfType<PlayerDeath>();
        arrivedPointsChanger = FindObjectOfType<ArrivedPointsChanger>();
    }
    void Update()
    {
        if (playerDeath.die == true)
            Destroy(gameObject, timeToDestory);
        if (resetPanel == true)
        {
            Destroy(gameObject, timeToDestory);
            resetPanel = false;
        }
    }
}
