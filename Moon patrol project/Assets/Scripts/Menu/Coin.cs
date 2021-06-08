using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

[RequireComponent(typeof(Rigidbody2D))]
public class Coin : MonoBehaviour
{
    AudioManager audioManager;
    GameObject spawnPoint;
    GameManager gameManager;
    Animator animator;
    SpriteRenderer spriteRenderer;
    Rigidbody2D rb;
    CheckpointsManager checkpointsManager;
    PlayerMovement playerMovement;
    GameTimer gameTimer;
    GameSession gameSession;

    bool isInCollision;
    bool back;

    GameObject deadPanel;

    [SerializeField] float coinBackSpeed;

    [Header("Coin sprites")]
    [SerializeField] Sprite front;
    [SerializeField] Sprite side;

    void Awake()
    {
        playerMovement = FindObjectOfType<PlayerMovement>();
        audioManager = FindObjectOfType<AudioManager>();
        gameManager = FindObjectOfType<GameManager>();
        spawnPoint = FindObjectOfType<CoinSpawnPoint>().gameObject;
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        isInCollision = false;
        back = false;
    }

    private void Update()
    {
        if (back)
            CoinBack();
    }

    private void OnMouseDrag()
    {
        animator.enabled = false;

        if (!isInCollision)
            spriteRenderer.sprite = front;

        Vector3 mousePosition = new Vector3(Input.mousePosition.x, Input.mousePosition.y, 10f);

        Vector3 objPosition = Camera.main.ScreenToWorldPoint(mousePosition);

        transform.position = objPosition;
    }

    private void OnMouseUp()
    {
        if (!isInCollision)
        {
            animator.enabled = true;
            back = true;
        }
        else if (isInCollision)
        {
            audioManager.Play("Coin");
            CoinSpawnPoint cs = spawnPoint.GetComponent<CoinSpawnPoint>();
            cs.paid = true;
            cs.rejectButton.SetActive(true);
            gameManager.playerLifes = 4;
  

            if (SceneManager.GetActiveScene().name == "game")
            {
                gameSession = FindObjectOfType<GameSession>();
                gameTimer = FindObjectOfType<GameTimer>();
                checkpointsManager = FindObjectOfType<CheckpointsManager>();
                deadPanel = FindObjectOfType<CoinSpawnPoint>().gameObject.transform.parent.gameObject;
                Destroy(deadPanel);
                Cursor.visible = false;
                checkpointsManager.playerMovement.enabled = true;
                playerMovement.slowBox.SetActive(true);
                playerMovement.mediumBox.SetActive(true);
                playerMovement.speedBox.SetActive(true);
                gameTimer.StartTimer();
                gameSession.score = 0;
            }
            Destroy(this.gameObject);
        }
    }

    void CoinBack()
    {
        Vector3 newPos = Vector3.MoveTowards(rb.position, spawnPoint.transform.position, coinBackSpeed * Time.fixedDeltaTime);
        rb.MovePosition(newPos);
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.name == "CoinSet")
        {
            isInCollision = true;
            spriteRenderer.sprite = side;
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.name == "CoinSet")
        {
            isInCollision = false;
            spriteRenderer.sprite = front;
        }
    }
}
