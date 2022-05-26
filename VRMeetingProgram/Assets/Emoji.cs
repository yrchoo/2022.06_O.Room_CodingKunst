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

    public int index;
    
    public bool activeIF;

    private float fDestroyTime = 2f;
    private float fTickTime = 2f;

    // Start is called before the first frame update
    void Start()
    {
        pv = GetComponent<PhotonView>();
        ChatInput = GameObject.Find("ChatInput").GetComponent<InputField>();
    }

    // Update is called once per frame
    void Update()
    {
        if (pv.IsMine)
        {
            fTickTime += Time.deltaTime;
            if (fTickTime >= fDestroyTime)
            {
                if (Input.GetKeyDown(KeyCode.Alpha1))
                {
                    CreateEmoji("crying");
                    fTickTime = 0;
                }
                else if (Input.GetKeyDown(KeyCode.Alpha2))
                {
                    CreateEmoji("Smiley01");
                    fTickTime = 0;
                }
                else if (Input.GetKeyDown(KeyCode.Alpha3))
                {
                    CreateEmoji("Smiley04");
                    fTickTime = 0;
                }
                else if (Input.GetKeyDown(KeyCode.Alpha4))
                {
                    CreateEmoji("Smiley16");
                    fTickTime = 0;
                }
                else if (Input.GetKeyDown(KeyCode.Alpha5))
                {
                    CreateEmoji("Smiley40");
                    fTickTime = 0;
                }
                else if (Input.GetKeyDown(KeyCode.Alpha6))
                {
                    CreateEmoji("Smiley50");
                    fTickTime = 0;
                }
            }
            if(Input.GetKey(KeyCode.T)){
                ChatInput.ActivateInputField();
                ChatInput.Select();
            }
        }
    }

    void CreateEmoji(string emoji)
    {
        if (pv.IsMine)
        {
            Debug.Log("CreateEmoji");
            //Transform spawnPoints = GameObject.Find("EmojiSpawnPoint").GetComponentInChildren<Transform>();
            Transform spawnPoints = transform.Find("EmojiSpawnPoint").GetComponentInChildren<Transform>();
            Vector3 pos = spawnPoints.transform.position;
            Quaternion rot = spawnPoints.transform.rotation;
            //Quaternion rot = transform.parent.transform.rotation;

            //rot.y += 180.0f;

            //PhotonNetwork.Instantiate(emoji, pos, rot).transform.parent = parent.transform;
            PhotonNetwork.Instantiate(emoji, pos, rot).transform.parent = transform;
        }
       
    }
    
}


