using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    AudioManager audioManager;
    [HideInInspector] public Rigidbody2D rb;
    GameManager gameManager;
    [HideInInspector] public Vector3 playerSpotPoint; //the place where the player will return

    float actualWheelSpeed;
    [HideInInspector] public Wheel[] wheels;
    [HideInInspector] public Vector3[] wheelsSpotPoints;
    [HideInInspector] public float[] deltaX;
    [HideInInspector] public float[] deltaY;
    [HideInInspector] public Rigidbody2D[] wheelsRB = new Rigidbody2D[3];
    [HideInInspector] public bool upWheels;

    [Header("Wheels options")]
    [Range(0, 10)] [SerializeField] float wheelSpeed;

    [Header("Player movement options")]
    public float playerMaxSpeed;
    public float playerMinSpeed;
    [Range(-5, 0)] [Tooltip("0 value is the center of screen // min value can't be lower than x player position")] public float rightLimitBorder;
    [Range(-8, -2)] [Tooltip("max value can't be higher than x player position")] [SerializeField] float leftLimitBorder;
    public float movementAccelerationTime;
    public float movementSlowTime;
    [SerializeField] float changeDirectionTime;
    [SerializeField] float slowMovementProcentBorder = 0.2f;
   /* [HideInInspector]*/ public float actualSpeed;
    float movementSection;
    [HideInInspector] public bool right;
    [HideInInspector] public bool left;
    bool moveLeftAlready;
    bool moveRightAlready;

    [Header("Jump options")]
    [SerializeField] [Range(-0.5f, 1)] float wheelJumpPosition;
    [HideInInspector] public float jumpForce;
    public float bigJumpForce;
    public float mediumBigJumpForce;
    public float mediumJumpForce;
    public float smallMediumJumpForce;
    public float smallJumpForce;
    [HideInInspector] public bool jump;

    [Header("Ground movement options")]
    public GameObject slowBox;
    public GameObject mediumBox;
    public GameObject speedBox;
    public float boxTime;
    public GameObject smallJumpBox;
    public GameObject mediumSmallJumpBox;
    public GameObject mediumJumpBox;
    public GameObject bigMediumJumpBox;
    public GameObject bigJumpBox;

    void Awake()
    {
        audioManager = FindObjectOfType<AudioManager>();
        gameManager = FindObjectOfType<GameManager>();
        rb = GetComponent<Rigidbody2D>();
        playerSpotPoint = transform.position;

        wheels = FindObjectsOfType<Wheel>();
        wheelsSpotPoints = new Vector3[wheels.Length];
        deltaX = new float[wheels.Length];
        deltaY = new float[wheels.Length];
        for (int i = 0; i < wheels.Length; i++)
        {
            wheelsSpotPoints[i] = wheels[i].transform.position;
            deltaX[i] = transform.position.x - wheelsSpotPoints[i].x;
            deltaY[i] = transform.position.y - wheelsSpotPoints[i].y;

            wheelsRB[i] = wheels[i].GetComponent<Rigidbody2D>();
        }

        jump = false;
    }

    void FixedUpdate()
    {
        WheelsMovement();

        RightMovement();

        LeftMovement();

        if (!right && !left)
            Back();

        if (jump)
        {
            foreach (var rb in wheelsRB)
            {
                rb.gameObject.GetComponent<Collider2D>().enabled = false;
                rb.gravityScale = 0;
            }
        }
    }

    private void Update()
    {
        if (Input.GetKeyDown(gameManager.rightMove))
            movementSection = Mathf.Abs(transform.position.x - rightLimitBorder);

        if (Input.GetKeyDown(gameManager.leftMove))
            movementSection = Mathf.Abs(transform.position.x - leftLimitBorder);

        if (Input.GetKeyDown(gameManager.jump) && !jump)
        {
            audioManager.Play("Jump");
            rb.velocity = Vector2.up * jumpForce;
            upWheels = true;

            if (right)
                movementSection = Mathf.Abs(transform.position.x - rightLimitBorder);
            else if (left)
                movementSection = Mathf.Abs(transform.position.x - leftLimitBorder);
        }
    }

    void RightMovement()
    {
        if (Input.GetKey(gameManager.rightMove) && !jump && !right)
        {
            if (!moveRightAlready)
            {
                actualSpeed = Mathf.Lerp(actualSpeed, -1, changeDirectionTime * Time.deltaTime);

                if (actualSpeed <= 0)
                    right = true;
            }
            else if (moveRightAlready)
                right = true;
        }

        if (right && (Input.GetKey(gameManager.rightMove) || jump))
        {
            moveRightAlready = false;

            if (Mathf.Abs(transform.position.x - rightLimitBorder) > movementSection * slowMovementProcentBorder)
                actualSpeed = Mathf.Lerp(actualSpeed, playerMaxSpeed, movementAccelerationTime * Time.deltaTime);
            else
                actualSpeed = Mathf.Lerp(actualSpeed, playerMinSpeed, movementSlowTime * Time.deltaTime);

            if (transform.position.x <= rightLimitBorder)
                transform.Translate(Vector2.right * actualSpeed * Time.deltaTime);
        }
        else if (right && !Input.GetKey(gameManager.rightMove) && !jump)
        {
            actualSpeed = Mathf.Lerp(actualSpeed, -1, changeDirectionTime * Time.deltaTime);

            if (actualSpeed <= 0)
            {
                moveLeftAlready = true;
                right = false;
            }

            if (transform.position.x <= rightLimitBorder)
                transform.Translate(Vector2.right * actualSpeed * Time.deltaTime);
        }
    }

    void LeftMovement()
    {
        if (Input.GetKey(gameManager.leftMove) && !jump && !left)
        {
            if (!moveLeftAlready)
            {
                actualSpeed = Mathf.Lerp(actualSpeed, -1, changeDirectionTime * Time.deltaTime);

                if (actualSpeed <= 0)
                    left = true;
            }
            else if (moveLeftAlready)
                left = true;
        }

        if (left && (Input.GetKey(gameManager.leftMove) || jump))
        {
            moveLeftAlready = false;

            if (Mathf.Abs(transform.position.x - leftLimitBorder) > movementSection * slowMovementProcentBorder)
                actualSpeed = Mathf.Lerp(actualSpeed, playerMaxSpeed, movementAccelerationTime * Time.deltaTime);
            else
                actualSpeed = Mathf.Lerp(actualSpeed, playerMinSpeed, movementSlowTime * Time.deltaTime);

            if (transform.position.x >= leftLimitBorder)
                transform.Translate(Vector2.left * actualSpeed * Time.deltaTime);
        }
        else if (left && !Input.GetKey(gameManager.leftMove) && !jump)
        {
            actualSpeed = Mathf.Lerp(actualSpeed, -1, changeDirectionTime * Time.deltaTime);

            if (actualSpeed <= 0)
            {
                moveRightAlready = true;
                left = false;
            }

            if (transform.position.x >= leftLimitBorder)
                transform.Translate(Vector2.left * actualSpeed * Time.deltaTime);
        }
    }

    void Back()
    {
        if ((!Input.GetKey(gameManager.leftMove) && !Input.GetKey(gameManager.rightMove) && !jump) || jump)
        {
            if (Mathf.Abs(transform.position.x - playerSpotPoint.x) > movementSection * slowMovementProcentBorder)
                actualSpeed = Mathf.Lerp(actualSpeed, playerMaxSpeed, movementAccelerationTime * Time.deltaTime);
            else
                actualSpeed = Mathf.Lerp(actualSpeed, playerMinSpeed, movementSlowTime * Time.deltaTime);
        }

        if (transform.position.x > playerSpotPoint.x + 0.1f)
            transform.Translate(Vector2.left * actualSpeed * Time.deltaTime);
        else if (transform.position.x < playerSpotPoint.x - 0.1f)
            transform.Translate(Vector2.right * actualSpeed * Time.deltaTime);
    }

    public void WheelsMovement()
    {
        for (int i = 0; i < wheelsSpotPoints.Length; i++)
        {
            wheelsSpotPoints[i].x = transform.position.x - deltaX[i];
            wheelsSpotPoints[i].y = transform.position.y - deltaY[i];
        }

        for (int i = 0; i < wheels.Length; i++)
        {
            Vector3 newPos = wheels[i].gameObject.transform.position;
            newPos.x = wheelsSpotPoints[i].x;

            if (jump || upWheels)
                newPos.y = wheelsSpotPoints[i].y + wheelJumpPosition;
            else if (wheels[i].transform.position.y < (wheelsSpotPoints[i].y - 0.33f))
                newPos.y = wheelsSpotPoints[i].y;

            wheels[i].transform.position = newPos;
        }

        foreach (var wheel in wheels)
        {
            wheel.gameObject.transform.Rotate(Vector3.back * wheelSpeed * 100f * Time.deltaTime);
        }
    }

    private void OnEnable()
    {
        right = false;
        left = false;
        moveLeftAlready = false;
        moveRightAlready = false;
        jump = false;
    }
}
