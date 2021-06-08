using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeadAnimations : StateMachineBehaviour
{
    FallInHole inHole;
    PlayerMovement playerMovement;
    CheckpointsManager checkpointsManager;
    GameTimer gameTimer;
    GameManager gameManager;
    Ground ground;

    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        ground = FindObjectOfType<Ground>();
        gameManager = FindObjectOfType<GameManager>();
        gameTimer = FindObjectOfType<GameTimer>();
        playerMovement = FindObjectOfType<PlayerMovement>();
        checkpointsManager = FindObjectOfType<CheckpointsManager>();

        playerMovement.slowBox.SetActive(false);
        playerMovement.mediumBox.SetActive(false);
        playerMovement.speedBox.SetActive(false);
        playerMovement.enabled = false;
    }

    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        checkpointsManager.LoadChackpoint();
        ground.ResetBackground();
    }
}
