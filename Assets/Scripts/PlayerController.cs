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
    
    // Start is called before the first frame update
    void Start()
    {
        state = States.idle;
        originalSprintTimer = 1000000000000f;
        walkSpeed = 4;
        
    }

    // Update is called once per frame
    void Update()
    {
        //xdir = Input.GetAxis("Horizontal");
        xdir = ((99 * xdir) + Input.GetAxis("Horizontal"))/100;
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
        }
    }

    // Update is called once per frame

   void SwitchToIdle()
   {
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
    }

   void SwitchToWalk()
   {
       sprintTimer = originalSprintTimer;//;
       state = States.walk;
       //animation is yes
   }

   void Walk()
   {
       xvector = xdir * walkSpeed * Time.deltaTime;
       transform.position = transform.position + new Vector3(xvector, 0, 0);
       sprintTimer -= Time.deltaTime;
       if ((Input.GetKeyDown(KeyCode.A) || Input.GetKeyDown(KeyCode.D) || Input.GetKeyDown(KeyCode.LeftArrow) || Input.GetKeyDown(KeyCode.RightArrow)) && sprintTimer > 0)
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
