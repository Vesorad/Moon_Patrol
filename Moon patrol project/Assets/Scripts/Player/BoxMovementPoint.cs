using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoxMovementPoint : MonoBehaviour
{
    Ground ground;
    PlayerMovement playerMovement;

    void Start()
    {
        ground = FindObjectOfType<Ground>();
        playerMovement = FindObjectOfType<PlayerMovement>();
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject == playerMovement.slowBox)
        {
            ground.actualSpeed = Mathf.Lerp(ground.actualSpeed, ground.minSpeed, playerMovement.boxTime * Time.deltaTime);
        }
        else if (collision.gameObject == playerMovement.mediumBox)
        {
            ground.actualSpeed = Mathf.Lerp(ground.actualSpeed, ground.mediumSpeed, playerMovement.boxTime * Time.deltaTime);
        }
        else if (collision.gameObject == playerMovement.speedBox)
        {
            ground.actualSpeed = Mathf.Lerp(ground.actualSpeed, ground.maxSpeed, playerMovement.boxTime * Time.deltaTime);
        }
    }
}
