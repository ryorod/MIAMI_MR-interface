using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MasterCubeManager : MonoBehaviour
{
    private GameObject blackBouncer;

    void Start()
    {
        this.blackBouncer = GameObject.Find("BlackBouncer");
    }

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.name == "LatentSpace_Drums")
        {
            this.blackBouncer.GetComponent<BlackBouncerManager>().useGravity = false;
            Physics.gravity = new Vector3(0, 0, 0);
            this.blackBouncer.transform.position = this.blackBouncer.GetComponent<BlackBouncerManager>().initPos;
            Rigidbody rb = this.blackBouncer.GetComponent<Rigidbody>();
            rb.velocity = new Vector3(0, 0, 0);
            rb.angularVelocity = new Vector3(0, 0, 0);
        }
    }
}
