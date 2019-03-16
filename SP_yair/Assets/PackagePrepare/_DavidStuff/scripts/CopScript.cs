using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Audio;


public class CopScript : MonoBehaviour
{


    private NavMeshAgent myAgent;
    private Collider myTrigger;
    public Transform[] patrolPoints;
    public int currentPoint;
    public Transform PlayerLocation;
    private float walkSpeed = 1f;
    private float runSpeed = 5f;
    private float detectionDistance = 10f;
    private float stopChaseDistance = 12f;
    private float attackDistance = 8f;
    private float goBackToChasingDistance =10f;
    public bool is_Stopped;

    public GunShooter shooty;
    bool isStunned;



    public bool waiting = true;

    public float playerDist;
    public Transform lookAtPoint;

    //public Bat_hitter hitter;
   
    
    public characterHealth charHealth;


    private Vector3 lookTarget;
    public float lookAtSmoothFactor = 3f;

    Animator myAnim;

    float dist;

    
    
    public bool hasDied = false;

    public enum RNModes { Idling, Walking, Chasing, Attacking, Stunned, Death };
    public RNModes myModes;


    // Start is called before the first frame update
    void Start()
    {

        
        myAgent = GetComponent<NavMeshAgent>();

        myAnim = GetComponent<Animator>();
        myTrigger = GetComponent<Collider>();
        currentPoint = 0;

        GoToPoint();
        PlayerLocation = GameObject.FindGameObjectWithTag("Player").transform;

        //hitter = GameObject.FindWithTag("Bat_hitter").GetComponent<Bat_hitter>();

        isStunned = false;

        lookTarget = transform.position + transform.forward;

        myModes = RNModes.Walking;
        GoToPoint();
        

    }

    
    void FixedUpdate()

    {
        playerDist = Vector3.Distance(transform.position, PlayerLocation.position);
        is_Stopped = myAgent.isStopped;



        if (charHealth.isDead == true)
        {
            myModes = RNModes.Death;
        }



        switch (myModes)
        {
            case RNModes.Idling:
                
                Idling();
                break;

            case RNModes.Walking:
                
                Walking();
                break;

            case RNModes.Chasing:
               
                Chasing();
               
                break;
            case RNModes.Attacking:
                
                Attacking();
                break;
            case RNModes.Stunned:
                if (isStunned == false)
                {
                    Stunned();
                }
                break;

            case RNModes.Death:
                
               Death();
                break;
        }

    





    }

    void Idling()
    {
        myAgent.isStopped = true;
      
        // transform.LookAt(PlayerLocation);
        myAnim.SetInteger("Walking", 0);

        if (playerDist <= detectionDistance)
        {
            myModes = RNModes.Chasing;
            

        }

        if (waiting == true)
        {
            Invoke("backToPatrolling", 5);
        }

    }

    void Walking()
    {
        if (myAgent.remainingDistance <= 0.3f  )
        {
            if (waiting == true)
            {
                myModes = RNModes.Idling;
                
            }

            else
            {
                GoToPoint();
                
            }
           
        }


        if (playerDist <= detectionDistance)
        {
            myModes = RNModes.Chasing;
           
        }

        myAnim.SetInteger("Walking", 1);
        myAgent.isStopped = false;

        myAgent.speed = walkSpeed ;
    }

    void Chasing()
    {




       
       

            myAgent.isStopped = false;
        myAgent.speed = runSpeed;

        myAnim.SetInteger("Walking", 2);
        myAgent.SetDestination(PlayerLocation.position);

        if (playerDist >= stopChaseDistance)
        {
            myModes = RNModes.Walking;
            GoToPoint();
           

        }

            if (playerDist <= attackDistance)
            {
                myModes = RNModes.Attacking;
            }

        
    }

    void Attacking()
    {
        
        transform.LookAt(PlayerLocation);
        myAgent.isStopped = true;
        if (playerDist >= goBackToChasingDistance)
        {
            myModes = RNModes.Chasing;
        }


        myAnim.SetInteger("Walking", 7);

    }

    



    public void GoToPoint()
    {
      


            myAgent.SetDestination(patrolPoints[currentPoint].position);
            currentPoint++;
            if (currentPoint >= patrolPoints.Length)
            {
                currentPoint = 0;
            }
        
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Smoke_emitter")
        {
            if (isStunned == false)
            {
                Debug.Log("Stunned");
                myModes = RNModes.Stunned;

            }
        }

    }


  

   

   
       
   

    void beginAttack()
    {
        //hitter.TurnBackOn();
    }

    void backToPatrolling()
    {
        myModes = RNModes.Walking;
        
        waiting = false;
        Invoke("makeWaitingTrue", 5);
        
    }

    void FireTheGun()
    {
        shooty.FireGun();
    }
    void makeWaitingTrue()
    {
        waiting = true;
    }

    void Stunned()
    {
        myAnim.SetInteger("Walking", 13);
        myAgent.isStopped = true;
        isStunned = true;
        Invoke("UnStun", 7);
        Debug.Log("Dongus");

    }

    void UnStun()
    {
        myModes = RNModes.Idling;
        isStunned = false;
    }

    void Death()
    {
        if (charHealth.isDead == true && hasDied == false)
        {
            myAgent.isStopped = true;

            hasDied = true;
            myAnim.SetTrigger("Death");


            Debug.Log("Cop death");

            myTrigger.enabled = false;


        }

    }



}






