using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBehaviourScript : MonoBehaviour
{
    Animator animator;
    CharacterController cc;

    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
        cc = GetComponent<CharacterController>();
    }

    // Update is called once per frame
    void Update()
    {
        float dy = Input.GetAxis("Vertical");
        if (Input.GetKey(KeyCode.Space)) dy *= 2;
        animator.SetFloat("Speed", dy, 0.08f, Time.deltaTime);
        //animator.SetFloat("Speed", dy);
        float dx = Input.GetAxis("Horizontal");
        animator.SetFloat("Direction", dx, 0.08f, Time.deltaTime);
        //animator.SetFloat("Direction", dx);

        //this.transform.Rotate(new Vector3(0, dx * Time.deltaTime * 10.0f, 0), Space.Self);
        //cc.Move(this.transform.forward * dy * Time.deltaTime * 5.0f);

        animator.SetBool("Jump", Input.GetKey(KeyCode.E));
        animator.SetBool("Attack1", Input.GetKey(KeyCode.R) || Input.GetMouseButton(0));
        animator.SetBool("Attack2", Input.GetKey(KeyCode.T) || Input.GetMouseButton(1));
    }

    //private void OnCollisionEnter(Collision collision)
    //{
    //    print("Player->OnCollisionEnter(): <" + collision.gameObject.tag + "> ::" + collision.gameObject.name);
    //    print("Player->OnCollisionEnter(): <" + collision.collider.gameObject.tag + "> ::" + collision.collider.gameObject.name);
    //}

    /*
    private void OnAnimatorMove()
    {
        float dy = Input.GetAxis("Vertical");
        animator.SetFloat("Speed", dy);
        float dx = Input.GetAxis("Horizontal");
        animator.SetFloat("Direction", dx);

        this.transform.Rotate(new Vector3(0, dx * Time.deltaTime * 75.0f, 0), Space.World);
        cc.Move(this.transform.forward * dy * Time.deltaTime * 5.0f);
    }
    */

    #region Attack1 魔法空氣球
    public AudioClip attack1_audio;
    public Transform attack1_pos;

    public GameObject attack1_effect;
    GameObject attack1_effect_playing;

    public GameObject attack1_magic;
    GameObject attack1_magic_playing;
    //public AudioClip attack1_magic_audio;


    public void Attack1_Begin()
    {
        AudioSource.PlayClipAtPoint(attack1_audio, attack1_pos.position, 1);
        attack1_effect_playing = Instantiate(attack1_effect, attack1_pos); // 其實好像不用Destroy()?? 隨Transform parent消失??

        //attack1_magic_playing = Instantiate(attack1_magic, attack1_pos.position - new Vector3(0, 0.25f, 0), Quaternion.Euler(0, 0, 0));
        attack1_magic_playing = Instantiate(attack1_magic, attack1_pos.position + new Vector3(0, 0.1f, 0), Quaternion.Euler(0, 0, 0));
        attack1_magic_playing.GetComponent<Rigidbody>().AddForce(this.transform.forward * 1500);
        //AudioSource.PlayClipAtPoint(attack1_magic_audio, attack1_magic_playing.transform.position, 1);

        this.gameObject.BroadcastMessage("HurtEnable");
    }

    public void Attack1_End()
    {
        Destroy(attack1_effect_playing);

        this.gameObject.BroadcastMessage("HurtDisable");
    }
    #endregion

    #region Attack2 劍擊
    public AudioClip attack2_audio;
    public Transform attack2_pos;

    public GameObject attack2_effect;
    GameObject attack2_effect_playing;

    public void Attack2_Begin()
    {
        AudioSource.PlayClipAtPoint(attack2_audio, attack2_pos.position, 1);
        attack2_effect_playing = Instantiate(attack2_effect, attack2_pos);

        this.gameObject.BroadcastMessage("HurtEnable");
    }

    public void Attack2_End()
    {
        Destroy(attack2_effect_playing);

        this.gameObject.BroadcastMessage("HurtDisable");
    }
    #endregion
}
