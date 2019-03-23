using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Audio;


public class Redneckscript : MonoBehaviour
{


    private NavMeshAgent myAgent;
    private Collider myTrigger;
    public Transform[] patrolPoints;
    public int currentPoint;
    public Transform PlayerLocation;
    private float walkSpeed = 1f;
    private float runSpeed = 5f;
    private float detectionDistance = 12f;
    private float stopChaseDistance = 15f;
    private float attackDistance = 1.5f;
    private float goBackToChasingDistance = 3f;
    private bool hasRoared = true;
   public int attackCounter = 1;
    public bool waiting = true;
    public bool p;
    public float playerDist;
    public Transform lookAtPoint;
    public bool isAttacking = false;

    public Bat_hitter hitter;
    public bool timeToAttack;

    public bool isStunned;
    private bool triggered;
    public bool otherEnemyPresent;

    int enemiesNearby;

    playerHealth AGHealth;

    public characterHealth charHealth;


    private Vector3 lookTarget;
    public float lookAtSmoothFactor = 3f;

    Animator myAnim;

    float dist;

    public bool heDied = false;

    
    
    public bool hasDied = false;

    public enum RNModes { Idling, Walking, Chasing, Attacking, Stunned, Death };
    public RNModes myModes;


    // Start is called before the first frame update
    void Start()
    {
        myAgent = GetComponent<NavMeshAgent>();

        myAnim = GetComponent<Animator>();
        AGHealth = GameObject.FindGameObjectWithTag("pHealth").GetComponent<playerHealth>();
        myTrigger = GetComponent<Collider>();
        currentPoint = 0;

        GoToPoint();

        otherEnemyPresent = false;
        PlayerLocation = GameObject.FindGameObjectWithTag("Player").transform;

        enemiesNearby = 0;

        timeToAttack = true;

        isStunned = false;


        
        p = true;


        lookTarget = transform.position + transform.forward;

        myModes = RNModes.Walking;
        GoToPoint();
        

    }

    
    void FixedUpdate()

    {
        if (AGHealth.isDead == true && heDied == false)
        {
            myModes = RNModes.Walking;
            Debug.Log("testy");
            
            GoToPoint();
            heDied = true;

        }


        if (heDied == true)
        {
            playerDist = 100f;
        }

        else
        {
            playerDist = Vector3.Distance(transform.position, PlayerLocation.position);
        }

        if (charHealth.isDead == true)
        {
            myModes = RNModes.Death;
        }
        

        if (enemiesNearby > 0)
        {
            otherEnemyPresent = true;
        }

        else
        {
            otherEnemyPresent = false;
        }
       
        //if (charHealth.isDead == true)
        //{
        //    myModes = RNModes.Death;
        //}



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



        if (hasRoared == false)
        {
            myAgent.isStopped = true;
           transform.LookAt(PlayerLocation.position);
            myAnim.SetInteger("Walking", 3);
            

        }

        else
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
    }

    void Attacking()
    {
        
        transform.LookAt(PlayerLocation);
        myAgent.isStopped = true;
        if (playerDist >= goBackToChasingDistance && isAttacking == false)
        {
            myModes = RNModes.Chasing;
        }

        myAnim.SetInteger("Walking", 3);

        int t;

        if (otherEnemyPresent == true)
        {
            if (timeToAttack == false && p == true )
            {
                myAnim.SetInteger("Walking", 3);
                t = Random.Range(4, 6);
                
                Invoke("BackToAttack", t);
                Debug.Log("state 1");
                p = false;
               
            }



            if (timeToAttack == true)
            {

                timeToAttack = false;
             
                if (attackCounter % 2 == 0 && attackCounter % 7 != 0)
                {
                    myAnim.SetTrigger("attack1");
                    



                }

                else if (attackCounter % 2 != 0 && attackCounter % 7 != 0)
                {
                    myAnim.SetTrigger("attack2");
                    
                }

                else if (attackCounter % 7 == 0)
                {
                    myAnim.SetTrigger("attack3");
                    
                }

            }
        }

        else
        {
            if (timeToAttack == false && p == true)
            {
                myAnim.SetInteger("Walking", 3);
                //t = Random.Range(3, 6);
                t = 5;
                Invoke("BackToAttack", t);
                Debug.Log("state 2");
                p = false;



            }



            if (timeToAttack == true)
            {

                timeToAttack = false;

                if (attackCounter % 2 == 0 && attackCounter % 7 != 0)
                {
                    myAnim.SetTrigger("attack1");
                    



                }

                else if (attackCounter % 2 != 0 && attackCounter % 7 != 0)
                {
                    myAnim.SetTrigger("attack2");
                    
                }

                else if (attackCounter % 7 == 0)
                {
                    myAnim.SetTrigger("attack3");
                    
                }

            }
        }

        


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

    private void OnTriggerEnter(Collider other)
    {

        

        if (other.gameObject.tag == "Enemy")
        {
            enemiesNearby++;
            

        }

        if (other.gameObject.tag == "Smoke_emitter")
        {

            if (isStunned == false)
            {
                Debug.Log("Stunned");
                myModes = RNModes.Stunned;
                
            }
            
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "Enemy")
        {
            enemiesNearby--;
        }
    }




    void increaseAttackCounter()
    {
        attackCounter++;
        
       hitter.TurnOffCollider();
        
        
        
       
    }

    void TurnOffIsAttacking()
    {
        isAttacking = false;
    }
   
       
    void stopRoaring()
    {
        hasRoared = true;
    }

   

    void beginAttack()
    {
        hitter.TurnBackOn();
        isAttacking = true;
    }

    void backToPatrolling()
    {
        myModes = RNModes.Walking;
        
        waiting = false;
        Invoke("makeWaitingTrue", 5);
        
    }

    void makeWaitingTrue()
    {
        waiting = true;
    }

    void BackToAttack()
    {
        timeToAttack = true;
      
        p = true;
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
            increaseAttackCounter();




            myTrigger.enabled = false;

            
        }
      

    }



}






