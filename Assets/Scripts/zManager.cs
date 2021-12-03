using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class zManager : MonoBehaviour
{
    private OSCController osc;
    public GameObject parentObject;
    public float span = 4f;
    private float currentTime = 0f;

    void Start()
    {
        this.osc = GameObject.Find("MixedRealityPlayspace").GetComponent<OSCController>();
        this.osc.Init();
    }

    void Update()
    {
        this.currentTime += Time.deltaTime;

        if(this.currentTime > this.span){
            currentTime = 0f;

            Vector3 pos = this.transform.position;
            Vector3 localPos = this.parentObject.transform.InverseTransformPoint(pos);
            string zPos = localPos.x.ToString() + " " + localPos.y.ToString() + " " + localPos.z.ToString();
            this.osc.Send("/z", zPos);
        }
    }
}
