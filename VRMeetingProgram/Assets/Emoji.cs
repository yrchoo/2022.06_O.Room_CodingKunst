using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;
using System.Reflection;
using UnityEngine.UI;

public class Emoji : MonoBehaviour
{

    private PhotonView pv;

    public InputField ChatInput;

    int index;
    
    public bool activeIF;

    GameObject emoji;
    GameObject[] Emojis;
    //int index;

    private float fDestroyTime = 2f;
    private float fTickTime = 2f;

    // Start is called before the first frame update
    void Start()
    {
        pv = GetComponent<PhotonView>();
        ChatInput = GameObject.Find("ChatInput").GetComponent<InputField>();
        Emojis = new GameObject[6];
        for (int i = 0; i<Emojis.Length; i++){
            Emojis[i] = gameObject.transform.Find($"Emoji{i+1}").gameObject;
            index = i;
            pv.RPC("DeActiveEmoji", RpcTarget.All, index);
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (pv.IsMine)
        {
            fTickTime += Time.deltaTime;
            if (fTickTime >= fDestroyTime)
            {
                pv.RPC("DeActiveEmoji", RpcTarget.All, index);

                if (Input.GetKeyDown(KeyCode.Alpha1))
                {
                    //emoji = Emojis[0];
                    index = 0;
                    fTickTime = 0;
                    pv.RPC("CreateEmoji", RpcTarget.All, 0);
                }
                else if (Input.GetKeyDown(KeyCode.Alpha2))
                {
                    //emoji = Emojis[1];
                    index = 1;
                    fTickTime = 0;
                    pv.RPC("CreateEmoji", RpcTarget.All, 1);
                }
                else if (Input.GetKeyDown(KeyCode.Alpha3))
                {
                    //emoji = Emojis[2];
                    fTickTime = 0;
                    index = 2;
                    pv.RPC("CreateEmoji", RpcTarget.All, 2);
                }
                else if (Input.GetKeyDown(KeyCode.Alpha4))
                {
                    //emoji = Emojis[3];
                    fTickTime = 0;
                    index = 3;
                    pv.RPC("CreateEmoji", RpcTarget.All, 3);
                }
                else if (Input.GetKeyDown(KeyCode.Alpha5))
                {
                    //emoji = Emojis[4];
                    fTickTime = 0;
                    index = 4;
                    pv.RPC("CreateEmoji", RpcTarget.All, 4);
                }
                else if (Input.GetKeyDown(KeyCode.Alpha6))
                {
                    //emoji = Emojis[5];
                    fTickTime = 0;
                    index = 5;
                    pv.RPC("CreateEmoji", RpcTarget.All, 5);
                }
            }
            
        }
    }

    [PunRPC]
    void CreateEmoji(int index)
    {
        
        // Debug.Log("CreateEmoji");
        // Transform spawnPoints = transform.Find("EmojiSpawnPoint").GetComponentInChildren<Transform>();
        // Vector3 pos = spawnPoints.transform.position;
        // Quaternion rot = spawnPoints.transform.rotation;
        // PhotonNetwork.Instantiate(emoji, pos, rot).transform.parent = transform;
        Emojis[index].SetActive(true);
       
    }

    [PunRPC]
    void DeActiveEmoji(int index){
        Emojis[index].SetActive(false);
    }
    
}


