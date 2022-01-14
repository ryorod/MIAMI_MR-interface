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
        Vector3 pos = this.transform.position;
        Vector3 velXYZ = this.gameObject.GetComponent<Rigidbody>().velocity;
        float vel = (velXYZ.x + velXYZ.y + velXYZ.z) / 3f;
        string msg = pos.x.ToString() + " " + pos.y.ToString() + " " + pos.z.ToString() + " " +
                     vel.ToString();
        this.osc.SendM4L("/" + this.gameObject.name, msg);
    }

    void OnCollisionExit(Collision collision)
    {
        this.osc.SendM4L("/" + this.gameObject.name + "_release", "");
    }
}
