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
    private bool isMute;

    private int count;

    [PunRPC]
    void mute()
    {
        if (isMute)
        {
            muteImage.enabled = true;
        }
        
    }

    [PunRPC]
    void mute2()
    {
        if (!isMute)
        {
            muteImage.enabled = false;
        }    
    }

    // Start is called before the first frame update
    void Start()
    {
        pv = GetComponent<PhotonView>();
        count = 0;
        recorder = GameObject.Find("VoiceController").GetComponent<Recorder>();
        //muteImage = GameObject.Find("GameManager").GetComponent<GameManager>().myPlayer.transform.Find("Mute").GetComponent<Image>();
        muteImage.enabled = false;
        //muteImage.SetActive(false);
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
                    recorder.IsRecording = false;
                    isMute = true;
                    pv.RPC("mute", RpcTarget.AllBuffered);
                    //mute();
                    //PhotonNetwork.Instantiate("Mute", transform.position, transform.rotation).transform.parent = transform;
                    count++;
                }
                else
                {
                    recorder.IsRecording = true;
                    isMute = false;
                    pv.RPC("mute2", RpcTarget.AllBuffered);
                    //mute2();
                    count++;
                }

            }
        }
        
    }


}
