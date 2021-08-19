using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackMagicBehaviourScript : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }


    private void OnCollisionEnter(Collision collision)
    {
        print("AttackMagic->OnCollisionEnter():" + collision.gameObject.name);
    }
}
