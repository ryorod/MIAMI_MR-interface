using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPropertyManager : MonoBehaviour
{
    private OSCController osc;
    private GameObject obj = null;
    private GameObject obj_2 = null;
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
            if (this.obj != null)
            {
                this.SendOSC(this.obj);
            }
            if (this.obj_2 != null)
            {
                this.SendOSC(this.obj_2);
            }
        }
    }

    public void OnParentTransform(GameObject obj)
    {
        if (this.obj == null)
        {
            this.obj = obj;
            if (!this.isManipulated) this.isManipulated = true;
        }
        else if (this.obj_2 == null)
        {
            this.obj_2 = obj;
            if (!this.isManipulated) this.isManipulated = true;
        }
    }

    public void OffParentTransform(GameObject obj)
    {
        if (this.obj == obj)
        {
            if (this.obj_2 == null) this.isManipulated = false;
            this.SendOSC(this.obj);
            this.obj = null;
        }
        else if (this.obj_2 == obj)
        {
            if (this.obj == null) this.isManipulated = false;
            this.SendOSC(this.obj_2);
            this.obj_2 = null;
        }
    }

    private void SendOSC(GameObject sendObj)
    {
        Vector3 pos = sendObj.transform.position;
        Vector3 angle = sendObj.transform.eulerAngles;
        Vector3 sizeXYZ = sendObj.transform.lossyScale;
        float size = (sizeXYZ.x + sizeXYZ.y + sizeXYZ.z) / 3f;
        string msg = pos.x.ToString() + " " + pos.y.ToString() + " " + pos.z.ToString() + " " +
                     angle.x.ToString() + " " + angle.y.ToString() + " " + angle.z.ToString() + " " +
                     size.ToString();
        this.osc.SendM4L("/" + sendObj.name, msg);
    }
}
