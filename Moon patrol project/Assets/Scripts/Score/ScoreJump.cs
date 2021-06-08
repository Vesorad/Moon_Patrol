using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreJump : MonoBehaviour
{
    [SerializeField] int scoreValueBigJump = 100;
    [SerializeField] int scoreValueSmallJump = 80;
    [SerializeField] GameObject rock;

    private void Start()
    {

    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            if (rock.GetComponent<Rock>().GetHitTimes() == 1)
            {
                FindObjectOfType<GameSession>().AddToScore(scoreValueSmallJump);

            }
            else
            {
                FindObjectOfType<GameSession>().AddToScore(scoreValueBigJump);
            }
        }
    }

}
