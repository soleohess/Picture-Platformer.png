using System.Collections;
using System.Collections.Generic;
using UnityEditor.Overlays;
using UnityEngine;
using UnityEngine.UIElements;

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
        slide,
        slidei
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
    private Animator animate;
    private SpriteRenderer renderer;
    private GameObject visual;
    // Start is called before the first frame update
    void Start()
    {
        visual = transform.GetChild(0).gameObject;
        animate = visual.GetComponent<Animator>();
        renderer = visual.GetComponent<SpriteRenderer>();
        state = States.idle;
        originalSprintTimer = 1f;
        walkSpeed = 4;
        rb = GetComponent<Rigidbody2D>();
        SwitchToIdle();
    }

    // Update is called once per frame
    void Update()
    {
        //xdir = Input.GetAxis("Horizontal");
        xdir = ((99 * xdir) + Input.GetAxis("Horizontal")) / 100;
        ydir = Input.GetAxis("Vertical");
        if (xdir < .001 && xdir > -.001)
        {
            xdir /*;*/ = 0;//;//;//;//;//;//;//;//;////////////////////////////////////////////////////////////////////////// 
        }
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
            case States.slidei:
                Slidei();
                break;
            /*
             case States.jump:
                Jump();
                break;
            case States.fall:
                Fall();
                break;
            */
        }

        if (Input.GetKeyDown("space"))
        {
            //SwitchToJump();
        }
        
        //Debug.Log("State is " + state.ToString());
    }

    void SwitchToIdle()
    {
        Debug.Log("SwitchToIdle");
        state = States.idle;
        animate.Play("Idle");
        //learn how animation yes
    }

    void Idle()
    {
        if (((Input.GetKeyDown(KeyCode.A) || Input.GetKeyDown(KeyCode.LeftArrow)) && xdir < 0 || 
             (Input.GetKeyDown(KeyCode.D) || Input.GetKeyDown(KeyCode.RightArrow)) && xdir > 0) && sprintTimer > 0)
        {
            Debug.Log("Idle to sprint");
            SwitchToSprint();
        }
        //animate.Play("Idle");
        //one of the conditions for exiting idle state
        if (xdir != 0)
        {
            SwitchToWalk();
        }

        sprintTimer -= Time.deltaTime;
        //Debug.Log("sprintTimer is " + sprintTimer.ToString());

    }

    void SwitchToWalk()
    {
        Debug.Log("SwitchToWalk")   ;
        sprintTimer = originalSprintTimer              /*;*/        ;
        state = States.walk;//;
        //animaation is yes
        animate.Play("Walk");
    }

    void Walk()
    {
        Move();
        sprintTimer -= Time.deltaTime;
        //Debug.Log("sprintTimer is " + sprintTimer.ToString());
        if (((Input.GetKeyDown(KeyCode.A) || Input.GetKeyDown(KeyCode.LeftArrow)) && xdir < 0 ||
             (Input.GetKeyDown(KeyCode.D) || Input.GetKeyDown(KeyCode.RightArrow)) && xdir > 0) && sprintTimer > 0)
        {
            Debug.Log("Walk to sprint");
            SwitchToSprint();
        }

        if (xdir == 0)
        {
            SwitchToIdle();
        }
    }

    void Move()
    {
        xvector = xdir * walkSpeed * Time.deltaTime;
        transform.position = transform.position + new Vector3(xvector, 0, 0);
        
    }

    void SwitchToSprint()
    {
        Debug.Log("SwitchToSprint");
        //animaation hyeaaa
        state = States.sprint;
        
    }

    void Sprint()
    {
        Move();
        Move();

        if (!Input.GetKey(KeyCode.LeftArrow) && !Input.GetKey(KeyCode.A) && xdir < 0) // if moving left and let go of button
        {
            if (Input.GetKey(KeyCode.I))
            {
                SwitchToSlidei();
            }
            else
            {
                SwitchToSlide();
            }
        }
        if (!Input.GetKey(KeyCode.RightArrow) && !Input.GetKey(KeyCode.D) && xdir > 0) // if moving right and let go of button
        {
            if (Input.GetKey(KeyCode.I))
            {
                SwitchToSlidei();
            }
            else
            {
                SwitchToSlide();
            }
        }

    }
    
    void SwitchToSlide()
    {
        Debug.Log("SwitchToSlide");
        //animaation
        state = States.slide;
        animate.Play("Slip");
    }

    void SwitchToSlidei()
    {
        Debug.Log("SwitchToSlidei");
        //animaation
        state = States.slidei;
        animate.Play("Slip");
    }
    
    void Slidei()
    {
        //xvector *= 1 - (.01f * Time.deltaTime); // e?
        //Pert
        xvector *= Mathf.Exp(1 - (-0.0001f * Time.deltaTime));
        transform.position = transform.position + new Vector3(xvector, 0, 0);
        if (xvector <= 0.01f && xvector > -0.01f)
        {
            SwitchToIdle();
        }
    }
    void Slide()
    {
        //xvector *= 1 - (.01f * Time.deltaTime); // e?
        //Pert
        xvector *= Mathf.Exp((-0.0000000001f * Time.deltaTime));
        transform.position = transform.position + new Vector3(xvector, 0, 0);
        Debug.Log(xvector);
        if (xvector <= 0.01f && xvector > -0.01f)
        {
            SwitchToIdle();
        }
    }

    /*
    void SwitchToJump()
    {
        Debug.Log("SwitchToJump");
        state = States.jump;
        rb.AddForce(Vector2.up * forceAmount, ForceMode2D.Impulse);
        //animaation
        animate.Play("Jump");
    }

    void Jump()
    {
        Debug.Log("Jump");
        canJump = Physics2D.Raycast(transform.position, Vector2.down, .04f);
        Debug.DrawRay(transform.position, Vector2.down, Color.red);
        if (canJump)
        {
            SwitchToIdle();
        }

        if (!canJump)
        {
            SwitchToFall();
        }
        Move();
        //rb.AddForce(Vector2.up * forceAmount, ForceMode2D.Impulse);
        //animaation
    }

    void SwitchToFall()
    {
        Debug.Log("SwitchToFall");
        animate.Play("Fall");
    }

    void Fall()
    {
        Move();
        //animaation
    }
    */
    
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
