using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovementNew : MonoBehaviour
{

    // VARIABLES
    [SerializeField] private float moveSpeed;
    [SerializeField] private float walkSpeed;
    [SerializeField] private float runSpeed;

    private Vector3 moveDirection;
    private Vector3 velocity;

    [SerializeField] private bool isGrounded;
    [SerializeField] private float groundCheckDistance;
    [SerializeField] private LayerMask groundMask;
    [SerializeField] private float gravity; 
    [SerializeField] private float jumpHeight;
    [SerializeField] float damage;

    float lastRegen;
    float staminaRegenSpeed = 1f;
    float staminaRegenAmount = 1f;

    public Transform punchAttackPoint;
    public Transform kickAttackPoint;
    public float attackRange = 0.5f;
    public LayerMask enemyLayers;

    // REFERENCES
    private CharacterController controller;
    private Animator anim; 

    CharacterStats playerStats;
    // GameObject target;




    // Start is called before the first frame update
    private void Start()
    {
        controller = GetComponent<CharacterController>();
        anim = GetComponentInChildren<Animator>();
        playerStats = GetComponent<CharacterStats>();
    }

    // Update is called once per frame
    private void Update()
    {

        //OnTriggerEnter();

        Move();

        if(Input.GetKeyDown(KeyCode.Mouse0))
        {
            StartCoroutine(PunchAttack());
        }

        if(Input.GetKeyDown(KeyCode.Q))
        {
            KickAttack();
        }

    }

    private void Move()
    {

        // Check Sphere just draws a sphere and checks. 
        isGrounded = Physics.CheckSphere(transform.position, groundCheckDistance, groundMask);

        if (isGrounded && velocity.y < 0)
        {
            velocity.y = -2f;
        }

        float moveZ = Input.GetAxis("Vertical");

        moveDirection = new Vector3(0, 0, moveZ);
        moveDirection = transform.TransformDirection(moveDirection);
        
        if(isGrounded)
        {

            if (playerStats.currStamina > 0)
            {
                if (moveDirection != Vector3.zero && !Input.GetKey(KeyCode.LeftShift))
                {
                    Walk();
                    RegenStamina();
                }
                else if(moveDirection != Vector3.zero && Input.GetKey(KeyCode.LeftShift))
                {       
                    Run();
                }
                else if(moveDirection == Vector3.zero)
                {
                    Idle();
                    RegenStamina();
                }   
            }
            else
            {
                RegenStamina();
                if (moveDirection != Vector3.zero && !Input.GetKey(KeyCode.LeftShift))
                {
                    Walk();
                }
                else if(moveDirection != Vector3.zero && Input.GetKey(KeyCode.LeftShift))
                {       
                    Walk();
                }
                else if(moveDirection == Vector3.zero)
                {
                    Idle();
                }                  
            }
            


            moveDirection *= moveSpeed;

            if(Input.GetKeyDown(KeyCode.Space))
            {
                Jump();
            }
        }


        // Time.deltaTime makes sure that no matter how many
        // frames we've got, we are always going to move
        // in proportion of actual time.

        // First we move on plane and then we apply gravity
        controller.Move(moveDirection * Time.deltaTime);
        velocity.y += gravity * Time.deltaTime;
        controller.Move(velocity* Time.deltaTime);
    }

    private void Idle()
    {
        anim.SetFloat("Speed", 0, 0.1f, Time.deltaTime);
    }

    private void Walk()
    {
        moveSpeed = walkSpeed;
        anim.SetFloat("Speed",0.5f, 0.1f, Time.deltaTime);
    }

    private void Run()
    {
        moveSpeed = runSpeed;
        anim.SetFloat("Speed", 1, 0.1f, Time.deltaTime);
        playerStats.currStamina -= 10 * Time.deltaTime;
        playerStats.CheckStamina();
    }

    private void Jump()
    {
        velocity.y = Mathf.Sqrt(jumpHeight*(-2)*gravity);
    }

    private IEnumerator PunchAttack()
    {
        anim.SetLayerWeight(anim.GetLayerIndex("Attack Layer"), 1);
        anim.SetTrigger("Punch Attack");

        yield return new WaitForSeconds(0.9f);
        anim.SetLayerWeight(anim.GetLayerIndex("Attack Layer"), 0);

        Collider[] hitEnemies = Physics.OverlapSphere(punchAttackPoint.position, attackRange, enemyLayers);

        foreach(Collider enemy in hitEnemies)
        {   
            Debug.Log("We hit");
           // target = GameObject.FindGameObjectWithTag("Enemy");
            enemy.GetComponent<CharacterStats>().TakeDamage(damage);

            // target.GetComponent<CharacterStats>().TakeDamage(damage);
        }

    }

    void KickAttack()
    {
        anim.SetTrigger("Kick Attack");
        Collider[] hitEnemies = Physics.OverlapSphere(kickAttackPoint.position, attackRange, enemyLayers);

        foreach(Collider enemy in hitEnemies)
        {   
            Debug.Log("We hit");
           // target = GameObject.FindGameObjectWithTag("Enemy");
            enemy.GetComponent<CharacterStats>().TakeDamage(damage);

            // target.GetComponent<CharacterStats>().TakeDamage(damage);
        }
    }

    void OnDrawGizmosSelected() 
    {
        Gizmos.DrawWireSphere(punchAttackPoint.position, attackRange);    

        Gizmos.DrawWireSphere(kickAttackPoint.position, attackRange);    
    }


    void RegenStamina()
    {
        if(Time.time - lastRegen > staminaRegenSpeed)
        {
            playerStats.currStamina += staminaRegenAmount;
            lastRegen = Time.time;
            playerStats.CheckStamina();
        }
    }

    void OnTriggerEnter(Collider other) 
    {
        if(other.gameObject.tag == "Enemy")
        {
           // print("Peos");
        }
    }   
        
    

}
