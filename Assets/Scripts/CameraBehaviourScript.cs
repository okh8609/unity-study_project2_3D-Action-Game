using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraBehaviourScript : MonoBehaviour
{
    public Transform cam_pos;

    // Start is called before the first frame update
    void Start()
    {
        this.transform.position = cam_pos.position;
        this.transform.rotation = cam_pos.rotation;
    }

    // Update is called once per frame
    void Update()
    {
        float tt = Time.deltaTime * 6.5f;
        //print(tt);
        this.transform.position = Vector3.Lerp(this.transform.position, cam_pos.position, tt);
        this.transform.rotation = Quaternion.Lerp(this.transform.rotation, cam_pos.rotation, tt);
    }
}
