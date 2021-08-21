using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public enum Role { Player, Enemy };
public enum Type { Hand, Knife, Foot, Magic };

public class HurtBehaviourScript : MonoBehaviour
{
    public Role role;
    public Type type;

    public float power;

    public bool active;
    public void HurtEnable() { active = true; }
    public void HurtDisable() { active = false; }

    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {

    }
}
