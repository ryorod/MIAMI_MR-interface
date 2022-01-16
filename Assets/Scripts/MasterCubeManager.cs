using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MasterCubeManager : MonoBehaviour
{
    private OSCController osc;
    private GameObject drums;
    private GameObject mel;
    private GameObject bass;
    [SerializeField] private GameObject audienceCore;
    private Vector3 audienceCoreInitPos;
    private bool isManipulated = false;

    void Start()
    {
        this.osc = GameObject.Find("MixedRealityPlayspace").GetComponent<OSCController>();
        this.osc.InitM4L();

        this.drums = GameObject.Find("LatentSpace_Drums");
        this.mel = GameObject.Find("LatentSpace_Mel");
        this.bass = GameObject.Find("LatentSpace_Bass");

        if (this.audienceCore != null)
        {
            this.audienceCoreInitPos = this.audienceCore.transform.position;
            this.audienceCore.SetActive(false);
        }

        // Somehow there's a NullReferenceException when hitting the drums cube.
        Debug.developerConsoleVisible = false;
    }

    void OnCollisionEnter(Collision collision)
    {
        if (this.isManipulated)
        {
            if (collision.gameObject == this.drums)
            {
                this.osc.SendM4L("/change_drums", "");
            }
            else if (collision.gameObject == this.mel)
            {
                this.osc.SendM4L("/change_mel", "");
            }
            else if (collision.gameObject == this.bass)
            {
                this.osc.SendM4L("/change_bass", "");
            }
            else if (this.audienceCore != null &&
                     collision.gameObject == this.audienceCore)
            {
                this.audienceCore.transform.position = this.audienceCoreInitPos;
                this.audienceCore.SetActive(false);
                this.osc.SendM4L("/audience_core_is_valid", "0");
            }
            else if (this.audienceCore != null &&
                     this.transform.position.x < this.audienceCoreInitPos.x + 0.3f &&
                     this.transform.position.x > this.audienceCoreInitPos.x - 0.3f)
            {
                this.audienceCore.SetActive(true);
                this.osc.SendM4L("/audience_core_is_valid", "1");
            }
        }
    }

    public void OnManipulation()
    {
        this.isManipulated = true;
    }

    public void OffManipulation()
    {
        this.isManipulated = false;
    }
}
