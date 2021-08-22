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
        next_can_attack = Time.time;
    }

    // Update is called once per frame
    void Update()
    {
        Navigate();
        AutoAttack();


    }

    void Navigate()
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
    }

    void AutoAttack()
    {
        if ((navAgent.remainingDistance < (navAgent.stoppingDistance + 1.5f)) &&
            this.next_can_attack < Time.time &&
            !animator.GetCurrentAnimatorStateInfo(0).IsName("GetHit Blend Tree")) //第0層正在播放的動畫名稱，是否叫做"XXX"
        {
            this.next_can_attack += 3.0f;

            this.transform.LookAt(targetObj.transform);

            animator.SetTrigger("Attack2");
            this.gameObject.BroadcastMessage("HurtEnable");
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
        this.gameObject.BroadcastMessage("HurtDisable");
    }
    #endregion

    private float next_can_attack;
    public void BeHit()
    {
        next_can_attack = Time.time + 6.5f;
    }

    void Die()
    {
        this.enabled = false;

        NavMeshAgent navAgent = GetComponent<NavMeshAgent>();
        if (navAgent != null) navAgent.isStopped = true;

        StartCoroutine(Clear());
    }
    IEnumerator Clear()
    {
        yield return new WaitForSeconds(3.0f);
        Destroy(this.gameObject);
    }

}
