using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Photon.Pun;
using Photon.Voice.Unity;


public class cshMute : MonoBehaviour
{
    public Recorder recorder;
    public Image muteImage;
    public PhotonView pv;

    private int count;

    [PunRPC]
    void mute()
    {
        muteImage.enabled = true;
    }

    [PunRPC]
    void mute2()
    {
        muteImage.enabled = false;
    }

    // Start is called before the first frame update
    void Start()
    {
        pv = GetComponent<PhotonView>();
        count = 0;
        recorder = GameObject.Find("VoiceController").GetComponent<Recorder>();
        muteImage.enabled = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.M))
        {
            if(count % 2 == 0)
            {
                recorder.IsRecording = false;
                pv.RPC("mute", RpcTarget.All);
                count++;
            } else
            {
                recorder.IsRecording = true;
                pv.RPC("mute2", RpcTarget.All);
                count++;
            }
            
        }
    }


}
