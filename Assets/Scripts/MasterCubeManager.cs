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
    private bool drumsDelayIsValid = false;
    private bool melReduxIsValid = false;
    private bool bassDelayIsValid = false;
    private bool melFifthChordIsValid = false;

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
            this.osc.SendM4L("/audience_core_is_valid", "0");
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
                if (this.drumsDelayIsValid)
                {
                    this.osc.SendM4L("/drums_delay_dry-wet", "0.");
                    this.drumsDelayIsValid = false;
                }
                else
                {
                    string msg = this.transform.position.y.ToString();
                    this.osc.SendM4L("/drums_delay_dry-wet", msg);
                    this.drumsDelayIsValid = true;
                }
            }
            else if (collision.gameObject == this.mel)
            {
                if (this.melReduxIsValid)
                {
                    this.osc.SendM4L("/mel_redux_downsample", "1.");
                    this.melReduxIsValid = false;
                }
                else
                {
                    string msg = this.transform.position.y.ToString();
                    this.osc.SendM4L("/mel_redux_downsample", msg);
                    this.melReduxIsValid = true;
                }
            }
            else if (collision.gameObject == this.bass)
            {
                if (this.bassDelayIsValid)
                {
                    this.osc.SendM4L("/bass_delay_dry-wet", "0.");
                    this.bassDelayIsValid = false;
                }
                else
                {
                    string msg = this.transform.position.y.ToString();
                    this.osc.SendM4L("/bass_delay_dry-wet", msg);
                    this.bassDelayIsValid = true;
                }
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
            else
            {
                if (this.melFifthChordIsValid)
                {
                    this.osc.SendM4L("/mel_fifth-chord", "0.");
                    this.melFifthChordIsValid = false;
                }
                else
                {
                    this.osc.SendM4L("/mel_fifth-chord", "1.");
                    this.melFifthChordIsValid = true;
                }
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
