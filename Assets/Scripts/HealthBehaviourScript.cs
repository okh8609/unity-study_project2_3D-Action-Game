using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthBehaviourScript : MonoBehaviour
{
    public float Health = 999;
    public Role opposite;
    public AudioClip hit_audio;
    public GameObject hit_effect;


    private void OnCollisionEnter(Collision collision)
    {
        HurtBehaviourScript hurt = collision.collider.gameObject.GetComponent<HurtBehaviourScript>();
        if (hurt != null)
        {
            if (hurt.role == opposite && hurt.active)
            {
                hurt.HurtDisable();

                this.Health -= hurt.power;
                print("[" + this.gameObject.name + "] ¥Í©R¤O = " + this.Health);

                AudioSource.PlayClipAtPoint(hit_audio, collision.GetContact(0).point, 1);
                StartCoroutine(PlayEffect(Instantiate(hit_effect, collision.GetContact(0).point, Quaternion.Euler(Vector3.zero), this.transform)));

                Vector3 hit_vec = collision.GetContact(0).point - this.transform.position;
                float hit_angle = Mathf.Atan2(hit_vec.x, hit_vec.z) / Mathf.PI * 180.0f;
                GetComponent<Animator>().SetFloat("HitAngle", hit_angle);
                GetComponent<Animator>().SetTrigger("Hit");

                EnemyBehaviourScript enemy = GetComponent<EnemyBehaviourScript>();
                if (enemy != null)
                    enemy.BeHit();
            }
        }

        //print("Enemy->OnCollisionEnter(): <" + collision.gameObject.tag + "> ::" + collision.gameObject.name);
    }

    IEnumerator PlayEffect(GameObject effect)
    {
        yield return new WaitForSeconds(0.5f);
        Destroy(effect);
    }

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }
}
