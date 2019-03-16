using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class basicController : MonoBehaviour
{

    public float strafeSpeed;
    float vertSpeed = 3;
    float runSpeed = 7;
   public float rotSpeed = 100;
   public float backSpeed = 1.5f;
    public float attackDriftSpeed;
   public float translation;
    float rotation;
    float moveSpeed;
    float strafing;
   public bool isAttacking = false;
    public woodenSwordScript swordScript;
    public GameObject bballer;
    private bowlingBallerScript b_script;
    public float dodgeForce;
   public bool isDodging;
    public Vector3 myVelocity;

    public GameObject trail;

    
   
    private float swordAttackCooldown = 1;
    private float t;

    public Rigidbody myRig;

    public JointScript myJoint;

    public bool hasDied = false;

    public playerHealth myHealth;

    public GameObject woodenSword;
    public GameObject joint;


    public enum Weapons {Sword, Joint, Flamethrower, BowlingBall };
    public Weapons myWeapons;

    Animator myAnim;
    // Start is called before the first frame update
    void Start()
    {
        myAnim = GetComponent<Animator>();
        myWeapons = Weapons.Sword;
        b_script = bballer.GetComponent<bowlingBallerScript>();

        trail.SetActive(false);

        t = 0;

        myRig = GetComponent<Rigidbody>();
        isDodging = false;
       
    }

    // Update is called once per frame
    void FixedUpdate()
    {

        myVelocity = myRig.velocity;

        switch (myWeapons)
        {
            case Weapons.Sword:
                woodenSword.SetActive(true);
                joint.SetActive(false);
                break;

            case Weapons.Joint:
                woodenSword.SetActive(false);
                joint.SetActive(true);
                break;

            case Weapons.Flamethrower:
               
                break;
            case Weapons.BowlingBall:
                
                woodenSword.SetActive(false);
                joint.SetActive(false);


                break;
        }

      

        choosingWeapons();

        combatManager();

        movement();


        //if (isDodging == false)
        //{
        //    myRig.velocity = new Vector3(0, 0, 0);
        //}

        //else
        //{
        //    myRig.velocity = transform.forward * (-1) * 2;
        //}


        if (myHealth.isDead == true && hasDied == false)
        {
            hasDied = true;
            myAnim.SetTrigger("Death");
        }
    }

    void movement()
    {

        if (hasDied == false)
        {

       


        rotation = Input.GetAxis("Mouse X") * rotSpeed * Time.deltaTime;

        if (!Input.GetKey(KeyCode.LeftShift) && !Input.GetKey(KeyCode.W) && !Input.GetKey(KeyCode.S) && !isAttacking)
        {
            strafing = Input.GetAxis("Horizontal") * strafeSpeed * Time.deltaTime;

            transform.Translate(strafing, 0, 0);
        }

        else
        {
            strafing = 0;

            transform.Translate(0, 0, translation);
            
        }
      


        transform.Rotate(0, rotation, 0);
        if (isAttacking)
        {
            moveSpeed = attackDriftSpeed;
            translation = Input.GetAxis("Vertical") * attackDriftSpeed * Time.deltaTime;
            rotation = 0;
            
        }
        else
        {

           

             if (Input.GetKey(KeyCode.W))
            {

                if (Input.GetKey(KeyCode.LeftShift))
                {
                    translation = Input.GetAxis("Vertical") * runSpeed * Time.deltaTime;
                    myAnim.SetInteger("Walking", 2);
                    moveSpeed = runSpeed;
                }

               

                else
                {
                    myAnim.SetInteger("Walking", 1);
                   translation = Input.GetAxis("Vertical") * vertSpeed * Time.deltaTime;
                    moveSpeed = vertSpeed;
                }



            }

            else if (Input.GetKey(KeyCode.S))
            {
                if (Input.GetKey(KeyCode.S) && Input.GetKeyDown(KeyCode.Space))
                {
                    myAnim.SetTrigger("Dodge");
                    //myRig.AddForce(transform.forward * dodgeForce);
                    
                    
                }

                else
                {
                    myAnim.SetInteger("Walking", -1);
                    translation = Input.GetAxis("Vertical") * backSpeed * Time.deltaTime;
                }
                
            }

            else if (Input.GetKey(KeyCode.A) && !Input.GetKey(KeyCode.W))
            {
                myAnim.SetInteger("Walking", 12);
            }

            else if (Input.GetKey(KeyCode.D) && !Input.GetKey(KeyCode.W))
            {
                myAnim.SetInteger("Walking", 11);
                Debug.Log("Horcrux");
            }



            else
            {
                myAnim.SetInteger("Walking", 0);

                translation = 0;
            }

           
        }

        }
    }

    void finishedAttacking()
    {
        Invoke("TurnOffIsAttacking", 1);
        
        myAnim.SetInteger("Walking", 0);
        myAnim.SetBool("IsAttacking", false);
        swordScript.turnOffCollider();
        trail.SetActive(false);
        
    }

    void startAttacking()
    {
        swordScript.turnOnCollider();
        trail.SetActive(true);
    }

    void callBowlingScript()
    {


        b_script.instantiateBowlingBall();
     
    
    }

    void choosingWeapons()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            myWeapons = Weapons.Sword;

        }

        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            myWeapons = Weapons.Joint;
        }

        if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            myWeapons = Weapons.BowlingBall;
        }
    }

    void combatManager()
    {


        if (Input.GetKeyDown(KeyCode.Mouse0))
        {

            if (t <= Time.time)
            {
                t = 0.7f + Time.time;

                isAttacking = true;


                if (myWeapons == Weapons.Sword)
                {

                    myAnim.SetTrigger("SwordAttack");
                    myAnim.SetBool("IsAttacking", true);
                    myAnim.SetInteger("Walking", 0);





                }

                if (myWeapons == Weapons.Joint)
                {
                    myAnim.SetTrigger("JointAttack");
                    myAnim.SetBool("IsAttacking", true);
                    myAnim.SetInteger("Walking", 0);
                }

                if (myWeapons == Weapons.BowlingBall)
                {
                    myAnim.SetTrigger("BowlingAttack");
                    myAnim.SetBool("IsAttacking", true);
                    myAnim.SetInteger("Walking", 0);
                }
            }
           

        }

    }

    void Dodge()
    {
        isDodging = true;
        myRig.velocity = transform.forward * (-1) * 5 + transform.up * 2;
        Invoke("MakeDodgingFalse", 1f);
    }

    void TurnOffIsAttacking()
    {
        isAttacking = false;
    }

    void BeginJointAttack()
    {
        myJoint.TurnOn();
    }

    void EndJointAttack()
    {
        myJoint.TurnOff();
    }

    void MakeDodgingFalse()
    {
        isDodging = false;
    }

}
