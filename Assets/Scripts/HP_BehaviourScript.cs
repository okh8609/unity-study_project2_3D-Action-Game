using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HP_BehaviourScript : MonoBehaviour
{
    float original_HP;
    float original_scale;

    // Start is called before the first frame update
    void Start()
    {
        original_HP = GetComponentInParent<HealthBehaviourScript>().HP;
        original_scale = this.transform.localScale.x;
    }

    // Update is called once per frame
    void Update()
    {
        float now_HP = GetComponentInParent<HealthBehaviourScript>().HP;
        if (now_HP <= 0)
        {
            Destroy(this.gameObject);
            return;
        }
        float new_scale = (now_HP / original_HP) * original_scale;
        this.transform.localScale = new Vector3(new_scale, this.transform.localScale.y, this.transform.localScale.z);
    }
}
