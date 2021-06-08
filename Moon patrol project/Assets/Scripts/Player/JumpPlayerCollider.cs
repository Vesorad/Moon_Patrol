using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JumpPlayerCollider : MonoBehaviour
{
    PlayerMovement playerMovement;
    GameManager gameManager;

    private void Awake()
    {
        playerMovement = FindObjectOfType<PlayerMovement>();
        gameManager = FindObjectOfType<GameManager>();
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Ground")
        {
            playerMovement.jump = true;
        }
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Ground")
        {
            if (playerMovement.jump)
            {
                if (!Input.GetKey(gameManager.rightMove) && !Input.GetKey(gameManager.leftMove))
                    playerMovement.jump = false;

                if (Input.GetKey(gameManager.rightMove))
                {
                    playerMovement.right = true;
                    playerMovement.jump = false;
                }

                if (Input.GetKey(gameManager.leftMove))
                {
                    playerMovement.left = true;
                    playerMovement.jump = false;
                }
            }
            playerMovement.jump = false;
            foreach (var rb in playerMovement.wheelsRB)
            {
                rb.gameObject.GetComponent<Collider2D>().enabled = true;
                rb.gravityScale = 1;
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Ground")
            playerMovement.upWheels = false;
    }
}
