using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Sygnalizator : MonoBehaviour
{
    PlayerDeath playerDeath;

    Image coution;
    Image red;
    Image green;
    [Space(10)]
    [SerializeField] int numberOfGleam;
    [SerializeField] float gleamSpeed;
    [SerializeField] bool showRed;

    bool breakSygnalizator;

    void Start()
    {
        playerDeath = FindObjectOfType<PlayerDeath>();
        red = GameObject.FindGameObjectWithTag("Red").GetComponent<Image>();
        green = GameObject.FindGameObjectWithTag("Green").GetComponent<Image>();
        coution = GameObject.FindGameObjectWithTag("Coution").GetComponent<Image>();

        Off();
    }

    private void Update()
    {
        if (playerDeath.die)
        {
            breakSygnalizator = true;
        }
    }

    public void Off()
    {
        coution.enabled = false;
        red.enabled = false;
        green.enabled = false;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
            StartCoroutine(SygnalizatorCorutine());
    }

    public IEnumerator SygnalizatorCorutine()
    {
        for (int i = 0; i < numberOfGleam; i++)
        {
            if (breakSygnalizator)
            {
                breakSygnalizator = false;
                Off();
                break;
            }

            if (showRed)
                red.enabled = true;
            else if (!showRed)
                green.enabled = true;

            coution.enabled = true;

            yield return new WaitForSeconds(gleamSpeed);

            if (showRed)
                red.enabled = false;
            else if (!showRed)
                green.enabled = false;

            coution.enabled = false;

            yield return new WaitForSeconds(gleamSpeed);
        }
        yield return null;
    }
}
