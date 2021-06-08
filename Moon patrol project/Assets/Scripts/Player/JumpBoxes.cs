using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JumpBoxes : MonoBehaviour
{
    PlayerMovement playerMovement;
    void Start()
    {
        playerMovement = FindObjectOfType<PlayerMovement>();
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject == playerMovement.bigJumpBox)
            playerMovement.jumpForce = playerMovement.bigJumpForce;
        else if (collision.gameObject == playerMovement.bigMediumJumpBox)
            playerMovement.jumpForce = playerMovement.mediumBigJumpForce;
        else if (collision.gameObject == playerMovement.mediumBox)
            playerMovement.jumpForce = playerMovement.mediumJumpForce;
        else if (collision.gameObject == playerMovement.mediumSmallJumpBox)
            playerMovement.jumpForce = playerMovement.smallMediumJumpForce;
        else if (collision.gameObject == playerMovement.smallJumpBox)
            playerMovement.jumpForce = playerMovement.smallJumpForce;
    }
}
