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

        animator.SetBool("Jump", Input.GetKey(KeyCode.Z));
        animator.SetBool("Attack1", Input.GetKey(KeyCode.X) || Input.GetMouseButton(0));
        animator.SetBool("Attack2", Input.GetKey(KeyCode.C) || Input.GetMouseButton(1));
    }

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
}
