using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    enum States
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
    
    // Start is called before the first frame update
    void Start()
    {
        state = States.idle;
        originalSprintTimer = .5f;
        walkSpeed = 1;
        
    }

    // Update is called once per frame
    void Update()
    {
        xdir = Input.GetAxis("Horizontal");
        ydir = Input.GetAxis("Vertical");
    }

    // Update is called once per frame

   void SwitchToIdle()
   {
       States.state = States.idle;
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
       sprintTimer = originalSprintTimer;
       States.state = States.walk;
       //animation is yes
   }

   void Walk()
   {
       xvector = xdir * walkSpeed * Time.deltaTime;
       transform.position = transform.position + new Vector3(xvector, 0, 0);
       sprintTimer -= Time.deltaTime;
       if ((GetKeyDown.A or GetKeyDown.D or GetKeyDown.ArrowLeft or GetKeyDown.ArrowRight) && sprintTimer > 0)
       {
           SwitchToSprint();
       }
   }

   void SwitchToSprint()
   {
       xdir.sprint
   }

   void Sprint()
   {
       
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
