using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityStandardAssets.Characters.ThirdPerson;

public class EnemyBehaviourScript : MonoBehaviour
{
    public GameObject targetObj;

    Animator animator;

    NavMeshAgent navAgent;

    public ThirdPersonCharacter character;


    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
        navAgent = GetComponent<NavMeshAgent>();
        navAgent.updateRotation = false;
    }

    // Update is called once per frame
    void Update()
    {
        //print("navAgent.desiredVelocity -> " + navAgent.desiredVelocity.ToString());
        //print("navAgent.desiredVelocity.magnitude -> " + navAgent.desiredVelocity.magnitude.ToString());
        //print("navAgent.velocity -> " + navAgent.velocity.ToString());
        //print("navAgent.velocity.magnitude -> " + navAgent.velocity.magnitude.ToString());

        navAgent.SetDestination(targetObj.transform.position);


        if (navAgent.remainingDistance > navAgent.stoppingDistance)
            character.Move(navAgent.velocity, false, false);
        else
            character.Move(Vector3.zero, false, false);


        if (navAgent.remainingDistance < navAgent.stoppingDistance + 1.5f)
        {
            this.transform.LookAt(targetObj.transform);
            animator.SetBool("Attack2", true);
        }
        else
        {
            animator.SetBool("Attack2", false);
        }
    }


    private void OnCollisionEnter(Collision collision)
    {
        print("Enemy->OnCollisionEnter(): <" + collision.gameObject.tag + "> ::" + collision.gameObject.name);
    }
}
