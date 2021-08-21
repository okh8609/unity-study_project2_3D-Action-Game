using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MagicBallBehaviourScript : MonoBehaviour
{
    public float SurvivalTime = 2.0f;
    float Deadline = 0.0f;
    public AudioClip hit_audio;
    public GameObject hit_effect;

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
        //Time.timeScale = 0; // 讓遊戲進入暫停狀態
        //print(collision.gameObject.name);
        //print(collision.gameObject.tag);

        if (!collision.gameObject.CompareTag("Self"))
        {
            Destroy(this.gameObject);
            AudioSource.PlayClipAtPoint(hit_audio, collision.GetContact(0).point, 1);
            StartCoroutine(PlayEffect(Instantiate(hit_effect, collision.GetContact(0).point, Quaternion.Euler(Vector3.zero))));
        }
    }

    IEnumerator PlayEffect(GameObject effect)
    {
        yield return new WaitForSeconds(1.5f);
        Destroy(effect);
    }
}
