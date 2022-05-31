using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Photon.Voice.PUN;
using Photon.Voice.Unity;
using Photon.Pun;

public class MuteEmoji : MonoBehaviour
{
    public Recorder recorder;
    public Image muteImage;

    private PhotonView pv;
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
        recorder = GameObject.Find("VoiceController").GetComponent<Recorder>();
        count = 0;
        muteImage.enabled = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.M))
        {
            if (pv.IsMine)
            {
                if (count % 2 == 0)
                {
                    recorder.TransmitEnabled = false;
                    GameObject myPlayer = GameObject.Find("GameManager").GetComponent<GameManager>().myPlayer;
                    pv.RPC("mute", RpcTarget.All);
                    count++;
                }
                else if (count % 2 == 1)
                {
                    recorder.TransmitEnabled = true;
                    count++;
                    GameObject myPlayer = GameObject.Find("GameManager").GetComponent<GameManager>().myPlayer;
                    pv.RPC("mute2", RpcTarget.All);
                }
            }
        }
    }
}
