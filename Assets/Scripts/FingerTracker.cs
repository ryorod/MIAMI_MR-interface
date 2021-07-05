using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Microsoft;
using Microsoft.MixedReality.Toolkit.Utilities;
using Microsoft.MixedReality.Toolkit.Input;

// https://www.benjaminoutram.com/blog/2020/1/28/hololens-2-development-with-unity-hand-tracking-and-accessing-finger-positions

public class FingerTracker : MonoBehaviour
{
    private MixedRealityPose pose;
    private OSCController osc;

    void Start()
    {
        this.osc = GameObject.Find("MixedRealityPlayspace").GetComponent<OSCController>();
        this.osc.Init();
    }

    void Update()
    {
        // only render if hand is tracked
        this.gameObject.GetComponent<Renderer>().enabled = false;

        if (HandJointUtils.TryGetJointPose(TrackedHandJoint.IndexTip, Handedness.Left, out pose))
        {
            this.transform.position = pose.Position;
            this.gameObject.GetComponent<Renderer>().enabled = true;

            Vector3 pos = this.transform.position;
            Vector3 localPos = this.transform.parent.gameObject.transform.InverseTransformPoint(pos);
            string fingerPos = localPos.x.ToString() + " " + localPos.y.ToString() + " " + localPos.z.ToString();
            this.osc.Send("/z", fingerPos);
        }
    }
}
