using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlackBouncerManager : MonoBehaviour
{
    private OSCController osc;

    void Start()
    {
        this.osc = GameObject.Find("MixedRealityPlayspace").GetComponent<OSCController>();
        this.osc.InitM4L();
    }

    void OnCollisionEnter(Collision collision)
    {
        Vector3 userPos = Camera.main.transform.position;
        Vector3 pos = this.transform.position;
        Vector3 velXYZ = this.gameObject.GetComponent<Rigidbody>().velocity;
        float vel = Mathf.Max(Mathf.Abs(velXYZ.x), Mathf.Max(Mathf.Abs(velXYZ.y), Mathf.Abs(velXYZ.z)));
        string msg = pos.x.ToString() + " " + pos.y.ToString() + " " + pos.z.ToString() + " " +
                     vel.ToString();
        string userPosMsg = userPos.x.ToString() + " " + userPos.y.ToString() + " " + userPos.z.ToString();
        
        this.osc.SendM4L("/user_pos", userPosMsg);
        this.osc.SendM4L("/" + this.gameObject.name, msg);
    }

    void OnCollisionExit(Collision collision)
    {
        this.osc.SendM4L("/" + this.gameObject.name + "_release", "");
    }
}
