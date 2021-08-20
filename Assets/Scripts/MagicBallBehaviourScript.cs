using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MagicBallBehaviourScript : MonoBehaviour
{
    public float SurvivalTime = 2.0f;
    float Deadline = 0.0f;

    // Start is called before the first frame update
    void Start()
    {
        Deadline = Time.time + SurvivalTime;
    }

    // Update is called once per frame
    void Update()
    {
        if (Time.time > Deadline)
            Destroy(this.gameObject);
    }

    private void OnCollisionEnter(Collision collision)
    {
        //Time.timeScale = 0; // ���C���i�J�Ȱ����A
        //print(collision.gameObject.name);
        //print(collision.gameObject.tag);

        if (!collision.gameObject.CompareTag("Self"))
            Destroy(this.gameObject);
    }
}