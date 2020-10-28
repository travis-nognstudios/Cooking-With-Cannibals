using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TalkingNPC : MonoBehaviour
{
    public GameObject talkingJoint;
    public AudioSource characterVoice;
    public bool xAxis = false;
    public bool zAxis = false;
    public float jawRotx;
    public float jawRotz;
    public float jawWidth;
    public float talkSpeed;

    void Awake()
    {
        jawRotx = talkingJoint.transform.rotation.x;
        jawRotz = talkingJoint.transform.rotation.z;
    }

    // Update is called once per frame
    void Update()
    {
        if (characterVoice.isPlaying && xAxis)
        {
            talkingJoint.transform.localRotation = Quaternion.Euler((jawRotx + Mathf.Sin(Time.time * talkSpeed) * jawWidth), 0, 0);
        }
        else if (characterVoice.isPlaying && zAxis)
        {
            talkingJoint.transform.localRotation = Quaternion.Euler(0, 0, (jawRotz + Mathf.Sin(Time.time * talkSpeed) * jawWidth));
        }
    }
}
