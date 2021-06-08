using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CrossFade : MonoBehaviour
{
    GameManager gameManager;
   [SerializeField] Animator animator;

    void Awake()
    {
        gameManager = FindObjectOfType<GameManager>();
        animator = GetComponent<Animator>();
        gameManager.transition = this.animator;
    }
}
