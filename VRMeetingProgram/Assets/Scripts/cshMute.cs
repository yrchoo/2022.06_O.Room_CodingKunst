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
    public GameObject muteImage;

    private int count;
    private bool isMute;



    [PunRPC]
    void mute()
    {
        if (pv.IsMine)
        {
            muteImage.SetActive(true);
        }
        
    }

    [PunRPC]
    void mute2()
    {
        if (pv.IsMine)
        {
            muteImage.SetActive(false);
        }
        
    }

    // Start is called before the first frame update

    void Start()
    {
        pv = GetComponent<PhotonView>();
        count = 0;

        recorder = GameObject.Find("VoiceController").GetComponent<Recorder>();
        //muteImage = GameObject.Find("GameManager").GetComponent<GameManager>().myPlayer.transform.Find("Mute");

        muteImage.SetActive(false);
        isMute = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (pv.IsMine)
        {
            if (Input.GetKeyDown(KeyCode.M))
            {
                if (count % 2 == 0)
                {
                    isMute = true;
                    recorder.IsRecording = false;
                    muteImage.SetActive(true);
                    count++;
                }
                else
                {
                    recorder.IsRecording = true;
                    muteImage.SetActive(false);
                    count++;
                    isMute = false;
                }
            }
        }
    }

}
