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
    public void mute()
    {
            muteImage.SetActive(true);
        
    }

    [PunRPC]
    public void mute2()
    {
            muteImage.SetActive(false);
        
    }

    // Start is called before the first frame update

    void Start()
    {
        pv = gameObject.GetComponent<PhotonView>();
        count = 0;
        
        recorder = GameObject.Find("VoiceController").GetComponent<Recorder>();
        //muteImage = GameObject.Find("GameManager").GetComponent<GameManager>().myPlayer.transform.Find("Mute");
        //recorder.IsRecording = false;

        if (pv.IsMine)
        {
            pv.RPC("mute2", RpcTarget.All);
        }
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
                    // Transform spawnPoints = transform.Find("MuteSpawnPoint").GetComponentInChildren<Transform>();
                    // Vector3 pos = spawnPoints.transform.position;
                    // Quaternion rot = spawnPoints.transform.rotation;
                    pv.RPC("mute", RpcTarget.All);
                    count++;
                }
                else
                {
                    recorder.IsRecording = true;
                    pv.RPC("mute2", RpcTarget.All);
                    count++;
                    isMute = false;
                }
            }
        }
    }

}
