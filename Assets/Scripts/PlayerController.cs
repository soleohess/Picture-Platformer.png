using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    enum States // controls are jump (space or W), left (arrow or A), and right (arrow or D);;
    {
        idle,
        walk,
        sprint,
        jump,
        fall,
        wallgrab,
        slide
    }

    States state;
    private int walkSpeed;
    private float sprintTimer;
    private float originalSprintTimer; // can change to public for tweaking
    private float xdir;
    private float ydir;
    private float xvector;
    private float yvector;
    public float forceAmount = 10f; // Jump Force
    [SerializeField] private Rigidbody2D rb;
    private bool canJump;

    // Start is called before the first frame update
    void Start()
    {
        state = States.idle;
        originalSprintTimer = 100f;
        walkSpeed = 4;
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        //xdir = Input.GetAxis("Horizontal");
        xdir = ((99 * xdir) + Input.GetAxis("Horizontal")) / 100;
        ydir = Input.GetAxis("Vertical");

        switch (state)
        {
            case States.idle:
                Idle();
                break;
            case States.walk:
                Walk();
                break;
            case States.sprint:
                Sprint();
                break;
            case States.slide:
                Slide();
                break;
            case States.jump:
                Jump();
                break;
        }

        if (Input.GetKeyDown("space"))
        {
            SwitchToJump();
        }
    }

    void SwitchToIdle()
    {
        Debug.Log("SwitchToIdle");
        state = States.idle;
        //learn how animation yes
    }

    void Idle()
    {
        //one of the conditions for exiting idle state
        if (xdir != 0)
        {
            SwitchToWalk();
        }

        sprintTimer -= Time.deltaTime;
        if (((Input.GetKeyDown(KeyCode.A) || Input.GetKeyDown(KeyCode.LeftArrow)) && xdir < 0 ||
             (Input.GetKeyDown(KeyCode.D) || Input.GetKeyDown(KeyCode.RightArrow)) && xdir > 0) && sprintTimer > 0)
        {
            SwitchToSprint();
        }

    }

    void SwitchToWalk()
    {
        Debug.Log("SwitchToWalk");
        sprintTimer = originalSprintTimer; //;
        state = States.walk;
        //animation is yes
    }

    void Walk()
    {
        xvector = xdir * walkSpeed * Time.deltaTime;
        transform.position = transform.position + new Vector3(xvector, 0, 0);
        sprintTimer -= Time.deltaTime;
        if (((Input.GetKeyDown(KeyCode.A) || Input.GetKeyDown(KeyCode.LeftArrow)) && xdir < 0 ||
             (Input.GetKeyDown(KeyCode.D) || Input.GetKeyDown(KeyCode.RightArrow)) && xdir > 0) && sprintTimer > 0)
        {
            SwitchToSprint();
        }

        if (xdir == 0)
        {
            SwitchToIdle();
        }
    }

    void SwitchToSprint()
    {
        Debug.Log("SwitchToSprint");
        //animaation hyeaaa
        state = States.sprint;
    }

    void Sprint()
    {
        xvector = xdir * 2 * walkSpeed * Time.deltaTime;
        transform.position = transform.position + new Vector3(xvector, 0, 0);

        if (xdir == 0)
        {
            SwitchToSlide();
        }
    }

    void SwitchToSlide()
    {
        Debug.Log("SwitchToSlide");
        //animaation
        state = States.slide;
    }

    void Slide()
    {
        xvector *= 1 - (.01f * Time.deltaTime); // e?
        transform.position = transform.position + new Vector3(xvector, 0, 0);
        if (xvector <= 0.01f)
        {
            SwitchToIdle();
        }
    }

    void SwitchToJump()
    {
        Debug.Log("SwitchToJump");
        state = States.jump;
        rb.AddForce(Vector2.up * forceAmount, ForceMode2D.Impulse);
        //animaation
    }

    void Jump()
    {
        Debug.Log("Jump");
        canJump = Physics2D.Raycast(transform.position, Vector2.down, 1f);
        Debug.DrawRay(transform.position, Vector2.down, Color.red);
        if (canJump)
        {
            SwitchToIdle();
        }
        xvector = xdir * walkSpeed * Time.deltaTime;
        transform.position = transform.position + new Vector3(xvector, 0, 0);
        //rb.AddForce(Vector2.up * forceAmount, ForceMode2D.Impulse);
        //animaation
    }

    void SwitchToFall()
    {
        
        //animaation
    }

    void Fall()
    {
        
        //animaation
    }
    
    
//not using SwitchState but leaving for reconsideration later
   /* void SwitchState(States state)
    {
        switch (state)
        {
            case States.idle:
                print("idle state");
                Idle();
                break;

            case States.walk:
                print("walk state");
                break;

            case States.sprint:
                print("sprint state");
                break;

            case States.jump:
                print("jump state");
                break;

            case States.fall:
                print("fall state");
                break;

            case States.wallgrab:
                print("wallgrab state");
                break;

            case States.slide:
                print("slide state");
                break;

        }
    }
    */
}
