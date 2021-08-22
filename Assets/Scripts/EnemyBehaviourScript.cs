using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityStandardAssets.Characters.ThirdPerson;

public class EnemyBehaviourScript : MonoBehaviour
{
    Vector3 patrol_start_position;
    float patrol_update_timeline;
    public float patrol_guard_scope;
    Vector3 patrol_random_position = Vector3.zero;

    GameObject targetObj;

    Animator animator;

    NavMeshAgent navAgent;

    public ThirdPersonCharacter character;

    public int score_value = 1000;


    // Start is called before the first frame update
    void Start()
    {
        patrol_start_position = this.transform.position;
        patrol_update_timeline = Time.time;
        targetObj = GameObject.Find("Player");
        animator = GetComponent<Animator>();
        navAgent = GetComponent<NavMeshAgent>();
        navAgent.updateRotation = false;
        next_can_attack = Time.time;
    }

    // Update is called once per frame
    void Update()
    {
        if (targetObj != null && Vector3.Distance(patrol_start_position, targetObj.transform.position) < patrol_guard_scope)
        {
            Navigate();
            AutoAttack();
        }
        else
        {
            Patrol();
        }
    }

    void Navigate()
    {
        //print("navAgent.desiredVelocity -> " + navAgent.desiredVelocity.ToString());
        //print("navAgent.desiredVelocity.magnitude -> " + navAgent.desiredVelocity.magnitude.ToString());
        //print("navAgent.velocity -> " + navAgent.velocity.ToString());
        //print("navAgent.velocity.magnitude -> " + navAgent.velocity.magnitude.ToString());

        navAgent.SetDestination(targetObj.transform.position);
        navAgent.speed = 2.5f;

        if (navAgent.remainingDistance > navAgent.stoppingDistance)
            character.Move(navAgent.velocity, false, false);
        else
            character.Move(Vector3.zero, false, false);
    }

    void AutoAttack()
    {
        if ((navAgent.remainingDistance < (navAgent.stoppingDistance + 1.25f)) &&
            this.next_can_attack < Time.time &&
            !animator.GetCurrentAnimatorStateInfo(0).IsName("GetHit Blend Tree")) //第0層正在播放的動畫名稱，是否叫做"XXX"
        {
            this.next_can_attack += 2.75f;

            this.transform.LookAt(targetObj.transform);

            animator.SetTrigger("Attack2");
            this.gameObject.BroadcastMessage("HurtEnable");
        }
    }

    void Patrol() // 巡邏
    {
        if (patrol_update_timeline < Time.time)
        {
            patrol_update_timeline = Time.time + 5.0f;

            patrol_random_position = patrol_start_position +
                new Vector3(Random.Range(-patrol_guard_scope, patrol_guard_scope), 0.0f, Random.Range(-patrol_guard_scope, patrol_guard_scope));
        }

        navAgent.SetDestination(patrol_random_position);
        navAgent.speed = 0.5f;

        if (navAgent.remainingDistance > navAgent.stoppingDistance)
            character.Move(navAgent.velocity, false, false);
        else
            character.Move(Vector3.zero, false, false);
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
        next_can_attack = Time.time + 3.5f;
    }

    void Die()
    {
        this.enabled = false;

        NavMeshAgent navAgent = GetComponent<NavMeshAgent>();
        if (navAgent != null) navAgent.isStopped = true;

        StartCoroutine(Clear());

        GameObject.Find("Player").GetComponent<UI_ScoreBehaviourScript>().AddScore(score_value);
    }
    IEnumerator Clear()
    {
        yield return new WaitForSeconds(3.0f);
        Destroy(this.gameObject);
    }

}
