using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Microsoft;
using Microsoft.MixedReality.Toolkit.Utilities;
using Microsoft.MixedReality.Toolkit.Input;

// https://www.benjaminoutram.com/blog/2020/1/28/hololens-2-development-with-unity-hand-tracking-and-accessing-finger-positions

public class FingerTracker : MonoBehaviour
{
    public GameObject fingerObject;
    private MixedRealityPose pose;
    private OSCController osc;

    void Start()
    {
        this.osc = GetComponent<OSCController>();
        this.osc.Init();
    }

    void Update()
    {
        // only render if hand is tracked
        this.fingerObject.GetComponent<Renderer>().enabled = false;

        if (HandJointUtils.TryGetJointPose(TrackedHandJoint.IndexTip, Handedness.Left, out pose))
        {
            this.fingerObject.transform.position = pose.Position;
            this.fingerObject.GetComponent<Renderer>().enabled = true;

            Vector3 pos = this.fingerObject.transform.position;
            Vector3 localPos = this.fingerObject.transform.parent.gameObject.transform.InverseTransformPoint(pos);
            string fingerPos = localPos.x.ToString() + " " + localPos.y.ToString() + " " + localPos.z.ToString();
            this.osc.Send("/z", fingerPos);
        }
    }
}
