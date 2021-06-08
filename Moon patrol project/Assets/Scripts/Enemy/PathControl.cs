using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimContr : MonoBehaviour
{
    [SerializeField] Animator animator;

    float oldPos;
    
    void Start()
    {
        oldPos = transform.position.x;
    }
    private void Update()
    {
        if (transform.hasChanged)
        {
            if (transform.position.x < oldPos)
            {
                animator.SetFloat("LeftOrRight", -1);
                oldPos = transform.position.x;
               // Debug.Log("Leci w lewo");
            }
           else
            {
                animator.SetFloat("LeftOrRight", 1);
                oldPos = transform.position.x;
               // Debug.Log("leci w prawo");
            }

        }

    }
    
}
