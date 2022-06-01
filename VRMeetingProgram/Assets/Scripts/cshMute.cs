using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Photon.Pun;
using Photon.Voice.Unity;


public class cshMute : MonoBehaviour
{
    public Recorder recorder;
    public PhotonView pv;
    public Image muteImage;

    private int count;
    private bool isMute;

    void Start()
    {
        pv = GetComponent<PhotonView>();
        count = 0;
        //muteImage = GameObject.Find("GameManager").GetComponent<GameManager>().myPlayer.transform.Find("Mute").GetComponent<Image>();
        muteImage = GameObject.Find("Mute").GetComponent<Image>();
        muteImage.enabled = false;
        recorder = GameObject.Find("VoiceController").GetComponent<Recorder>();
        isMute = false;
    }

    // Update is called once per frame
    void Update()
    {

        if (Input.GetKeyDown(KeyCode.M))
        {
            if (count % 2 == 0)
            {
                isMute = true;
                recorder.IsRecording = false;
                count++;
                if (pv.IsMine)
                {

                    muteImage.enabled = true;

                }
            }
            else
            {
                recorder.IsRecording = true;
                count++;
                isMute = false;

                if (pv.IsMine)
                {
                    muteImage.enabled = false;

                }
            }

        }

        
    }

}
