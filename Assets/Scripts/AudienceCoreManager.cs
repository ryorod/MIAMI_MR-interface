using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudienceCoreManager : MonoBehaviour
{
    private OSCController osc;
    private bool isManipulated = false;

    void Start()
    {
        this.osc = GameObject.Find("MixedRealityPlayspace").GetComponent<OSCController>();
        this.osc.InitM4L();
    }

    void Update()
    {
        if (this.isManipulated)
        {
            this.SendOSC();
        }
    }

    public void OnManipulation()
    {
        this.isManipulated = true;
    }

    public void OffManipulation()
    {
        this.isManipulated = false;
        this.SendOSC();
    }

    private void SendOSC()
    {
        Vector3 pos = this.transform.position;
        string msg = pos.x.ToString() + " " + pos.y.ToString() + " " + pos.z.ToString();
        this.osc.SendM4L("/" + this.gameObject.name, msg);
    }
}
