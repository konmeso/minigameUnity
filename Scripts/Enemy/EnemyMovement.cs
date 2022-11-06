using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyMovement : MonoBehaviour
{

    [SerializeField] float damage;
    float lastAttackTime = 0;
    float attackCooldown = 2;
    [SerializeField] float stoppingDistance;
    [SerializeField] float targetingDistance;

    NavMeshAgent agent;
    GameObject target;
    Animator anim;



    // Start is called before the first frame update
    private void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        target = GameObject.FindGameObjectWithTag("Player");
        anim = GetComponentInChildren<Animator>();
    }

    // Update is called once per frame
    private void Update()
    {
        float dist = Vector3.Distance(transform.position, target.transform.position);
        if(dist<stoppingDistance)
        {
            StopEnemy();
            Attack();
        }
        else if(dist>=stoppingDistance && dist<targetingDistance)
        {
            GoToTarget();
        }
        
    }

    private void GoToTarget()
    {
        agent.isStopped = false;
        agent.SetDestination(target.transform.position);
        anim.SetBool("isWalking", true);
    }

    private void StopEnemy()
    {
        agent.isStopped = true;
        anim.SetBool("isWalking", false);
    }

    private void Attack()
    {
        if(Time.time - lastAttackTime > attackCooldown)
            {
                lastAttackTime = Time.time;
                target.GetComponent<CharacterStats>().TakeDamage(damage);
                target.GetComponent<CharacterStats>().CheckHealth();    
            }
     }



}
