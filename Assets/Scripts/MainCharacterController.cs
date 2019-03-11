using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainCharacterController : PhysicsObject
{
    private GameController gameController;

    public float maxSpeed = 7;
    public float jumpTakeOffSpeed = 15;

    private SpriteRenderer spriteRenderer;
    private Animator animator;
    private GameObject mainCamera;

    public bool gameStarted = false;

    private bool walking;
    private bool idling;
    private bool jumping;

    private bool rendererFlag;

    private AudioSource audioSource;
    private Collider2D mainCharColl;
    private Collider2D[] overlapping;
    private ContactFilter2D contactF;
    public  LosingCanvas losingCanvas;

    // Use this for initialization
    void Awake()
    {
        losingCanvas = GameObject.Find("LosingCanvas").GetComponent<LosingCanvas>();
        losingCanvas.gameObject.SetActive(false);
        spriteRenderer = GetComponentInChildren<SpriteRenderer>();
        animator = GetComponentInChildren<Animator>();
        mainCamera = GameObject.FindGameObjectWithTag("MainCamera");
        audioSource = GetComponent<AudioSource>();
        mainCharColl = GetComponent<Collider2D>();
        gameController = GameObject.Find("GameController").GetComponent<GameController>();
    }

    public void GoToTitle()
    {
        Invoke("GoToTitleScreen", 4.0f);
    }

    public void GoToTitleScreen()
    {
        SceneManager.LoadScene(0);
        SceneManager.UnloadSceneAsync(1);
    }

    protected override void ComputeVelocity()
    {
        if (gameController.inElevator == false && gameController.pointsCounter.gameStarted == true)
        {
            Vector2 move = Vector2.zero;


            move.x = Input.GetAxis("Horizontal");
            if ((move.x < -0.01f || move.x > 0.01f) && grounded == true)
            {
                if (move.x == 0)
                {
                    walking = false;
                }
                else
                {
                    walking = true;
                    //animator.SetTrigger("walking");
                }
            }
            if ((move.x > -0.01f || move.x < 0.01f) && grounded == true)
            {
                if (move.x == 0)
                {
                    idling = true;
                    //animator.SetTrigger("idling");
                }
                else
                {
                    idling = false;
                }
            }



            if (Input.GetButtonDown("Jump") && grounded)
            {
                velocity.y = jumpTakeOffSpeed;
            }
            else if (Input.GetButtonUp("Jump"))
            {
                if (velocity.y > 0)
                {
                    velocity.y = velocity.y * 0.5f;
                }
            }

            if (move.x != 0.0f)
            {
                if (move.x > 0.0f)
                {
                    spriteRenderer.flipX = false;
                }
                else
                {
                    spriteRenderer.flipX = true;
                }
            }

            if (jumping == false)
            {
                if (grounded == false)
                {
                    jumping = true;
                    //animator.SetTrigger("jumping");
                }
                else
                {
                    jumping = false;
                }
            }

            //animator.SetBool("grounded", grounded);
            //animator.SetFloat("velocityX", Mathf.Abs(velocity.x) / maxSpeed);

            targetVelocity = move * maxSpeed * gameController.cameraSpeed / 8;
        }
    }

    public void ToggleRenderer(bool OnOff)
    {
        spriteRenderer.enabled = OnOff;
    }

    private void Update()
    {
        base.Update();
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            GoToTitleScreen();
        }   
    }

    private void OnBecameInvisible()
    {
        if (gameController != null)
        {
            if (gameController.inElevator == false)
            {
                if (losingCanvas != null)
                {
                    losingCanvas.gameObject.SetActive(true);
                    losingCanvas.FadeIn();
                }
                gameController.pointsCounter.gameStarted = false;
                gameController.pointsCounter.SetScoreField();
                gameController.TurnOffLevel();
            }
        }
    }
}