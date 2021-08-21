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


        if (navAgent.remainingDistance < navAgent.stoppingDistance + 1.5f &&
            !animator.GetBool("HitWaiting") &&
            !animator.GetCurrentAnimatorStateInfo(0).IsName("GetHit Blend Tree")) //第0層正在播放的動畫名稱，是否叫做"XXX"
        {
            this.transform.LookAt(targetObj.transform);

            animator.SetBool("Attack1", false);
            animator.SetBool("Attack2", true);
            this.gameObject.BroadcastMessage("HurtEnable");

            //switch (Random.Range(0, 2))
            //{
            //    case 0:
            //        animator.SetBool("Attack1", false);
            //        animator.SetBool("Attack2", true);
            //        break;
            //    case 1:
            //        animator.SetBool("Attack1", true);
            //        animator.SetBool("Attack2", false);
            //        break;
            //    default:
            //        animator.SetBool("Attack1", false);
            //        animator.SetBool("Attack2", false);
            //        break;
            //}
        }
        else
        {
            animator.SetBool("Attack1", false);
            animator.SetBool("Attack2", false);
            this.gameObject.BroadcastMessage("HurtDisable");
        }
    }


    //private void OnCollisionEnter(Collision collision)
    //{
    //    print("Enemy->OnCollisionEnter(): <" + collision.gameObject.tag + "> ::" + collision.gameObject.name);
    //    print("Enemy->OnCollisionEnter(): <" + collision.collider.gameObject.tag + "> ::" + collision.collider.gameObject.name);
    //}

    #region Attack2 劍擊
    public AudioClip attack2_audio;
    public Transform attack2_pos;

    public GameObject attack2_effect;
    GameObject attack2_effect_playing;

    public void Attack2_Begin()
    {
        AudioSource.PlayClipAtPoint(attack2_audio, attack2_pos.position, 1);
        attack2_effect_playing = Instantiate(attack2_effect, attack2_pos);
    }

    public void Attack2_End()
    {
        Destroy(attack2_effect_playing);
    }
    #endregion
}
