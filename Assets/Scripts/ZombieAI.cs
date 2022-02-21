using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class ZombieAI : MonoBehaviour
{
    private NavMeshAgent theAgent;
    private Rigidbody rigidbody;
    private Animator anim;
    public GameObject player;

    private float velocity;
    private Vector3 previous;

    private bool isAttacking;
    private bool finishAttack;
    private int testCounter = 0;
    private int attackTime = 2;

    void Start()
    {
        theAgent = GetComponent<NavMeshAgent>();
        rigidbody = GetComponent<Rigidbody>();
        anim = GetComponent<Animator>();

        isAttacking = false;
        finishAttack = false;
    }

    void Update()
    {
        theAgent.SetDestination(player.transform.position);
        velocity = ((transform.position - previous).magnitude) / Time.deltaTime;
        previous = transform.position;

        if (isAttacking)
        {
            anim.SetBool("isAttacking", true);
        } else
        {
            anim.SetBool("isAttacking", false);
        }
    }

    private void FixedUpdate()
    {
        if (isAttacking)
        {
            rigidbody.velocity = Vector2.zero;
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject == player)
        {
            isAttacking = true;
        }
    }

    private void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject == player)
        {
            isAttacking = false;
        }
    }

    IEnumerator Attack()
    {
        isAttacking = true;
        Debug.Log("attack" + testCounter);

        // trigger attack animation
        yield return new WaitForSeconds(attackTime); // waits for the number of seconds that is attack time
        isAttacking = false;        
    }

    private void OnCollisionStay(Collision collision)
    {

        //if (collision.gameObject == player)
        //{
        //    if (!isAttacking)
        //    {
        //        StartCoroutine(Attack());
        //        testCounter++;
        //    }
        //}
    }
}
