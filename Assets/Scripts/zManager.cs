using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class zManager : MonoBehaviour
{
    private OSCController osc;
    public GameObject parentObject;
    private Vector3 zWorldPos;
    private bool isManipulated = false;
    private float localPosBoundary;
    public string instrument = "drums";

    void Start()
    {
        this.osc = GameObject.Find("MixedRealityPlayspace").GetComponent<OSCController>();
        this.osc.Init();

        this.zWorldPos = this.transform.position;
        this.localPosBoundary = 0.5f - this.GetComponent<SphereCollider>().radius * this.transform.localScale.x;
    }

    void Update()
    {
        if (this.isManipulated)
        {
            Vector3 newPos = this.zWorldPos;
            Vector3 newLocalPos = this.parentObject.transform.InverseTransformPoint(newPos);
            if (newLocalPos.x < this.localPosBoundary &&
                newLocalPos.x > -this.localPosBoundary &&
                newLocalPos.y < this.localPosBoundary &&
                newLocalPos.y > -this.localPosBoundary &&
                newLocalPos.z < this.localPosBoundary &&
                newLocalPos.z > -this.localPosBoundary)
            {
                this.transform.position = newPos;
            }
            else
            {
                if (newLocalPos.x >= this.localPosBoundary)
                {
                    newLocalPos.x = this.localPosBoundary;
                    this.transform.localPosition = newLocalPos;
                }
                if (newLocalPos.x <= -this.localPosBoundary)
                {
                    newLocalPos.x = -this.localPosBoundary;
                    this.transform.localPosition = newLocalPos;
                }
                if (newLocalPos.y >= this.localPosBoundary)
                {
                    newLocalPos.y = this.localPosBoundary;
                    this.transform.localPosition = newLocalPos;
                }
                if (newLocalPos.y <= -this.localPosBoundary)
                {
                    newLocalPos.y = -this.localPosBoundary;
                    this.transform.localPosition = newLocalPos;
                }
                if (newLocalPos.z >= this.localPosBoundary)
                {
                    newLocalPos.z = this.localPosBoundary;
                    this.transform.localPosition = newLocalPos;
                }
                if (newLocalPos.z <= -this.localPosBoundary)
                {
                    newLocalPos.z = -this.localPosBoundary;
                    this.transform.localPosition = newLocalPos;
                }

                this.zWorldPos = this.transform.position;
            }
        }
        else 
        {
            this.transform.position = this.zWorldPos;
        }
    }

    public void OnParentTransform()
    {
        this.isManipulated = true;
    }

    public void OffParentTransform()
    {
        this.isManipulated = false;
        this.SendOSC();
    }

    private void SendOSC()
    {
        Vector3 localPos = this.transform.localPosition;
        string zPos = localPos.x.ToString() + " " + localPos.y.ToString() + " " + localPos.z.ToString();
        this.osc.Send("/z_" + this.instrument, zPos);
    }
}
