using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlackBouncerManager : MonoBehaviour
{
    private OSCController osc;
    [System.NonSerialized] public bool useGravity = false;
    [System.NonSerialized] public Vector3 initPos;
    private Vector3 bounceStartPos;

    void Start()
    {
        this.osc = GameObject.Find("MixedRealityPlayspace").GetComponent<OSCController>();
        this.osc.InitM4L();

        this.initPos = this.transform.position;
        Physics.gravity = new Vector3(0, 0, 0);
    }

    void Update()
    {
        if (this.useGravity)
        {
            if (this.transform.position.y < -2f)
            {
                this.transform.position = this.bounceStartPos;
            }
        }
    }

    void OnCollisionEnter(Collision collision)
    {
        Vector3 velXYZ = this.gameObject.GetComponent<Rigidbody>().velocity;
        float vel = Mathf.Max(Mathf.Abs(velXYZ.x), Mathf.Max(Mathf.Abs(velXYZ.y), Mathf.Abs(velXYZ.z)));
        string msg = vel.ToString();
        this.osc.SendM4L("/" + this.gameObject.name, msg);
    }

    public void StartBouncing()
    {
        this.bounceStartPos = this.transform.position;

        if (!this.useGravity)
        {
            Physics.gravity = new Vector3(0, -9.807f, 0);
            this.useGravity = true;
        }
    }
}
